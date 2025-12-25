namespace PersonalProyect.DTOs.SalesDetails
{
    public class SaleDetailModalDTO
    {
        public Guid? Id { get; set; }
        public string? ProductName { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? SubTotal { get; set; }
        public bool? IsTemporary { get; set; }
    }
}
