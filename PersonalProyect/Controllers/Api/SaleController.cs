using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Controllers.Api;
using PersonalProyect.Core;
using PersonalProyect.Core.Pagination;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs.Sales;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Services.Implementations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize]
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
        public async Task<IActionResult> Create([FromBody] CreateSaleDTO dto)
        {
            // Obtener userId del usuario actual desde el claim correcto
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim))
                return Unauthorized(new { message = "Usuario no autenticado" });

            if (!Guid.TryParse(userIdClaim, out var userId))
                return BadRequest(new { message = "UserId inválido en el token" });

            // Llamar al servicio
            Response<Guid> response = await _saleService.CreateAsync(dto, userId);

            if (!response.IsSuccess)
            {
                return BadRequest(new
                {
                    message = response.Message,
                    errors = response.Errors // 👈 AQUÍ viene el error real de EF
                });
            }


            return Ok(new { saleId = response.Result, message = response.Message });
        }

        // ----------------
        // -- Paginación --
        // ----------------

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] SalePaginationRequest request)
        {
            var response = await _saleService.GetPaginatedListAsync(request);
            return ControllerBasicValidation(response, ModelState);
        }

        [HttpGet("debug")]
        public IActionResult DebugUser()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            var isAuth = User.Identity?.IsAuthenticated ?? false;
            return Ok(new { IsAuthenticated = isAuth, Claims = claims });
        }

    }
}
