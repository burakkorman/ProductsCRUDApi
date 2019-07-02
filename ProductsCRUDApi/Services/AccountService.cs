using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductsCRUDApi.Entities;
using ProductsCRUDApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Services
{
    public class AccountService : IAccountService
    {
        //private readonly IEnumerable<User> _users = new List<User>
        //{
        //    new User { Id = 1, Username = "fred", Password = "123" },
        //    new User { Id = 2, Username = "alice", Password = "456" },
        //    new User { Id = 3, Username = "joe", Password = "789" },
        //};

        private NorthwindContext _context;

        public AccountService(NorthwindContext context)
        {
            this._context = context;
        }

        public string Login(UserDTO user)
        {
            //var user = _users.Where(x => x.Username == loginDto.Username && x.Password == loginDto.Password).SingleOrDefault();
            var u = _context.Users.ToList().Where(x => x.Username == user.Username && x.Password == user.Password).SingleOrDefault();

            if (u == null)
            {
                return null;
            }

            var signingKey = Encoding.ASCII.GetBytes("korman54525252525");
            //var expiryDuration = int.Parse(_configuration["Jwt:ExpiryDuration"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = null,              // Not required as no third-party is involved
                Audience = null,            // Not required as no third-party is involved
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddDays(7),
                Subject = new ClaimsIdentity(new List<Claim> {
                        new Claim("userid", u.Id.ToString())
                    }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
        
        public void Create(UserDTO user)
        {
            User u = new User
            {
                Username = user.Username,
                Password = HashPassword(user.Password)
            };
            _context.Users.Add(u);
            _context.SaveChanges();
        }

        public void Update(int id, UserDTO user)
        {
            User u = _context.Users.FirstOrDefault(w => w.Id == id);
            u.Username = user.Username;
            u.Password = HashPassword(user.Password);

            _context.Entry(u).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            User u = _context.Users.FirstOrDefault(w => w.Id == userId);
            _context.Users.Remove(u);
            _context.SaveChanges();
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
                return hash;
            }
        }
    }
}
