using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProductsCRUDApi.Entities;
using ProductsCRUDApi.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductsCRUDApi.Services
{
    public class AccountService
    {
        private readonly IEnumerable<User> _users = new List<User>
        {
            new User { Id = 1, Username = "fred", Password = "123" },
            new User { Id = 2, Username = "alice", Password = "456" },
            new User { Id = 3, Username = "joe", Password = "789" },
        };

        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Login(LoginDTO loginDto)
        {
            var user = _users.Where(x => x.Username == loginDto.Username && x.Password == loginDto.Password).SingleOrDefault();

            if (user == null)
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
                        new Claim("userid", user.Id.ToString())
                    }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(signingKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var token = jwtTokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
