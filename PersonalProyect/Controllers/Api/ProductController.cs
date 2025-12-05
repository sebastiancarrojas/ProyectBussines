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
    public class ProductController : ApiController
    {
        // Inyectar dependencias necesarias
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        // GET 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            Response<ProductDTO> response = await _productService.GetOneAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response<List<ProductDTO>> response = await _productService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDTO dto)
        {
            Response<ProductDTO> response = await _productService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductDTO dto)
        {
            Response<ProductDTO> response = await _productService.UpdateAsync(id, dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response<object> response = await _productService.DeleteAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

    }
}
