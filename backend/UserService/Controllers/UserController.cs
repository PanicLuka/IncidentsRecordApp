using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using UserService.Models;
using UserService.Service;

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
        [HttpGet("{userId}")]
        public  ActionResult<UserDto> GetUserById(Guid userId)
        {
            var userDto =  repository.GetUserById(userId);

            if (userDto == null)
            {
                return NotFound();
            }

            return Ok(userDto);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("user/{email}")]
        public ActionResult<UserDto> GetUserByEmail(string email)
        {
            var userDto = repository.GetUserByEmail(email);

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

        [HttpPut("{userId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUser(Guid userId,[FromBody] UserDto userDto)
        {
            try
            {
                var newUser =  repository.UpdateUser(userId, userDto);

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
        [HttpDelete("{userId}")]
        public IActionResult DeleteUser(Guid userId)
        {
            try
            {
                if (repository.GetUserById(userId) == null)
                {
                    return NotFound();
                }

                repository.DeleteUser(userId);
                return NoContent();

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
