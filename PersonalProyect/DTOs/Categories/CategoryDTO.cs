using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs.Categories
{
    public class CategoryDTO
    {
        [Key]
        public Guid Id { get; set; }
        public required string CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public required string Status { get; set; } = "Activo";
    }
}
