using AutoMapper;
using PersonalProyect.Data;
using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Data.Entities;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Services.Implementations
{
    public class CustomerService : CustomQueryableOperationsService, ICustomerService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CustomerService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Create Customer

        public async Task<Response<CustomerDTO>> CreateAsync(CustomerDTO dto)
        {
            return await CreateAsync<Customer, CustomerDTO>(dto);
        }

        // Delete Customer
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<Customer>(id);
        }

        // Update Customer
        public async Task<Response<CustomerDTO>> UpdateAsync(Guid id, CustomerDTO dto)
        {
            return await UpdateAsync<Customer, CustomerDTO>(id, dto);
        }

        // Get 
        public async Task<Response<CustomerDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Customer, CustomerDTO>(id);
        }

        public async Task<Response<List<CustomerDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Customer, CustomerDTO>();
        }
    }
}
