using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class SaleDetail : IId
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
        public Guid SaleId { get; set; }
        public Guid? ProductId { get; set; }
        
        // Temporales

        public string? ProductName { get; set; }
        public bool IsTemporary { get; set; }

        // Relationships

        public Product? Products { get; set; }
        public Sale? Sales { get; set; }
    }
}
