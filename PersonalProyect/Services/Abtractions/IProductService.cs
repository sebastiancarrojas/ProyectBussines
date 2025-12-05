using PersonalProyect.Core;
using PersonalProyect.DTOs;

namespace PersonalProyect.Services.Abtractions
{
    public interface IProductService
    {
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<ProductDTO>> CreateAsync(DTOs.ProductDTO dto);
        public Task<Response<ProductDTO>> UpdateAsync(Guid id, ProductDTO dto);
        public Task<Response<ProductDTO>> GetOneAsync(Guid id);
        public Task<Response<List<ProductDTO>>> GetCompleteListAsync();
    }
}
