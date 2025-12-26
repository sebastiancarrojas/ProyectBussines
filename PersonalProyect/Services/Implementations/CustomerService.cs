using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.Data.Enum;
using PersonalProyect.DTOs.Customers;
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

        public async Task<Response<Guid>> CreateQuickCustomerAsync(QuickCreateCustomerDto dto)
        {
            if (!Enum.TryParse<DocumentType>(
                    dto.DocumentType,
                    true,
                    out var documentType))
            {
                return Response<Guid>.Failure("Tipo de documento inválido");
            }

            var exists = await _context.Customers.AnyAsync(c =>
                c.DocumentType == documentType &&
                c.DocumentNumber == dto.DocumentNumber
            );


            if (exists)
                return Response<Guid>.Failure("El cliente ya existe");

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                DocumentType = documentType,
                DocumentNumber = dto.DocumentNumber,
                FullName = dto.FullName,
            };

            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();

            return Response<Guid>.Success(customer.Id);

        }

        public async Task<Response<List<CustomerLookupDTO>>> SearchByDocumentAsync(string document)
        {
            if (string.IsNullOrWhiteSpace(document))
                return Response<List<CustomerLookupDTO>>.Success(new List<CustomerLookupDTO>());

            var customers = await _context.Customers
                .Where(c => c.DocumentNumber == document) 
                .OrderBy(c => c.FullName)
                .Select(c => new CustomerLookupDTO
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    DocumentNumber = c.DocumentNumber
                })
                .ToListAsync();

            return Response<List<CustomerLookupDTO>>.Success(customers);
        }




        // Create Customer

        public async Task<Response<CreateCustomerDTO>> CreateAsync(CreateCustomerDTO dto)
        {
            return await CreateAsync<Customer, CreateCustomerDTO>(dto);
        }

        // Delete Customer
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            return await DeleteAsync<Customer>(id);
        }

        // Update Customer
        public async Task<Response<CreateCustomerDTO>> UpdateAsync(Guid id, CreateCustomerDTO dto)
        {
            return await UpdateAsync<Customer, CreateCustomerDTO>(id, dto);
        }

        // Get 
        public async Task<Response<CreateCustomerDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Customer, CreateCustomerDTO>(id);
        }

        public async Task<Response<List<CreateCustomerDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Customer, CreateCustomerDTO>();
        }
    }
}
