using PersonalProyect.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class SaleDetailDTO
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

    }
}
