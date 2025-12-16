using PersonalProyect.Data.Abstractions;

namespace PersonalProyect.Data.Entities
{
    public class Brand : IId
    {
        public Guid Id { get; set; }
        public required string BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public required string Status { get; set; }
        // Relationships
        public List<Product> Products { get; set; } = new();
    }
}
