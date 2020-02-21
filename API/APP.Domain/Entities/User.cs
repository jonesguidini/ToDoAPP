using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace APP.Domain.Entities
{
    [NotMapped]
    public class User : DeletedEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        //public ICollection<Log> Logs { get; set; }
    }
}