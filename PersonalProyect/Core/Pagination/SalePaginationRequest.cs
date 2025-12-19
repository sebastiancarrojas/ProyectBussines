namespace PersonalProyect.Core.Pagination
{
    public class SalePaginationRequest : PaginationRequest
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
