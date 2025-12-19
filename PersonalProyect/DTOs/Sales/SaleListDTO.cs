using PersonalProyect.Data.Enum;

namespace PersonalProyect.DTOs.Sales
{
    public class SaleListDTO
    {
        public Guid? Id { get; set; }
        public DateTime SaleDate { get; set; }
        public string SaleDateOnly { get; set; }
        public string SaleTimeOnly {  get; set; }
        public decimal? TotalAmount { get; set; }
        public string? PaymentStatus { get; set; } 
        public string? SaleType { get; set; }
        public string? UserName { get; set; }
        public string? CustomerName { get; set; }
        public string? SaleNumber { get; set; }

    }
}
