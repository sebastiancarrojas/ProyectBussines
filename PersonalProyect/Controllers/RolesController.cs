using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;
using PersonalProyect.Core;
using PersonalProyect.Core.Attributes;

namespace PersonalProyect.Controllers
{
    public class RolesController : Controller
    {
        // -------------------------------------------------------------------
        // ------------------ Inyección de dependencias  ---------------------
        // -------------------------------------------------------------------

        private readonly IRoleService _roleService;
        private readonly INotyfService _notyfService;

        public RolesController(IRoleService rolesService, INotyfService notyfService)
        {
            _roleService = rolesService;
            _notyfService = notyfService;
        }

        // -------------------------------------------------------------------
        // ----------- Renderiza la vista principal de roles -----------------
        // -------------------------------------------------------------------

        [HttpGet]
        [CustomAuthorize(permission: "showRoles", module: "Roles")]
        public async Task<IActionResult> Index()
        {
            var response = await _roleService.GetAllAsync();

            if (!response.IsSuccess)
            {
                TempData["Error"] = response.Message;
                return RedirectToAction("Index", "Home");
            }
            return View(response.Result);
        }

        // -------------------------------------------------------------------
        // ------------------------ CREATE ROLES -----------------------------
        // -------------------------------------------------------------------

        [HttpGet]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create()
        {
            var permissionsResponse = await _roleService.GetPermissionsAsync();

            if (!permissionsResponse.IsSuccess)
            {
                _notyfService.Error(permissionsResponse.Message);
                return RedirectToAction(nameof(Index));
            }
            var dto = new RoleDTO
            {
                Permissions = permissionsResponse.Result
            };

            return View(dto);
        }

        [HttpPost]
        [CustomAuthorize(permission: "createRoles", module: "Roles")]
        public async Task<IActionResult> Create(RoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validación");
                var permissionsResponse = await _roleService.GetPermissionsAsync();
                if (!permissionsResponse.IsSuccess)
                {
                    _notyfService.Error(permissionsResponse.Message);
                    return RedirectToAction(nameof(Index));
                }
                dto.Permissions = permissionsResponse.Result;
                return View(dto);
            }
            var createResponse = await _roleService.CreateAsync(dto);
            if (createResponse.IsSuccess)
            {
                _notyfService.Success(createResponse.Message);
                return RedirectToAction(nameof(Index));
            }
            _notyfService.Error(createResponse.Message);

            // Volver a cargar permisos
            var permissionsResponse2 = await _roleService.GetPermissionsAsync();
            if (!permissionsResponse2.IsSuccess)
            {
                _notyfService.Error(permissionsResponse2.Message);
                return RedirectToAction(nameof(Index));
            }
            dto.Permissions = permissionsResponse2.Result;
            return View(dto);
        }

        // -------------------------------------------------------------------
        // ------------------------ DELETE ROLES -----------------------------
        // -------------------------------------------------------------------

        [HttpPost]
        [CustomAuthorize("deleteRoles", "Roles")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            Response<object> response = await _roleService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
            }
            else
            {
                _notyfService.Success(response.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        // --------------------------------------------------
        // ---------------- EDITAR ROLES --------------------
        // --------------------------------------------------

        [HttpGet]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(Guid id)
        {
            Response<RoleDTO> response = await _roleService.GetOneAsync(id);
            if (!response.IsSuccess)
            {
                _notyfService.Error(response.Message);
                return RedirectToAction(nameof(Index));
            }

            return View(response.Result);
        }

        [HttpPost]
        [CustomAuthorize(permission: "updateRoles", module: "Roles")]
        public async Task<IActionResult> Edit(RoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                _notyfService.Error("Debe ajustar los errores de validación");
                var permissionsResponse = await _roleService.GetPermissionsAsync();
                if (!permissionsResponse.IsSuccess)
                {
                    _notyfService.Error(permissionsResponse.Message);
                    return RedirectToAction(nameof(Index));
                }
                dto.Permissions = permissionsResponse.Result;
                return View(dto);
            }
            var updateResponse = await _roleService.EditAsync(dto);
            if (updateResponse.IsSuccess)
            {
                _notyfService.Success(updateResponse.Message);
                return RedirectToAction(nameof(Index));
            }
            _notyfService.Error(updateResponse.Message);
            var permissionsResponse2 = await _roleService.GetPermissionsAsync();
            if (!permissionsResponse2.IsSuccess)
            {
                _notyfService.Error(permissionsResponse2.Message);
                return RedirectToAction(nameof(Index));
            }
            dto.Permissions = permissionsResponse2.Result;
            return View(dto);
        }

    }
}


