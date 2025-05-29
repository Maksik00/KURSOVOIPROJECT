namespace SFLAPI.DTOs
{
    public class RegisterDto
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // "Student" или "Company"
    }
}
