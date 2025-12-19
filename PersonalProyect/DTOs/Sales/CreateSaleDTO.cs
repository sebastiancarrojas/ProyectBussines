namespace PersonalProyect.DTOs.Sales
{
    public class CreateSaleDTO
    {
        public Guid CustomerId { get; set; }
        public List<CreateSaleDetailDTO> Details { get; set; } = new();
    }
}
