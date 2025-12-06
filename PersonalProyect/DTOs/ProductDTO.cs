using PersonalProyect.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class ProductDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Status { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public string? Code { get; set; }

        // Archivo recibido desde el formulario
        public IFormFile? ImageFile { get; set; }

        // Ruta guardada y enviada a la BD
        public string? ImageUrl { get; set; }


        // Relationships
        public List<SaleDetail> SalesDetails { get; set; } = new();
    }
}
