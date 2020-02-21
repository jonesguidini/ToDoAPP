using Microsoft.EntityFrameworkCore;
using APP.Data.Context;
using APP.Domain.Contracts.Repositories;
using APP.Domain.Entities;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace APP.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SQLContext _context;
        private DbSet<User> _users;

        public AuthRepository(SQLContext context)
        {
            _context = context;
            _users = context.Set<User>();
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await _users.FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
                return null;
            
            if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt)){
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                for(int i = 0; i < computedHash.Length; i++){
                    if(computedHash[i] != passwordHash[i]) return false;
                }     
            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512()){
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
            
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _users.AnyAsync(x => x.Username == username))
                return true;

            return false;
        }
    }
}