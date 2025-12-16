using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class BrandCreateDTO
    {
        [Required]
        public string? BrandName { get; set; }
        public string? BrandDescription { get; set; }
        public string Status { get; set; } = "Activo";
    }
}
