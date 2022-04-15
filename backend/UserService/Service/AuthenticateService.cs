using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserService.Helpers;
using UserService.Models;
using BC = BCrypt.Net.BCrypt;

namespace UserService.Service
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IUsersService _userService;
        private readonly IRoleService _roleService;
        private readonly IOptions<JsonValuesHelper> _options;
      

        public AuthenticateService(IUsersService userService, IRoleService roleService, IOptions<JsonValuesHelper> options)
        {
            _userService = userService;
            _roleService = roleService;
            _options = options;
           
        }

        public string GenerateToken(UserLogin user)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.JWTkey));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            Guid roleId =  _userService.GetRoleIdByUserEmail(user.Email);
            string roleType =  _roleService.GetRoleByRoleId(roleId);
            Guid userId = _userService.GetUserIdByEmail(user.Email);
            
            
            List<Claim> claims = new List<Claim>
                {
                    new Claim("UserID", userId.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Role, roleType),

                };

            var tokenOptions = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                audience: _options.Value.Audience,
                claims:claims,
                expires: DateTime.Now.AddHours(_options.Value.Hours),
                signingCredentials: signingCredentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public bool VerifiedPassword(UserLogin user)
        {
            var account = _userService.GetUserByEmail(user.Email);
            return BC.Verify(user.Password, account.Password);
        }
    }
}
