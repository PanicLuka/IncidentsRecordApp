using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserService.Attributes;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/permission")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;
        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [MicroserviceAuth]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PermissionDto>> GetAllPermissions([FromQuery] PermissionParameters permissionParameters)
        {
            var permissionDtos = _permissionService.GetAllPermissions(permissionParameters);

            return Ok(permissionDtos);

        }

        [MicroserviceAuth]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{permissionId}")]
        public ActionResult<PermissionDto> GetPermissionById(Guid permissionId)
        {
            var permissionDto = _permissionService.GetPermissionById(permissionId);

            return Ok(permissionDto);
     
        }
        [MicroserviceAuth]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreatePermission([FromBody] PermissionDto permissionDto)
        {
            try
            {

                _permissionService.CreatePermission(permissionDto);
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
        [HttpPut("{permissionId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePermission(Guid permissionId, [FromBody] PermissionDto permissionDto)
        {
            try
            {
                var newPermission = _permissionService.UpdatePermission(permissionId, permissionDto);

                return Ok(newPermission);
    
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
        [HttpDelete("{permissionId}")]
        public IActionResult DeletePermission(Guid permissionId)
        {
            try
            {
                _permissionService.DeletePermission(permissionId);
                 return NoContent();
           
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
