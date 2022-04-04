
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/login")]

    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthenticate authenticate;

        public AuthController(IUserRepository userRepository, IAuthenticate authenticate)
        {
            this.userRepository = userRepository;
            this.authenticate = authenticate;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login([FromBody] UserLogin user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var entity = await userRepository.GetUserByEmailAsync(user.Email);

            if (user.Email == entity.Email && user.Password == entity.Password)
            {


                var tokenString = await authenticate.GenerateToken(user);


                return Ok(tokenString);
            }

            return Unauthorized();

        }

    }
}
