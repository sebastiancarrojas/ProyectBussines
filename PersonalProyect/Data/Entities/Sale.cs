using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Sale : IId
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
        public List<SaleDetail> SalesDetails { get; set; } = new();
        public Administrator Administrators { get; set; } = new();
        public Customer Customers { get; set; } = new();
        public List<Payment> Payments { get; set; } = new();
    }
}
