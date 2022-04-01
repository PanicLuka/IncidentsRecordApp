﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Enitites;
using UserService.Models;
using UserService.Validators;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/register")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    public class RegisterController : ControllerBase
    {
        private readonly IRegisterRepository repository;
        private readonly IMapper mapper;
        private readonly UserValidator userValidator;
        public RegisterController(IRegisterRepository repository, UserValidator userValidator, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userValidator = userValidator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]

        public async Task<ActionResult<List<UserDto>>> GetUsersAsync()
        {
            var users = await repository.GetAllUsersAsync();

            if (users == null || users.Count == 0)
            {
                return NoContent();
            }

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
                UserDto registerDto = mapper.Map<UserDto>(user);

                userDtos.Add(registerDto);
            }

            return Ok(userDtos);
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{UserId}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int UserId)
        {
            var user = await repository.GetUserByIdAsync(UserId);

            if (user == null)
            {
                return NotFound();
            }

            var userDto = mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateUserAsync([FromBody] UserDto user)
        {
            try
            {
                User userEntity = mapper.Map<User>(user);

                userValidator.ValidateAndThrow(userEntity);

                await repository.CreateUserAsync(userEntity);

                await repository.SaveChangesAsync();

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
        public async Task<ActionResult> UpdateUserAsync(int UserId,[FromBody] UserDto userDto)
        {
            try
            {

                var oldUser = await repository.GetUserByIdAsync(UserId);

                if(oldUser == null)
                {
                    return NotFound();
                }

                User user = mapper.Map<User>(userDto);

                userValidator.ValidateAndThrow(user);

                mapper.Map(user, oldUser);

                
                await repository.SaveChangesAsync();
               
               

                return Ok(mapper.Map<UserDto>(oldUser));

                

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
        public async Task<IActionResult> DeleteUserAsync(int UserId)
        {
            try
            {
                var user = repository.GetUserByIdAsync(UserId);

                if(user == null)
                {
                    return NotFound();
                }

                await repository.DeleteUserAsync(UserId);
                await repository.SaveChangesAsync();
                return NoContent();

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
