using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Controllers
{
    public class AdminUsersController : Controller
    {
        // Inyectar dependencias necesarias
        private readonly IUserService _userService;
        public AdminUsersController(IUserService userService)
        {
            _userService = userService;
        }

        // Acción para listar todos los usuarios
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }
    }
}
