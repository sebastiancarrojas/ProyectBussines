namespace PersonalProyect.DTOs.Brands
{
    public class BrandDTO
    {
        public Guid Id { get; set; }
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public string Status { get; set; } = "Activo";
    }
}
