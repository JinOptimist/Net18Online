using Enums.Users;

namespace WebPortalEverthing.Models.LoadTesting.Auth
{
    public class RegLoadUserViewModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Role Role { get; set; } = Role.User;
    }
}