using PersonalProyect.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class PaymentDTO
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
    }
}
