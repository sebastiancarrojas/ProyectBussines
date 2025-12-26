namespace PersonalProyect.DTOs.Customers
{
    public class CustomerLookupDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
    }

}
