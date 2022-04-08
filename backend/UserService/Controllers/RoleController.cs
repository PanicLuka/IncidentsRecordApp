using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserService.Service;
using UserService.Models;

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
        [HttpGet("{roleId}")]
        public ActionResult<RoleDto> GetRoleById(Guid roleId)
        {
            var roleDto = repository.GetRoleById(roleId);

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
        [HttpPut("{roleId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateRole(Guid roleId, [FromBody] RoleDto roleDto)
        {
            try
            {
                var newRole = repository.UpdateRole(roleId, roleDto);

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
        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(Guid roleId)
        {
            try
            {
                if (repository.GetRoleById(roleId) == null)
                {
                    return NotFound();
                }

                repository.DeleteRole(roleId);
                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
