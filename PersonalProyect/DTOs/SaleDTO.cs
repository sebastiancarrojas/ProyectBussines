using PersonalProyect.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class SaleDTO
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public DateTime SaleDate { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        public decimal OutstandingBalance { get; set; }
        [Required]
        public string? Status { get; set; }
        [Required]
        public string? SaleType { get; set; }
        public decimal TotalPaid { get; set; }

        // Relationships

        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public Guid AdministratorId { get; set; }

    }
}
