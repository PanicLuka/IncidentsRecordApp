using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserService.Service;
using UserService.Models;
using UserService.Attributes;

namespace UserService.Controllers
{
    
    [ApiController]
    [Route("api/roles")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class RoleController : ControllerBase
    {   
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [MicroserviceAuth]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<RoleDto>> GetAllRoles()
        {
            var roleDtos = _roleService.GetAllRoles();

            return Ok(roleDtos);

        }
        [MicroserviceAuth]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{roleId}")]
        public ActionResult<RoleDto> GetRoleById(Guid roleId)
        {
            var roleDto = _roleService.GetRoleById(roleId);

            return Ok(roleDto);
        }
        [MicroserviceAuth]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateRole([FromBody] RoleDto roleDto)
        {
            try
            {
             
                _roleService.CreateRole(roleDto);
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
        [HttpPut("{roleId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateRole(Guid roleId, [FromBody] RoleDto roleDto)
        {
            try
            {
                var newRole = _roleService.UpdateRole(roleId, roleDto);

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
        [MicroserviceAuth]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{roleId}")]
        public IActionResult DeleteRole(Guid roleId)
        {
            try
            {
               
                _roleService.DeleteRole(roleId);
                return NoContent();
              

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
