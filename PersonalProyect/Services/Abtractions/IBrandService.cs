using PersonalProyect.Core;
using PersonalProyect.DTOs;

namespace PersonalProyect.Services.Abtractions
{
    public interface IBrandService
    {
        public Task<Response<BrandDTO>> CreateAsync(BrandCreateDTO dto);
        public Task<Response<List<BrandDTO>>> GetCompleteListAsync();
    }
}
