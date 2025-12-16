using Microsoft.AspNetCore.Mvc;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Services.Implementations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ApiController
    {
        // -------------------------------
        // -- Inyeccion de dependencias --
        // -------------------------------

        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // -----------------
        // -- Crear marca --
        // -----------------

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BrandCreateDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _brandService.CreateAsync(dto);
            return Ok(result);
        }

        // ---------------------
        // -- Lista de marcas --
        // ---------------------

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _brandService.GetCompleteListAsync();
            return ControllerBasicValidation(response, ModelState);
        }
    }
}
