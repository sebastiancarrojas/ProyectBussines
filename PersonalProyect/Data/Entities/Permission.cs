using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Permission : IId
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(50)]
        [Required]
        public required string Name { get; set; }

        [MaxLength(100)]
        [Required]
        public required string Description { get; set; }

        [MaxLength(32)]
        [Required]
        public required string Module { get; set; }

        public ICollection<RolePermission>? RolePermissions { get; set; }
    }
}
