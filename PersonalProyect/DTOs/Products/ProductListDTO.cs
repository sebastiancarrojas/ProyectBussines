using Microsoft.Identity.Client;

namespace PersonalProyect.DTOs.Products
{
    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string? Barcode { get; set; }
        public string? CategoryName { get; set; } 
        public string? BrandName { get; set; }
        public int? CurrentStock { get; set; }
        public string? Status { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? StockMin { get; set; }
        public string? ProductDescription { get; set; }
    }
}
