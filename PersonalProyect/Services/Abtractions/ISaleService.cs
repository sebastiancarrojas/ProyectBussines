using PersonalProyect.DTOs;
using PersonalProyect.Core;

namespace PersonalProyect.Services.Abtractions
{
    public interface ISaleService
    {
        public Task<Response<SaleDTO>> CreateAsync(SaleDTO dto);
        public Task<Response<SaleDTO>> GetOneAsync(Guid id);
        public Task<Response<List<SaleDTO>>> GetCompleteListAsync();
    }
}
