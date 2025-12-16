using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;
using System.Security.Claims;

namespace PersonalProyect.Services.Implementations
{
    public class UserService : IUserService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _Data;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(DataContext dataContext, UserManager<User> userManager, SignInManager<User> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            _Data = dataContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        // Crear un usuario nuevo usando ASP.NET Identity
        public async Task<Response<IdentityResult>> AddUserAsync(User user, string password)
        {
            // Usar UserManager para crear el usuario con la contraseña proporcionada
            IdentityResult result = await _userManager.CreateAsync(user, password);

            // Retornar una respuesta indicando el éxito o fracaso de la operación
            return new Response<IdentityResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };
        }

        // Confirmar el email del usuario usando un token
        // Indagar mas a profundiad (Identity)
        public async Task<Response<IdentityResult>> ConfirmUserAsync(User user, string token)
        {
            // Usar UserManager para confirmar el email del usuario con el token proporcionado
            IdentityResult result = await _userManager.ConfirmEmailAsync(user, token);
            // Retornar una respuesta indicando el éxito o fracaso de la operación
            return new Response<IdentityResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };
        }

        // Valida el Token de confirmación de email para el usuario proporcionado
        public async Task<Response<string>> GenerateConfirmationTokenAsync(User user)
        {
            // Usar UserManager para generar un token de confirmación de email para el usuario proporcionado
            string result = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            // Retornar una respuesta con el token generado
            return Response<string>.Success(result);
        }

        // Iniciar sesión de un usuario usando email y contraseña
        public async Task<Response<SignInResult>> LoginAsync(LoginDTO dto)
        {
            // Usar SignInManager para intentar iniciar sesión con el email y contraseña proporcionados
            SignInResult result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, false, false);
            // Retornar una respuesta indicando el éxito o fracaso de la operación
            return new Response<SignInResult>
            {
                Result = result,
                IsSuccess = result.Succeeded
            };
        }

        // Cerrar sesión del usuario actual
        public async Task LogoutAsync()
        {
            // Usar SignInManager para cerrar la sesión del usuario actual
            await _signInManager.SignOutAsync();
        }

        // Registrar un nuevo usuario usando los datos proporcionados en AccountUserDTO
        public async Task<Response<IdentityResult>> SignupAsync(AccountUserDTO dto)
        {
            // Crear una nueva instancia de User con los datos proporcionados
            User user = new User
            {
                // Id = Guid.NewGuid().ToString(),
                UserName = dto.Email,
                Email = dto.Email,
                EmailConfirmed = true, // Opcional: si quieres que se confirme automáticamente
                FullName = dto.FullName,
            };

            // Llamar a AddUserAsync para crear el usuario con la contraseña proporcionada
            return await AddUserAsync(user, dto.Password);
        }

        // Actualizar la información del usuario
        public async Task<Response<AccountUserDTO>> UpdateUserAsync(AccountUserDTO dto)
        {
            try
            {
                // Parametro dto debe contener el Id del usuario a actualizar
                User user = await GetUserAsync(dto.Id!);
                user.FullName = dto.FullName;

                // Actualizar el usuario en la base de datos
                _Data.Users.Update(user);

                // Guardar los cambios
                await _Data.SaveChangesAsync();

                // Retornar una respuesta exitosa con el DTO actualizado
                return Response<AccountUserDTO>.Success(dto, "Usuario actualizado correctamente");
            }
            catch (Exception ex)
            {
                // Retornar una respuesta de error en caso de excepción
                return Response<AccountUserDTO>.Failure(ex, "Error al actualizar el usuario");
            }
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _Data.Users
                              .Include(u => u.Role)
                              .FirstOrDefaultAsync(u => u.Email == email);
        }




        // Retornar el usuario por su Id
        public async Task<User> GetUserAsync(string id)
        {
            return await _Data.Users.FindAsync(id);
        }


        public bool CurrentUserIsAuthenticated()
        {
            ClaimsPrincipal? user = _httpContextAccessor.HttpContext?.User;
            return user?.Identity is not null && user.Identity.IsAuthenticated;
        }

        public async Task<bool> CurrentUserHasPermissionAsync(string permission, string module)
        {
            ClaimsPrincipal? claimsUser = _httpContextAccessor.HttpContext?.User;

            // Valida si hay sesión
            if (claimsUser is null)
            {
                return false;
            }

            string userName = claimsUser.Identity!.Name!;

            User? user = await GetUserByEmailAsync(userName);

            if (user is null)
            {
                return false;
            }

            if (user.Role.Name == Env.SUPER_ADMIN_ROLE_NAME)
            {
                return true;
            }

            return await _Data.RolePermissions
    .Include(rp => rp.Permission)
    .AnyAsync(rp => rp.RoleId == user.ProjectRoleId &&
                    rp.Permission.Module == module &&
                    rp.Permission.Name == permission);

        }

        // ---------------------------------------------------------
        // -------- List Users for Admin Layout NavBar -------------
        // ---------------------------------------------------------

        public async Task<List<UserListDTO>> GetAllUsersAsync()
        {
           return await _Data.Users.Include(u => u.Role).Select(u => new UserListDTO
                {
                    Id = u.Id.ToString(),
                    Email = u.Email,
                    FullName = u.FullName,
                    UserName = u.UserName,
                    RegistrationDate = u.RegistrationDate,
                    RoleName = u.Role.Name
                })
                .ToListAsync();
        }
    }
}
