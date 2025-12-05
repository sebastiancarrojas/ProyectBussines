using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Payment : IId
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime PaymentDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public string? Note { get; set; }

        // Relationships

        [Required]
        public Guid SaleId { get; set; }
        public Sale Sales { get; set; } = new();
    }
}
