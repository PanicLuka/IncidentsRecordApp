using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Data;
using UserService.Enitites;
using UserService.Helpers;
using UserService.Models;
using UserService.Validators;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/role")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        
        private readonly IRoleRepository repository;
        private readonly RoleValidator roleValidator;

        public RoleController(IRoleRepository repository, RoleValidator roleValidator)
        {
            this.repository = repository;
            this.roleValidator = roleValidator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<RoleDto>>> GetAllRolesAsync()
        {
            var roles = await repository.GetAllRolesAsync();

            if(roles == null || roles.Count == 0)
            {
                return NotFound();
            }

            List<RoleDto> roleDtos = new List<RoleDto>();

            foreach(var role in roles)
            {
                RoleDto roleDto = role.RoleToDto();

                roleDtos.Add(roleDto);
            }

            return Ok(roleDtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{RoleId}")]
        public async Task<ActionResult<RoleDto>> GetRoleById(int RoleId)
        {
            var role = await repository.GetRoleByIdAsync(RoleId);

            if (role == null)
            {
                return NotFound();
            }

            var roleDto = role.RoleToDto();

            return Ok(roleDto);
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateRole([FromBody] RoleDto roleDto)
        {
            try
            {
                Role roleEntity = roleDto.DtoToRole();

                roleValidator.ValidateAndThrow(roleEntity);

                await repository.CreateRoleAysnc(roleEntity);

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

        [HttpPut("{RoleId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> UpdateRoleAsync(int RoleId, [FromBody] RoleDto roleDto)
        {
            try
            {
                
                var oldRole = await repository.GetRoleByIdAsync(RoleId);

                if (oldRole == null)
                {
                    return NotFound();
                }

                Role role = roleDto.DtoToRole();

                roleValidator.ValidateAndThrow(role);

                //mapper.Map(role, oldRole);

                oldRole.UserType = role.UserType;

                await repository.SaveChangesAsync();



                //return Ok(mapper.Map<RoleDto>(oldRole));
                return Ok(oldRole.RoleToDto());



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
        [HttpDelete("{RoleId}")]
        public async Task<IActionResult> DeleteRoleAsync(int RoleId)
        {
            try
            {
                var role = await repository.GetRoleByIdAsync(RoleId);

                if (role == null)
                {
                    return NotFound();
                }

                await repository.DeleteRoleAsync(RoleId);

                await repository.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
