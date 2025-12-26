using PersonalProyect.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs.Customers
{
    public class CreateCustomerDTO
    {
        [Key]
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string? NickName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public string Email { get; set; } = string.Empty;

    }
}
