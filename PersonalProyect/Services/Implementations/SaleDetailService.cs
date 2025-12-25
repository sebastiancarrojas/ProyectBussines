using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.DTOs.SalesDetails;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class SaleDetailService : CustomQueryableOperationsService, ISaleDetail
    {
        // Inyectar dependencias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public SaleDetailService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GetSaleDetailsBySaleIdAsync

        public async Task<Response<List<SaleDetailModalDTO>>> GetSaleDetailsBySaleIdAsync(Guid SaleId)
        {
            var queryable = _context.SalesDetails.Where(sd => sd.SaleId == SaleId);

            var dtoQueryable = queryable.Select(sd => new SaleDetailModalDTO {
                Id = sd.Id,

                ProductName = sd.IsTemporary ? sd.ProductName : sd.Products != null ? sd.Products.ProductName : null,

                Quantity = sd.Quantity,
                UnitPrice = sd.UnitPrice,
                SubTotal = sd.SubTotal,
                IsTemporary = sd.IsTemporary,
                });

            var result = await dtoQueryable.ToListAsync();

            return Response<List<SaleDetailModalDTO>>.Success(result);
        }


    }
}
