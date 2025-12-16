using PersonalProyect.Data.Abstractions;

namespace PersonalProyect.Data.Entities
{
    public class Category : IId
    {
        public Guid Id { get; set; }
        public required string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public required string Status { get; set; }

        // Relationships

        public List<Product> Products { get; set; } = new();
    }
}
