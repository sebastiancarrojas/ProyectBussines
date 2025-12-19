using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class SaleDetail : IId
    {
        [Key]
        public Guid Id { get; set; }
        public required int Quantity { get; set; }
        public required decimal UnitPrice { get; set; }
        public required decimal SubTotal { get; set; }
        public required Guid SaleId { get; set; }
        public required Guid ProductId { get; set; }

        // Relationships

        public Product? Products { get; set; }
        public Sale? Sales { get; set; }
    }
}
