using APP.Domain.Entities;
using System.Threading.Tasks;

namespace APP.Domain.Contracts.Repositories
{
    public interface IAuthRepository
    {
        
         Task<User> Register(User user, string password);

         Task<User> Login(string username, string password);

         Task<bool> UserExists(string username);
    }
}