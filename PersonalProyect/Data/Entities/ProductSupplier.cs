namespace PersonalProyect.Data.Entities
{
    public class ProductSupplier
    {
        public Guid SupplierId { get; set; }
        public Guid ProductId { get; set; }

        public Product Product { get; set; }
        public Supplier Supplier { get; set; }

        public required decimal CostPrice { get; set; }
        public bool IsPrimary { get; set; }
    }
}
