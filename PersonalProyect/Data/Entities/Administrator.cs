using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Administrator : IId
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string? FullName { get; set; } 
        [Required]
        public string? NickName { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // Relationships

        public List<Sale> Sales { get; set; } = new();

    }
}
