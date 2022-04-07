using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using UserService.Data;
using UserService.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/register")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUsersService repository;
        public UserController(IUsersService repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public ActionResult<List<UserDto>> GetUsers()
        {
            var userDtos =  repository.GetAllUsers();

            if (userDtos == null || userDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(userDtos);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{UserId}")]
        public  ActionResult<UserDto> GetUserById(Guid UserId)
        {
            var userDto =  repository.GetUserById(UserId);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("user/{Email}")]
        public ActionResult<UserDto> GetUserByEmail(string Email)
        {
            var userDto = repository.GetUserByEmail(Email);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }


        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateUser([FromBody] UserDto userDto)
        {
            try
            {
                repository.CreateUser(userDto);
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

        [HttpPut("{UserId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUser(Guid UserId,[FromBody] UserDto userDto)
        {
            try
            {
                var newUser =  repository.UpdateUser(UserId, userDto);

                if(newUser == null)
                {
                    return NotFound();
                }

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


        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{UserId}")]
        public IActionResult DeleteUser(Guid UserId)
        {
            try
            {
                if (repository.GetUserById(UserId) == null)
                {
                    return NotFound();
                }

                repository.DeleteUser(UserId);
                return NoContent();

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
