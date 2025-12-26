using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.DTOs.Customers;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ApiController
    {
        // Inyectar dependencias necesarias
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("quick")]
        public async Task<IActionResult> CreateQuickCustomer([FromBody] QuickCreateCustomerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _customerService.CreateQuickCustomerAsync(dto);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByDocument([FromQuery] string document)
        {
            var response = await _customerService.SearchByDocumentAsync(document);

            if (!response.IsSuccess)
                return BadRequest(response);

            return Ok(response);
        }



        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var response = await _customerService.GetOneAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _customerService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCustomerDTO dto)
        {
            var response = await _customerService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateCustomerDTO dto)
        {
            var response = await _customerService.UpdateAsync(id, dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _customerService.DeleteAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
