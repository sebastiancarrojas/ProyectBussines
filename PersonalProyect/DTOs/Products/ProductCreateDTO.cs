using PersonalProyect.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs.Products
{
    public class ProductCreateDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? ProductDescription { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? StockMin { get; set; }
        public int? CurrentStock { get; set; }
        public string? UnitOfMeasure { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public string? ImageUrl { get; set; }
        public string? Status { get; set; } = "Activo";

        // Foreign Keys

        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; } 

    }
}
