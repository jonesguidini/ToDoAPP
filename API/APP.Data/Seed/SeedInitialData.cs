using APP.Data.Context;
using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace APP.API.Data
{
    public class SeedInitialData
    {
        private readonly DBContext _context;
        private DbSet<User> _users;

        public SeedInitialData(DBContext context)
        {
            _context = context;
            _users = context.Set<User>();
        }

        public void SeedUsers()
        {
            var userData = System.IO.File.ReadAllText("../APP.Data/Seed/SeedBaseUsers.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);

            users = users.Where(x => !_users.Select(y => y.Email).Contains(x.Email)).ToList();


            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("admin", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();
                user.Email = user.Email.ToLower();
                user.Created = DateTime.Now;

                _users.Add(user);
            }

            _context.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}