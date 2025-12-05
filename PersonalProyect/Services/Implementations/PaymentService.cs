using PersonalProyect.Services.Abtractions;
using PersonalProyect.Data.Entities;
using AutoMapper;
using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Data;

namespace PersonalProyect.Services.Implementations
{
    public class PaymentService : CustomQueryableOperationsService, IPaymentService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public PaymentService(DataContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        // Create Payment
        public async Task<Response<PaymentDTO>> CreateAsync(PaymentDTO dto)
        {
            return await CreateAsync<Payment, PaymentDTO>(dto);
        }
        // Get
        public async Task<Response<PaymentDTO>> GetOneAsync(Guid id)
        {
            return await GetOneAsync<Payment, PaymentDTO>(id);
        }
        public async Task<Response<List<PaymentDTO>>> GetCompleteListAsync()
        {
            return await GetCompleteListAsync<Payment, PaymentDTO>();
        }
    }
}
