using System.Collections.Generic;

namespace APP.Domain.Entities
{
    public class User : DeletedEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        //public ICollection<Log> Logs { get; set; }
    }
}