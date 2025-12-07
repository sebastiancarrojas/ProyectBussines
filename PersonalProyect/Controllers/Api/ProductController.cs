using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Controllers.Api;
using PersonalProyect.Core;
using PersonalProyect.Data;
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
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, DataContext context, IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _mapper = mapper;
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
        public async Task<IActionResult> Create([FromForm] ProductDTO dto)
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
