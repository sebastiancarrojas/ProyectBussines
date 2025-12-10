using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Role : IId
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }
        public ICollection<RolePermission>? RolePermissions { get; set; }

    }
}
