
using System.ComponentModel.DataAnnotations;
using System.Timers;

namespace PersonalProyect.DTOs
{
    public class AccountUserDTO
    {
        public string? Id { get; set; }
        // Email
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        // Password
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        // Confirm Password
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        public required string ConfirmPassword { get; set; }
        // FullName
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public required string FullName { get; set; }
    }
}
