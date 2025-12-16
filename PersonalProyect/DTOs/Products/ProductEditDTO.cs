namespace PersonalProyect.DTOs.Products
{
    public class ProductEditDTO
    {
        public string? ProductName { get; set; }
        public string? ProductDescription { get; set; }
        public string? Barcode { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? StockMin { get; set; }
        public Guid? CategoryId {  get; set; }
        public Guid? BrandId { get; set; }
    }
}
