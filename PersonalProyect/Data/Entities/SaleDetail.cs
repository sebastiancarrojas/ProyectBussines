using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class SaleDetail : IId
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal SubTotal { get; set; }
        [Required]

        // Relationships
        public Guid SaleId { get; set; }
        [Required]
        public Guid ProductId { get; set; }

        public Product? Products { get; set; }
        public Sale? Sales { get; set; }
    }
}
