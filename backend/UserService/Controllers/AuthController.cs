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
        private readonly IUsersService _userService;
        private readonly IAuthenticateService _authenticateService;

        public AuthController(IUsersService userService, IAuthenticateService authenticateService)
        {
            _userService = userService;
            _authenticateService = authenticateService;
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

            var savedUser = _userService.GetUserByEmail(user.Email);

            bool passwordVerified = _authenticateService.VerifiedPassword(user);

            if (user.Email == savedUser.Email && passwordVerified)
            {

                var tokenString = _authenticateService.GenerateToken(user);
                return Ok(tokenString);
            }

            return Unauthorized();

        }

    }
}
