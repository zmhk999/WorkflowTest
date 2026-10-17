namespace RaceDay_API.Models
{
    public class RegisterDto
    {
        public string UserName { get; set; } = "";
        public string UserPassword { get; set; } = "";
        public string UserAge { get; set; } = "";
        public string Email { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string RoleName { get; set; } = "";
    }
}
