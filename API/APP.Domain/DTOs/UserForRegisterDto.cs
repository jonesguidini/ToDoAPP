using APP.Domain.Entities;

namespace APP.Domain.DTOs
{
    public class UserForRegisterDTO : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}