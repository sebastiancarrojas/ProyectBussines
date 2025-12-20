using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Core.Pagination;
using PersonalProyect.Data;
using PersonalProyect.Data.Abstractions;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs.Products;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class ProductService : CustomQueryableOperationsService, IProductService
    {
        // --------------------------------------
        // -- Inyectar dependencias necesarias --
        // --------------------------------------

        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // --------------------
        // -- Crear producto --
        // --------------------

        public async Task<Response<ProductCreateDTO>> CreateAsync(ProductCreateDTO dto)
        {
            try
            {
                var exists = await _context.Products
                    .AnyAsync(p => p.Barcode == dto.Barcode);

                if (exists)
                {
                    return Response<ProductCreateDTO>.Failure(
                        "Ya existe un producto con ese código de barras"
                    );
                }

                var entity = _mapper.Map<Product>(dto);
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<ProductCreateDTO>(entity);

                return Response<ProductCreateDTO>.Success(resultDto, "Producto creado correctamente");
            }
            catch (Exception ex)
            {
                return Response<ProductCreateDTO>.Failure(ex, "Error al crear el producto");
            }
        }

        // --------------------------------
        // -- Obtener un producto por Id --
        // --------------------------------

        public async Task<Response<ProductCreateDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Product, ProductCreateDTO>(id);
        }

        // --------------------------------
        // -- Obtener lista de productos --
        // --------------------------------

        public async Task<Response<List<ProductCreateDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Product, ProductCreateDTO>();
        }

        // -------------------------
        // -- Desactivar producto --
        // -------------------------

        public async Task<Response<object>> DeactivateAsync(Guid id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return new Response<object>
                {
                    IsSuccess = false,
                    Message = "Producto no encontrado"
                };

            // Cambiar estado a inactivo
            product.Status = "Inactivo";
            product.UpdatedAt = DateTime.Now;

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return new Response<object>
            {
                IsSuccess = true,
                Message = "Producto desactivado correctamente"
            };
        }


        // ---------------------
        // -- Editar producto --
        // ---------------------
        public async Task<Response<ProductEditDTO>> UpdateAsync(Guid id, ProductEditDTO dto)
        {
            try
            {
                var entity = await _context.Products.FindAsync(id);
                if (entity == null)
                    return Response<ProductEditDTO>.Failure(null, "Producto no encontrado");

                // Validaciones FK
                if (dto.BrandId.HasValue &&
                    !await _context.Brands.AnyAsync(b => b.Id == dto.BrandId.Value))
                    return Response<ProductEditDTO>.Failure(null, "Marca inválida");

                if (dto.CategoryId.HasValue &&
                    !await _context.Categories.AnyAsync(c => c.Id == dto.CategoryId.Value))
                    return Response<ProductEditDTO>.Failure(null, "Categoría inválida");

                // Mapeo parcial seguro
                _mapper.Map(dto, entity);

                // Asignación explícita de FK
                if (dto.BrandId.HasValue)
                    entity.BrandId = dto.BrandId.Value;

                if (dto.CategoryId.HasValue)
                    entity.CategoryId = dto.CategoryId.Value;

                entity.UpdatedAt = DateTime.Now;

                await _context.SaveChangesAsync();

                return Response<ProductEditDTO>.Success(dto, "Producto actualizado correctamente");
            }
            catch (DbUpdateException ex)
            {
                return Response<ProductEditDTO>.Failure(
                    ex,
                    ex.InnerException?.Message ?? ex.Message
                );
            }
        }

        // --------------------
        // -- Lista paginada --
        // --------------------

        public async Task<Response<PaginationResponse<ProductListDTO>>> GetPaginatedListAsync(ProductPaginationRequest request)
        {
            var queryable = _context.Products
                .Include(p => p.Brands)
                .Include(p => p.Categories)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Filter))
            {
                string filter = request.Filter.ToLower(); // Opcional: búsqueda case-insensitive
                queryable = queryable.Where(p =>
                    p.ProductName.ToLower().Contains(filter) ||
                    p.Barcode.ToLower().Contains(filter) ||
                    p.Brands.BrandName.ToLower().Contains(filter) ||
                    p.Categories.CategoryName.ToLower().Contains(filter));
            }

            if (request.CategoryId.HasValue)
                queryable = queryable.Where(p =>
                    p.CategoryId == request.CategoryId.Value);

            if (request.BrandId.HasValue)
                queryable = queryable.Where(p =>
                    p.BrandId == request.BrandId.Value);

            if (!string.IsNullOrWhiteSpace(request.Status))
                queryable = queryable.Where(p => p.Status == request.Status);

            if (request.StockMin.HasValue)
                queryable = queryable.Where(p => p.CurrentStock >= request.StockMin.Value);

            if (request.StockMax.HasValue)
                queryable = queryable.Where(p => p.CurrentStock <= request.StockMax.Value);

            var dtoQueryable = queryable.Select(p => new ProductListDTO
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Barcode = p.Barcode,
                BrandName = p.Brands.BrandName,
                CategoryName = p.Categories.CategoryName,
                CurrentStock = p.CurrentStock,
                Status = p.Status,
                UnitPrice = p.UnitPrice,
                StockMin = p.StockMin,
                ProductDescription = p.ProductDescription
            });

            return await GetPaginationAsync<ProductListDTO>(
                request,
                dtoQueryable
            );
        }
        // ----------------------------------------
        // -- Buscar producto en registrar venta --
        // ----------------------------------------

        public async Task<Response<List<ProductSearchDTO>>> SearchForSaleAsync(string term)
        {
            if (string.IsNullOrWhiteSpace(term))
                return Response<List<ProductSearchDTO>>.Success(new List<ProductSearchDTO>());

            var products = await _context.Products
                .Where(p =>
                    p.Status == "Activo" &&
                    (p.ProductName.Contains(term) ||
                     p.Barcode.Contains(term)))
                .OrderBy(p => p.ProductName)
                .Take(10)
                .Select(p => new ProductSearchDTO
                {
                    Id = p.Id,
                    ProductName = p.ProductName,
                    Barcode = p.Barcode,
                    UnitPrice = p.UnitPrice,
                    CurrentStock = p.CurrentStock
                })
                .ToListAsync();

            return Response<List<ProductSearchDTO>>.Success(products);
        }

    }
}
