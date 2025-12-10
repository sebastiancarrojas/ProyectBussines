using PersonalProyect.Core;
using PersonalProyect.DTOs;

namespace PersonalProyect.Services.Abtractions
{
    public interface IRoleService
    {
        public Task<Response<RoleDTO>> CreateAsync(RoleDTO dto);
        public Task<Response<object>> DeleteAsync(Guid id);
        public Task<Response<RoleDTO>> EditAsync(RoleDTO dto);
        public Task<Response<RoleDTO>> GetOneAsync(Guid id);
        public Task<Response<List<PermissionsForRoleDTO>>> GetPermissionsAsync();
        Task<Response<List<RoleDTO>>> GetAllAsync();


    }
}
