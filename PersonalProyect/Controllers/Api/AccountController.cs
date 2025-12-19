
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _userService.LoginWithTokenAsync(dto);

            if (!response.IsSuccess)
                return Unauthorized(new { message = response.Message });

            // Devuelve el token en JSON
            return Ok(new { token = response.Result, message = response.Message });
        }
    }
}

