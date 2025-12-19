using PersonalProyect.Data.Abstractions;
using PersonalProyect.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class Sale : IId
    {
        [Key]
        public Guid Id { get; set; }
        public required DateTime SaleDate { get; set; } 
        public required decimal TotalAmount { get; set; }
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pendiente;
        public SaleType SaleType { get; set; } = SaleType.Contado;
        public required Guid UserId { get; set; }
        public Guid? CustomerId { get; set; }
        public long SaleNumber { get; private set; }

        // Relationships

        public List<SaleDetail> SalesDetails { get; set; } = new();
        public User? Users { get; set; } 
        public Customer? Customers { get; set; }
    }
}
