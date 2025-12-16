using PersonalProyect.Data.Abstractions;

namespace PersonalProyect.Data.Entities
{
    public class Supplier : IId
    {
        public Guid Id { get; set; }
        public required string SupplierName { get; set; }
        public string? SupplierDescription { get; set; }
        public string? TaxID { get; set; }
        public string? SupplierPhone { get; set; }
        public string? SupplierEmail { get; set; }
        public string? SupplierAddress { get; set; }
        public required string Status { get; set; }

        // Relationships

        public List<ProductSupplier> ProductSuppliers { get; set; } = new();
    }
}
