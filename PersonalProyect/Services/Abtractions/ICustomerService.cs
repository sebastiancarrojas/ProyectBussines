using PersonalProyect.Core;
using PersonalProyect.DTOs.Customers;

namespace PersonalProyect.Services.Abtractions
{
    public interface ICustomerService
    {
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<CreateCustomerDTO>> CreateAsync(CreateCustomerDTO dto);
        public Task<Response<CreateCustomerDTO>> UpdateAsync(Guid id, CreateCustomerDTO dto);
        public Task<Response<CreateCustomerDTO>> GetOneAsync(Guid id);
        public Task<Response<List<CreateCustomerDTO>>> GetCompleteListAsync();
        public Task<Response<Guid>> CreateQuickCustomerAsync(QuickCreateCustomerDto dto);
        public Task<Response<List<CustomerLookupDTO>>> SearchByDocumentAsync(string document);
    }
}
