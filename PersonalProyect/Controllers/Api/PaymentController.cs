using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PersonalProyect.Core;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ApiController
    {
        // Inyectar dependencias necesarias
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        // Create Payment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PaymentDTO dto)
        {
            Response<PaymentDTO> response = await _paymentService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }
        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var response = await _paymentService.GetOneAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _paymentService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
