using PersonalProyect.DTOs;
using PersonalProyect.Core;

namespace PersonalProyect.Services.Abtractions
{
    public interface ICustomerService
    {
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<CustomerDTO>> CreateAsync(CustomerDTO dto);
        public Task<Response<CustomerDTO>> UpdateAsync(Guid id, CustomerDTO dto);
        public Task<Response<CustomerDTO>> GetOneAsync(Guid id);
        public Task<Response<List<CustomerDTO>>> GetCompleteListAsync();
    }
}
