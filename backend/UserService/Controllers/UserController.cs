using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using UserService.Attributes;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    
    [ApiController]
    [Route("api/users")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService _userService;
        public UserController(IUsersService userService)
        {
            _userService = userService;
        }

        [MicroserviceAuth]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<UserDto>> GetUsers([FromQuery] UserParameters userParameters)
        {
            var userDtos =  _userService.GetAllUsers(userParameters);
        
            return Ok(userDtos);
   
        }

        [MicroserviceAuth]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{userId}")]
        public  ActionResult<UserDto> GetUserById(Guid userId)
        {
            var userDto =  _userService.GetUserById(userId);

            return Ok(userDto);

        }
        [MicroserviceAuth]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("user/{email}")]
        public ActionResult<UserDto> GetUserByEmail(string email)
        {
            var userDto = _userService.GetUserByEmail(email);

            return Ok(userDto);
        }

        [MicroserviceAuth]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                _userService.CreateUser(userDto);
                return Ok();
            }
            catch (ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }


        }

        [MicroserviceAuth]
        [HttpPut("{userId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUser(Guid userId,[FromBody] UserDto userDto)
        {
            try
            {
                var newUser =  _userService.UpdateUser(userId, userDto);

                return Ok(newUser);
             
            }
            catch (ValidationException v)
            {
                return StatusCode(StatusCodes.Status400BadRequest, v.Errors);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [MicroserviceAuth]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            try
            {
                _userService.DeleteUser(userId);
                return NoContent();
      
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
