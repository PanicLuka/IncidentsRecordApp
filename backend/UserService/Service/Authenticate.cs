using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Data;
using UserService.Models;
using BC = BCrypt.Net.BCrypt;

namespace UserService.Service
{
    public class Authenticate : IAuthenticate
    {
        private readonly IUserRepository userRepository;
        private readonly IRoleRepository roleRepository;
        public Authenticate(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            this.userRepository = userRepository;
            this.roleRepository = roleRepository;
        }
        public string GenerateToken(UserLogin user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SuperSecretKey@123"));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            Guid roleId =  userRepository.GetRoleIdByUserEmail(user.Email);
            string roleType =  roleRepository.GetRoleByRoleId(roleId);


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


        

        public bool VerifyPassword(UserLogin user)
        {
            var account = userRepository.GetUserByEmail(user.Email);
            
            if (!BC.Verify(user.Password, account.Password))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
