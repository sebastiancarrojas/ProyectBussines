using PersonalProyect.Data.Abstractions;
using PersonalProyect.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Customer : IId
    {
        [Key]
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActived { get; set; }
        public DocumentType? DocumentType { get; set; }
        public string? DocumentNumber { get; set; }

        // Relationships

        public List<Sale> Sales { get; set; } = new();
    }
}
