using PersonalProyect.Core;
using PersonalProyect.DTOs;

namespace PersonalProyect.Services.Abtractions
{
    public interface ISaleDetail
    {
        public Task<Response<SaleDetailDTO>> CreateAsync (DTOs.SaleDetailDTO dto);
        public Task<Response<SaleDetailDTO>> UpdateAsync (Guid id, SaleDetailDTO dto);
        public Task<Response<SaleDetailDTO>> GetOneAsync (Guid id);
        public Task<Response<List<SaleDetailDTO>>> GetCompleteListAsync ();
        public Task<Response<object>> DeleteAsync (Guid id);

    }
}
