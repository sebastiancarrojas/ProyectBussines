namespace PersonalProyect.DTOs.Customers
{
    public class QuickCreateCustomerDto
    {
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;
    }

}
