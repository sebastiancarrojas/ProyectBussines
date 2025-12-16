using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Controllers.Api;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Services.Implementations;
using PersonalProyect.Core.Pagination;
using PersonalProyect.DTOs.Products;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ApiController
    {

        // --------------------------------------
        // -- Inyectar dependencias necesarias --
        // --------------------------------------

        private readonly IProductService _productService;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public ProductController(IProductService productService, DataContext context, IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }

        // --------------------
        // -- Crear producto --
        // --------------------

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDTO dto)
        {
            var response = await _productService.CreateAsync(dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // --------------------------------
        // -- Obtener un producto por Id --
        // --------------------------------

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            Response<ProductCreateDTO> response = await _productService.GetOneAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        // --------------------------------
        // -- Obtener lista de productos -- 
        // --------------------------------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response<List<ProductCreateDTO>> response = await _productService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }

        // ---------------------
        // -- Editar producto --
        // ---------------------

        [HttpPatch("{id}")]

        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] ProductEditDTO dto)
        {
            Response<ProductEditDTO> response = await _productService.UpdateAsync(id, dto);
            return ControllerBasicValidation(response, ModelState);
        }

        // -------------------------
        // -- Desactivar producto --
        // -------------------------

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate([FromRoute] Guid id)
        {
            Response<object> response = await _productService.DeactivateAsync(id);
            return ControllerBasicValidation(response, ModelState);
        }

        // ----------------
        // -- Paginación --
        // ----------------

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] ProductPaginationRequest request)
        {
            var response = await _productService.GetPaginatedListAsync(request);
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
