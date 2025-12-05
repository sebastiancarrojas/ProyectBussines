using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Customer : IId
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(100)]
        public string? FullName { get; set; }
        [MaxLength(50)]
        public string? NickName { get; set; }
        [MaxLength(10)]
        public string? PhoneNumber { get; set; }
        [MaxLength(100)]
        public string? Address { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
        [MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        // Relationships

        public List<Sale> Sales { get; set; } = new();
    }
}
