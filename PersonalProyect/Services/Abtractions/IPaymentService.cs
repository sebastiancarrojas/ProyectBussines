using PersonalProyect.DTOs;
using PersonalProyect.Core;

namespace PersonalProyect.Services.Abtractions
{
    public interface IPaymentService
    {
        public Task<Response<PaymentDTO>> CreateAsync(PaymentDTO dto);
        public Task<Response<PaymentDTO>> GetOneAsync(Guid id);
        public Task<Response<List<PaymentDTO>>> GetCompleteListAsync();
    }
}
