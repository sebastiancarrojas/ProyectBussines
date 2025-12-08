using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.Data.Entities
{
    public class User : IdentityUser // <Guid>
    {
        // IdentityUser ya contiene:
        // - Id (string)
        // - UserName
        // - Email
        // - PasswordHash
        // - PhoneNumber
        // - Lockout, TwoFactor, etc.

        [Required]
        public string? FullName { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;

        // public List<Sale> Sales { get; set; } = new();
    }
}

