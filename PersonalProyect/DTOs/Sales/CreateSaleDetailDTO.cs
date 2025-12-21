namespace PersonalProyect.DTOs.Sales
{
    public class CreateSaleDetailDTO
    {
        public Guid? ProductId { get; set; }      // null si es temporal
        public string? ProductName { get; set; }  // solo para temporales

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }    // necesario

        public bool IsTemporary { get; set; }
    }
}
