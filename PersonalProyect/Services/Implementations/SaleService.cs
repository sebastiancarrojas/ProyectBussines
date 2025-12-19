using AutoMapper;
using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Core.Pagination;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.Data.Enum;
using PersonalProyect.DTOs.Sales;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class SaleService : CustomQueryableOperationsService, ISaleService
    {
        // Intectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SaleService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create Sale
        public async Task<Response<Guid>> CreateAsync(CreateSaleDTO dto, Guid userId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // 1️⃣ Validaciones básicas
                if (dto.Details == null || !dto.Details.Any())
                    return Response<Guid>.Failure(null, "La venta no tiene productos");

                // 2️⃣ Obtener productos reales
                var productIds = dto.Details.Select(d => d.ProductId).ToList();

                var products = await _context.Products
                    .Where(p => productIds.Contains(p.Id))
                    .ToListAsync();

                if (products.Count != productIds.Count)
                    return Response<Guid>.Failure(null, "Uno o más productos no existen");

                // 3️⃣ Crear venta (cabecera)
                var sale = new Sale
                {
                    Id = Guid.NewGuid(),
                    SaleDate = DateTime.Now,
                    UserId = userId,
                    CustomerId = dto.CustomerId,
                    SaleType = SaleType.Contado,
                    PaymentStatus = PaymentStatus.Pagada,
                    TotalAmount = 0m
                };

                decimal total = 0;

                // 4️⃣ Crear detalles
                foreach (var item in dto.Details)
                {
                    var product = products.First(p => p.Id == item.ProductId);

                    if (item.Quantity <= 0)
                        return Response<Guid>.Failure(null, "Cantidad inválida");

                    if (product.CurrentStock < item.Quantity)
                        return Response<Guid>.Failure(
                            null,
                            $"Stock insuficiente para {product.ProductName}"
                        );

                    var subTotal = product.UnitPrice * item.Quantity;
                    total += subTotal;



                    sale.SalesDetails.Add(new SaleDetail
                    {
                        ProductId = product.Id,
                        Quantity = item.Quantity,
                        UnitPrice = product.UnitPrice,
                        SubTotal = subTotal,
                        SaleId = sale.Id,
                    });

                    // 5️⃣ Descontar stock
                    product.CurrentStock -= item.Quantity;
                }

                // 6️⃣ Total
                sale.TotalAmount = total;

                // 7️⃣ Guardar
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return Response<Guid>.Success(sale.Id, "Venta registrada correctamente");
            }
            catch (DbUpdateException ex)
            {
                await transaction.RollbackAsync();

                var innerMessage =
                    ex.InnerException?.InnerException?.Message ??
                    ex.InnerException?.Message ??
                    ex.Message;

                Console.WriteLine("EF CORE ERROR:");
                Console.WriteLine(innerMessage);

                return new Response<Guid>
                {
                    IsSuccess = false,
                    Message = "Error al registrar la venta",
                    Errors = new List<string> { innerMessage }
                };
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                return Response<Guid>.Failure(ex, "Error inesperado al registrar la venta");
            }
        }

        // -------------------
        // -- Listar ventas --
        // -------------------

        public async Task<Response<PaginationResponse<SaleListDTO>>> GetPaginatedListAsync (SalePaginationRequest request)
        {
            var queryable = _context.Sales.Include(s => s.Users).Include(s => s.Customers).AsQueryable();


            if(!string.IsNullOrWhiteSpace(request.Filter))
            {
                string filter = request.Filter.ToLower();
                queryable = queryable.Where(s => s.Users.FullName.ToLower().Contains(filter) || 
                                                 s.Customers.FullName.ToLower().Contains(filter) ||
                                                 s.SaleNumber.ToString().Contains(filter)
                                                 
                );
            }

            if (request.StartDate.HasValue)
            {
                queryable = queryable.Where(s =>
                    s.SaleDate >= request.StartDate.Value);
            }

            if (request.EndDate.HasValue)
            {
                queryable = queryable.Where(s =>
                    s.SaleDate < request.EndDate.Value);
            }

            var dtoQueryable = queryable.Select(s => new SaleListDTO
            {
                Id = s.Id,
                SaleDate = s.SaleDate,
                SaleDateOnly = s.SaleDate.ToString("yyyy-MM-dd"),
                SaleTimeOnly = s.SaleDate.ToString("HH:mm:ss"),
                SaleType = s.SaleType.ToString(),
                TotalAmount = s.TotalAmount,    
                PaymentStatus = s.PaymentStatus.ToString(),
                UserName = s.Users.FullName,
                CustomerName = s.Customers.FullName,
                SaleNumber = s.SaleNumber.ToString(),
            });
            return await GetPaginationAsync<SaleListDTO>(request, dtoQueryable);
        }
    }
}
