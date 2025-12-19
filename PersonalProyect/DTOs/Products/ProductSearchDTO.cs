namespace PersonalProyect.DTOs.Products
{
    public class ProductSearchDTO
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;
        public string Barcode { get; set; } = null!;
        public decimal? UnitPrice { get; set; }
        public int? CurrentStock { get; set; }
    }
}
