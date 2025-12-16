using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class CategoryCreateDTO
    {
        [Required]
        public string CategoryName { get; set; } = string.Empty;

        public string? CategoryDescription { get; set; }

        public string Status { get; set; } = "Activo";
    }
}
