using PersonalProyect.Data.Abstractions;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{ 
    public class Product : IId
    {
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? Status { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string? Code { get; set; }

    // Relationships
    public List<SaleDetail> SalesDetails { get; set; } = new();

    }
}
