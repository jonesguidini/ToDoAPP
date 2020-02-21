using APP.Domain.Entities;

namespace APP.Domain.VMs
{
    public class UserVM : DeletedEntityVM
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}