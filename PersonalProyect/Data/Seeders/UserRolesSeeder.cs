using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Data.Seeders
{
    public class UserRolesSeeder
    {
        private readonly DataContext _context;
        private readonly IUserService _userService;
       
        private const string BASIC_ROLE_NAME = "Basic";

        // Inyectar dependencias necesarias
        public UserRolesSeeder(DataContext context, IUserService usersService)
        {
            _context = context;
            _userService = usersService;
        }

        public async Task SeedAsync()
        {
            await CheckRolesAsync();
            await CheckUsersAsync();
        }

        private async Task CheckRolesAsync()
        {
            await AdminRoleAsync();
            await BasicRoleAsync();
      
        }

        private async Task CheckUsersAsync()
        {
            // Admin
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "sebastian1108@gmail.com");

            if (user is null)
            {
                Role adminRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                user = new User
                {
                    Email = "sebastian1108@gmail.com",
                    FullName = "Sebastian Cardon Rojas",
                    UserName = "sebastian1108@gmail.com",
                    ProjectRoleId = adminRole!.Id
                };

                await _userService.AddUserAsync(user, "123456");

                string token = (await _userService.GenerateConfirmationTokenAsync(user)).Result;
                await _userService.ConfirmUserAsync(user, token);
            }

            // Basic
            user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == "kevincarrojas1108@gmail.com");

            if (user is null)
            {
                Role basicRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == BASIC_ROLE_NAME);

                user = new User
                {
                    Email = "kevincarrojas1108@gmail.com",
                    FullName = "Kevin Cardona Rojas",
                    PhoneNumber = "3200000000",
                    UserName = "kevincarrojas1108@gmail.com",
                    ProjectRoleId = basicRole!.Id
                };

                await _userService.AddUserAsync(user, "123456");

                string token = (await _userService.GenerateConfirmationTokenAsync(user)).Result;
                await _userService.ConfirmUserAsync(user, token);
            }
        }

        private async Task AdminRoleAsync()
        {
            bool exists = await _context.Roles
                .AnyAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

            if (!exists)
            {
                // Crear el rol admin
                var role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = Env.SUPER_ADMIN_ROLE_NAME
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                // Obtener TODOS los permisos
                List<Permission> permissions = await _context.Permissions.ToListAsync();

                // Asignar cada permiso al rol
                foreach (var permission in permissions)
                {
                    await _context.RolePermissions.AddAsync(
                        new RolePermission
                        {
                            PermissionId = permission.Id,
                            RoleId = role.Id
                        }
                    );
                }

                await _context.SaveChangesAsync();
            }
            else
            {
                // Si el rol ya existe, asegurar que tenga todos los permisos
                var role = await _context.Roles
                    .FirstAsync(r => r.Name == Env.SUPER_ADMIN_ROLE_NAME);

                var existingPermissions = await _context.RolePermissions
                    .Where(rp => rp.RoleId == role.Id)
                    .Select(rp => rp.PermissionId)
                    .ToListAsync();

                var allPermissions = await _context.Permissions.ToListAsync();

                var missingPermissions = allPermissions
                    .Where(p => !existingPermissions.Contains(p.Id))
                    .ToList();

                if (missingPermissions.Any())
                {
                    foreach (var p in missingPermissions)
                    {
                        await _context.RolePermissions.AddAsync(
                            new RolePermission
                            {
                                PermissionId = p.Id,
                                RoleId = role.Id
                            }
                        );
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }


        private async Task BasicRoleAsync()
        {
            bool exists = await _context.Roles
                .AnyAsync(r => r.Name == BASIC_ROLE_NAME);

            Role role;

            if (!exists)
            {
                // Crear rol Basic
                role = new Role
                {
                    Id = Guid.NewGuid(),
                    Name = BASIC_ROLE_NAME
                };

                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
            }
            else
            {
                role = await _context.Roles
                    .FirstAsync(r => r.Name == BASIC_ROLE_NAME);
            }

            // Obtener permisos SOLO del módulo Productos
            List<Permission> productPermissions = await _context.Permissions
                .Where(p => p.Module == "Products")
                .ToListAsync();

            // Obtener permisos actuales del rol
            List<Guid> existingPermissions = await _context.RolePermissions
                .Where(rp => rp.RoleId == role.Id)
                .Select(rp => rp.PermissionId)
                .ToListAsync();

            // Filtrar permisos faltantes
            var missing = productPermissions
                .Where(p => !existingPermissions.Contains(p.Id))
                .ToList();

            if (missing.Any())
            {
                foreach (var permission in missing)
                {
                    await _context.RolePermissions.AddAsync(
                        new RolePermission
                        {
                            PermissionId = permission.Id,
                            RoleId = role.Id
                        }
                    );
                }

                await _context.SaveChangesAsync();
            }
        }


    }
}