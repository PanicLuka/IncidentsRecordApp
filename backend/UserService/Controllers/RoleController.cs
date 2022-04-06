
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserService.Data;
using UserService.Models;
using UserService.Validators;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/role")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    
    public class RoleController : ControllerBase
    {   
        private readonly IRoleService repository;
        public RoleController(IRoleService repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<RoleDto>> GetAllRoles()
        {
            var roleDtos = repository.GetAllRoles();

            if (roleDtos == null || roleDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(roleDtos);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{RoleId}")]
        public ActionResult<RoleDto> GetRoleById(Guid RoleId)
        {
            var roleDto = repository.GetRoleById(RoleId);

            if (roleDto == null)
            {
                return NotFound();
            }

            return Ok(roleDto);

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateRole([FromBody] RoleDto roleDto)
        {
            try
            {
             
                repository.CreateRole(roleDto);
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

        [Authorize(Roles = "Admin")]
        [HttpPut("{RoleId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateRole(Guid RoleId, [FromBody] RoleDto roleDto)
        {
            try
            {

                var newRole = repository.UpdateRole(RoleId, roleDto);

                if (newRole == null)
                {
                    return NotFound();
                }

                return Ok(newRole);

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

        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{RoleId}")]
        public IActionResult DeleteRole(Guid RoleId)
        {
            try
            {
                if (repository.GetRoleById(RoleId) == null)
                {
                    return NotFound();
                }

                repository.DeleteRole(RoleId);
                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
