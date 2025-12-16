namespace PersonalProyect.Core.Pagination
{
    public class ProductPaginationRequest : PaginationRequest
    {
        public Guid? CategoryId { get; set; }
        public Guid? BrandId { get; set; }
        public int? StockMin { get; set; }
        public int? StockMax { get; set; }
        public string? Status { get; set; }
    }

}
