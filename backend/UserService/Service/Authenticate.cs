using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;

namespace UserService.Service
{
    public class Authenticate : IAuthenticate
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        private readonly static int iterations = 100;
        public Authenticate(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }
        public async Task<string> GenerateToken(UserLogin user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@123"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            int roleId = await userRepository.GetRoleIdByUserEmail(user.Email);
            string roleType = await roleRepository.GetRoleByRoleIdAsync(roleId);


            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, roleType)

                };

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5002",
                audience: "http://localhost:5002",
                claims:claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        
        public bool VerifyPassword(string password, string savedHash, string savedSalt)
        {
            var saltBytes = Convert.FromBase64String(savedSalt);
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, iterations);
            if (Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == savedHash)
            {
                return true;
            }
            return false;
        }
    }
}
