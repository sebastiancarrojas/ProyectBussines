using System.ComponentModel.DataAnnotations;

namespace PersonalProyect.DTOs
{
    public class LoginDTO
    {
        // Email
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico")]
        public required string Email { get; set; }
        // Password
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MinLength(4, ErrorMessage = "El campo {0} debe tener al menos {1} caracteres.")]
        [Display(Name = "Contraseña")]
        public required string Password { get; set; }
    }
}
