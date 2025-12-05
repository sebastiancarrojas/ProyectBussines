using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleDetailController : ApiController
    {
        // Inyertar dependencias necesarias
        private readonly ISaleDetail _saleDetailService;
        public SaleDetailController(ISaleDetail saleDetailService)
        {
            _saleDetailService = saleDetailService;
        }

        // Create SaleDetail
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DTOs.SaleDetailDTO dto)
        {
            var response = await _saleDetailService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // Update SaleDetail
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] DTOs.SaleDetailDTO dto)
        {
            var response = await _saleDetailService.UpdateAsync(id, dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // Delete SaleDetail
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var response = await _saleDetailService.DeleteAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var response = await _saleDetailService.GetOneAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _saleDetailService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
