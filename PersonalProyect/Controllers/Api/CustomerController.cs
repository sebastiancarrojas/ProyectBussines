using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.DTOs;

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
        public async Task<IActionResult> Create([FromBody] CustomerDTO dto)
        {
            var response = await _customerService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CustomerDTO dto)
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
