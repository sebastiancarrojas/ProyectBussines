using Microsoft.EntityFrameworkCore;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;


namespace PersonalProyect.Data.Seeders
{
    public class PermissionsSeeder
    {
        private readonly DataContext _context;

        public PermissionsSeeder(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Permission> permissions = [.. Products(), .. Roles()];

            foreach (Permission permission in permissions)
            {
                bool exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name);

                if (!exists)
                {
                    await _context.Permissions.AddAsync(permission);
                }
            }

            await _context.SaveChangesAsync();
        }

        private List<Permission> Products()
        {
            return new List<Permission>
            {
                new Permission { Id = Guid.NewGuid(), Name = "showProducts", Description = "Ver Productos", Module = "Products"},
                new Permission { Id = Guid.NewGuid(), Name = "createProducts", Description = "Crear Productos", Module = "Products"},
                new Permission { Id = Guid.NewGuid(), Name = "updateProducts", Description = "Editar Productos", Module = "Products"},
                new Permission { Id = Guid.NewGuid(), Name = "deleteProducts", Description = "Eliminar Productos", Module = "Products"},
            };
        }
        private List<Permission> Roles()
        {
            return new List<Permission>
            {
                new Permission { Id = Guid.NewGuid(), Name = "showRoles", Description = "Ver Roles", Module = "Roles"},
                new Permission { Id = Guid.NewGuid(), Name = "createRoles", Description = "Crear Roles", Module = "Roles"},
                new Permission { Id = Guid.NewGuid(), Name = "updateRoles", Description = "Editar Roles", Module = "Roles"},
                new Permission { Id = Guid.NewGuid(), Name = "deleteRoles", Description = "Eliminar Roles", Module = "Roles"},
            };
        }
    }
}