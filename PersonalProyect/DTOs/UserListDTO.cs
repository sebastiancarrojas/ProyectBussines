
// DTO para la implementación de una lista de usuarios en el Nav Bar AdminLayout

namespace PersonalProyect.DTOs
{
    public class UserListDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string RoleName { get; set; }
    }
}
