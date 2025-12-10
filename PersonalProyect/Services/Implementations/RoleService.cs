using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using PersonalProyect.Core;
using PersonalProyect.Data;
using PersonalProyect.Data.Entities;
using PersonalProyect.DTOs;
using PersonalProyect.Services.Abtractions;
using System;
using System.Reflection.Metadata.Ecma335;


namespace PersonalProyect.Services.Implementations
{
    public class RoleService : CustomQueryableOperationsService, IRoleService
    {
        // Inyectar dependencias necesarias
        private readonly DataContext _Data;
        private readonly IMapper _mapper;
        public RoleService(DataContext dataContext, IMapper mapper) : base(dataContext, mapper)
        {
            _Data = dataContext;
            _mapper = mapper;
        }
        public async Task<Response<object>> DeleteAsync(Guid id)
        {
            if (_Data.Users.Any(u => u.ProjectRoleId == id))
            {
                return Response<object>.Failure("No puede eliminar el rol ya que existen usuarios que lo contienen");
            }

            return await DeleteAsync<Role>(id);
        }

        
        public async Task<Response<List<RoleDTO>>> GetAllAsync()
        {
            try
            {
                // Obtener todos los roles
                var roles = await _Data.Roles.ToListAsync();

                // Mapear a DTO
                var rolesDTO = _mapper.Map<List<RoleDTO>>(roles);

                return Response<List<RoleDTO>>.Success(rolesDTO, "Lista de roles obtenida con éxito");
            }
            catch (Exception ex)
            {
                return Response<List<RoleDTO>>.Failure(ex);
            }
        }

        public async Task<Response<List<PermissionsForRoleDTO>>> GetPermissionsAsync()
        {
            Response<List<PermissionDTO>> permissionsResponse = await GetCompleteListAsync<Permission, PermissionDTO>();

            if (!permissionsResponse.IsSuccess)
            {
                return Response<List<PermissionsForRoleDTO>>.Failure(permissionsResponse.Message);
            }

            List<PermissionsForRoleDTO> dto = permissionsResponse.Result.Select(p => new PermissionsForRoleDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Module = p.Module,
                Selected = false
            }).ToList();

            return Response<List<PermissionsForRoleDTO>>.Success(dto);
        }

        public async Task<Response<RoleDTO>> EditAsync(RoleDTO dto)
        {
            try
            {
                // Validación si deseas bloquear edición de algún rol especial
                if (dto.Name == Env.SUPER_ADMIN_ROLE_NAME)
                {
                    return Response<RoleDTO>.Failure($"El rol '{Env.SUPER_ADMIN_ROLE_NAME}' no puede ser editado");
                }

                // Mapear DTO → Entidad
                Role role = _mapper.Map<Role>(dto);
                _Data.Roles.Update(role);

                // -----------------------------------------
                //      PERMISSIONS (SIN SECCIONES)
                // -----------------------------------------

                List<Guid> permissionIds = new();

                if (!string.IsNullOrEmpty(dto.PermissionIds))
                {
                    permissionIds = JsonConvert.DeserializeObject<List<Guid>>(dto.PermissionIds);
                }

                // Eliminar permisos antiguos del rol
                List<RolePermission> oldRolePermissions =
                    await _Data.RolePermissions
                        .Where(rp => rp.RoleId == dto.Id)
                        .ToListAsync();

                _Data.RolePermissions.RemoveRange(oldRolePermissions);

                // Crear nuevos permisos
                foreach (Guid permissionId in permissionIds)
                {
                    RolePermission rolePermission = new RolePermission
                    {
                        RoleId = role.Id,
                        PermissionId = permissionId
                    };

                    await _Data.RolePermissions.AddAsync(rolePermission);
                }

                // Guardar cambios
                await _Data.SaveChangesAsync();

                return Response<RoleDTO>.Success(dto, "Rol actualizado con éxito");
            }
            catch (Exception ex)
            {
                return Response<RoleDTO>.Failure(ex);
            }
        }

        public async Task<Response<RoleDTO>> GetOneAsync(Guid id)
        {
            try
            {
                Role role = await _Data.Roles
                    .Include(r => r.RolePermissions) // Traer permisos asignados
                    .FirstOrDefaultAsync(r => r.Id == id);

                if (role == null)
                    return Response<RoleDTO>.Failure("Rol no encontrado");

                // Mapear Role → RoleDTO
                RoleDTO dto = _mapper.Map<RoleDTO>(role);

                // Llenar PermissionIds como JSON
                dto.PermissionIds = JsonConvert.SerializeObject(
                    role.RolePermissions.Select(rp => rp.PermissionId).ToList()
                );

                // Opcional: llenar también la lista de todos los permisos para mostrar checkboxes
                dto.Permissions = await _Data.Permissions
                    .Select(p => new PermissionsForRoleDTO
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Module = p.Module
                    }).ToListAsync();

                return Response<RoleDTO>.Success(dto);
            }
            catch (Exception ex)
            {
                return Response<RoleDTO>.Failure(ex);
            }
        }


        // ---------------------------------------------
        // --------------- CREATE ROLES ----------------
        // ---------------------------------------------
        public async Task<Response<RoleDTO>> CreateAsync(RoleDTO dto)
        {
            using (IDbContextTransaction transaction = await _Data.Database.BeginTransactionAsync())
            {
                try
                {
                    // Crear el rol desde el DTO
                    Role role = _mapper.Map<Role>(dto);
                    role.Id = Guid.NewGuid();

                    await _Data.Roles.AddAsync(role);
                    await _Data.SaveChangesAsync();

                    // Permisos seleccionados (si vienen en JSON)
                    List<Guid> permissionIds = new();

                    if (!string.IsNullOrEmpty(dto.PermissionIds))
                    {
                        permissionIds = JsonConvert.DeserializeObject<List<Guid>>(dto.PermissionIds);
                    }

                    // Crear RolePermission por cada permiso seleccionado
                    foreach (Guid permissionId in permissionIds)
                    {
                        RolePermission rolePermission = new RolePermission
                        {
                            RoleId = role.Id,
                            PermissionId = permissionId
                        };

                        await _Data.RolePermissions.AddAsync(rolePermission);
                    }

                    await _Data.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Response<RoleDTO>.Success(dto, "Rol creado con éxito");
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Response<RoleDTO>.Failure(ex);
                }
            }
        }

    }

}
