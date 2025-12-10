using PersonalProyect.Data.Abstractions;

namespace PersonalProyect.Data.Entities
{
    public class RolePermission : IId
    {
        public Guid Id { get; set; }
        public required Guid RoleId { get; set; }
        public required Guid PermissionId { get; set; }

        public Role? Role { get; set; }
        public Permission? Permission { get; set; }
    }
}
