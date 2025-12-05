using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Controllers.Api;
using PersonalProyect.Core;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ApiController
    {
        // Inyectar dependencias necesarias
        private readonly ISaleService _saleService;
        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        // Create Sale
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaleDTO dto)
        {
            Response<SaleDTO> response = await _saleService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }


        // GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var response = await _saleService.GetOneAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _saleService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
