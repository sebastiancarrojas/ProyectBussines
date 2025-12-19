using PersonalProyect.Core;
using PersonalProyect.Core.Pagination;
using PersonalProyect.DTOs.Sales;

namespace PersonalProyect.Services.Abtractions
{
    public interface ISaleService
    {
        public Task<Response<Guid>> CreateAsync(CreateSaleDTO dto, Guid userId);
        public Task<Response<PaginationResponse<SaleListDTO>>> GetPaginatedListAsync(SalePaginationRequest request);
    }
}
