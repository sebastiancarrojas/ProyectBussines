using AutoMapper;
using PersonalProyect.Data;
using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Data.Entities;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class ProductService : CustomQueryableOperationsService, IProductService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create Product
        public async Task<Response<ProductDTO>> CreateAsync(ProductDTO dto)
        {
            try
            {
                if (dto.ImageFile != null)
                {
                    var folder = Path.Combine("wwwroot", "images", "products");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(dto.ImageFile.FileName)}";
                    var filePath = Path.Combine(folder, fileName);

                    using var stream = new FileStream(filePath, FileMode.Create);
                    await dto.ImageFile.CopyToAsync(stream);

                    dto.ImageUrl = $"/images/products/{fileName}";
                }

                var entity = _mapper.Map<Product>(dto);
                _context.Products.Add(entity);
                await _context.SaveChangesAsync();

                var resultDto = _mapper.Map<ProductDTO>(entity);
                return new Response<ProductDTO> { Result = resultDto };
            }
            catch (Exception ex)
            {
                return Response<ProductDTO>.Failure(ex, "Error al crear el producto");

            }

        }


        // Delete Product
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<Product>(id);
        }

        // Update Product
        public async Task<Response<ProductDTO>> UpdateAsync(Guid id, ProductDTO dto)
        {
            return await UpdateAsync<Product, ProductDTO>(id, dto);
        }

        // Get 
        public async Task<Response<ProductDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Product, ProductDTO>(id);
        }

        public async Task<Response<List<ProductDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Product, ProductDTO>();
        }
    }
}
