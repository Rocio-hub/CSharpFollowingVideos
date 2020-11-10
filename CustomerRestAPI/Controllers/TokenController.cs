
using CustomerApp.Core.DomainService;
using CustomerApp.Core.Entity;
using CustomerRestAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using CustomerRestAPI.Helpers;

namespace CustomerRestAPI.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private readonly IUserRepository<User> repository;
        public TokenController(IUserRepository<User> repos)
        {
            repository = repos;
        }
        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = repository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            if (user == null)
            {
                return Unauthorized();
            }
            if (!VerifyPasswordHash(model.Password, user.PasswordHash,user.PasswordSalt))
            {
                return Unauthorized();
            }
            return Ok(new
            {
                username = user.Username,
                token = GenerateToken(user)
            });
        }

        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storeddSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storeddSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i =0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        //This method generates and returns a JWT Token for a User
        private string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.Username)
            };
            if (user.IsAdmin)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            }
            var token = new JwtSecurityToken(
                new JwtHeader(new SigningCredentials(
                    JwtSecurityKey.Key,
                    SecurityAlgorithms.HmacSha256)),
                new JwtPayload(null, //issuer not needed
                null, //audience not needed
                claims.ToArray(),
                DateTime.Now,
                DateTime.Now.AddMinutes(5))); //expires
            
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
