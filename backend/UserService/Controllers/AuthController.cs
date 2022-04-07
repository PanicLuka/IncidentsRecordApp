using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/login")]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService userRepository;
        private readonly IAuthenticate authenticate;

        public AuthController(IUsersService userRepository, IAuthenticate authenticate)
        {
            this.userRepository = userRepository;
            this.authenticate = authenticate;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Login([FromBody] UserLogin user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client request");
            }

            var entity =  userRepository.GetUserByEmail(user.Email);

            bool verify = authenticate.VerifyPassword(user);

            if (user.Email == entity.Email && verify == true)
            {

                var tokenString =  authenticate.GenerateToken(user);
                return Ok(tokenString);
            }

            return Unauthorized();

        }

    }
}
