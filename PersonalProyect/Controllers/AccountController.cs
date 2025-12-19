using AspNetCoreHero.ToastNotification.Abstractions;    
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using PersonalProyect.Data;
using PersonalProyect.Services.Abtractions;
using AutoMapper;
using PersonalProyect.DTOs;
using PersonalProyect.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using PersonalProyect.Data.Entities;

namespace PersonalProyect.Controllers
{
    public class AccountController : Controller
    {
        // Inyectar dependencias necesarias

        private readonly DataContext _context;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly INotyfService _notyfService;

        public AccountController(DataContext context, IUserService userService, IMapper mapper, INotyfService notyfService)
        {
            _context = context;
            _userService = userService;
            _mapper = mapper;
            _notyfService = notyfService;
        }

        // Renderiza la vista de Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        // Este método devuelve un JSON con información del usuario actual
        // Mostrando si está autenticado, su nombre y todos los claims asociados
        [HttpGet]
        [AllowAnonymous]
        public IActionResult DebugClaims()
        {
            var identity = HttpContext.User.Identity;
            var IsAuth = identity?.IsAuthenticated ?? false;
            var name = identity?.Name;
            var claims = HttpContext.User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            return Json(new { IsAuthenticated = IsAuth, Name = name, Claims = claims });
        }

        // Renderiza la vista de Signup
        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        // Maneja el proceso de registro de usuario
        [HttpPost]
        public async Task<IActionResult> Signup(AccountUserDTO dto)
        {
            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio de usuario para registrar
                Response<IdentityResult> result = await _userService.SignupAsync(dto);

                // Si el registro es exitoso, muestra un mensaje de éxito y redirige al login
                if (result.IsSuccess)
                {
                    _notyfService.Success(result.Message);
                    return RedirectToAction(nameof(Login));
                }

                // Si el registro falla, muestra los errores específicos
                else
                {
                    _notyfService.Error(result.Message);
                }
            }
            // Si el modelo no es válido o el registro falla, vuelve a mostrar la vista con el DTO
            return View(dto);
        }


        // Maneja el proceso de logout
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Llama al servicio de usuario para cerrar sesión
            await _userService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        // Renderiza la vista para actualizar la información del usuario
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateUser()
        {
            // Obtiene el usuario actual usando su email
            User user = await _userService.GetUserByEmailAsync(User.Identity.Name);

            // Verifica si el usuario existe
            if (user is null)
            {
                // Si no existe, retorna un error 404
                return NotFound();
            }

            // Mapea el usuario a un DTO y lo pasa a la vista
            return View(_mapper.Map<AccountUserDTO>(user));
        }

        // Maneja el proceso de actualización de la información del usuario
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateUser(AccountUserDTO dto)
        {
            // Elimina las validaciones de Password y ConfirmPassword
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");

            // Verifica si el modelo es válido
            if (ModelState.IsValid)
            {
                // Llama al servicio de usuario para actualizar
                Response<AccountUserDTO> result = await _userService.UpdateUserAsync(dto);

                // Muestra un mensaje basado en el resultado de la actualización
                if (result.IsSuccess)
                {
                    _notyfService.Success(result.Message);
                }
                // Si la actualización falla, muestra un mensaje de error
                else
                {
                    _notyfService.Error(result.Message);
                }

                return RedirectToAction("Index", "Home");
            }

            // Si el modelo no es válido, muestra un mensaje de error y vuelve a mostrar la vista con el DTO
            _notyfService.Error("Debe ajustar lo errores de validación");
            return View(dto);
        }
    }
}
