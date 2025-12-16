using Microsoft.AspNetCore.Mvc;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Services.Implementations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ApiController
    {
        // --------------------------------------
        // -- Inyectar dependencias necesarias --
        // --------------------------------------
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // ---------------------
        // -- Crear categoria --
        // ---------------------

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _categoryService.CreateAsync(dto);
            return Ok(result);
        }

        // -------------------------
        // -- Lista de categorias --
        // -------------------------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _categoryService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
