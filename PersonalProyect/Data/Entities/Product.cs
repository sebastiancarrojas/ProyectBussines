using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{ 
    public class Product : IId
    {
        [Key]
        public Guid Id { get; set; }
        public required string ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? ProductDescription { get; set; }
        public decimal UnitPrice { get; set; }
        public int? StockMin { get; set; }
        public int? CurrentStock { get; set; }
        public string? UnitOfMeasure { get; set; }
        public required DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; } = DateTime.Now;
        public string? ImageUrl { get; set; }
        public required string Status { get; set; }

        // Foreign Keys

        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }

        // public Guid CreatedByUserId { get; set; }


        // Relationships
        public List<SaleDetail> SalesDetails { get; set; } = new();
        public Category? Categories { get; set; }
        public Brand? Brands { get; set; }
        public List<ProductSupplier> ProductSuppliers { get; set; } = new();

    }
}
