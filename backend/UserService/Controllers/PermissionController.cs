using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using UserService.Models;
using UserService.Service;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/permission")]
    [Produces("application/json", "application/xml")]
    [Consumes("application/json")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService repository;
        public PermissionController(IPermissionService repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<PermissionDto>> GetAllPermissions()
        {
            var permissionDtos = repository.GetAllPermissions();

            if (permissionDtos == null || permissionDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(permissionDtos);

        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{PermissionId}")]
        public ActionResult<PermissionDto> GetPermissionById(Guid PermissionId)
        {
            var permissionDto = repository.GetPermissionById(PermissionId);

            if (permissionDto == null)
            {
                return NotFound();
            }

            return Ok(permissionDto);

        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreatePermission([FromBody] PermissionDto permissionDto)
        {
            try
            {

                repository.CreatePermission(permissionDto);
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

        //[Authorize(Roles = "Admin")]
        [HttpPut("{PermissionId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePermission(Guid PermissionId, [FromBody] PermissionDto permissionDto)
        {
            try
            {
                var newPermission = repository.UpdatePermission(PermissionId, permissionDto);

                if (newPermission == null)
                {
                    return NotFound();
                }

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

        //[Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{PermissionId}")]
        public IActionResult DeletePermission(Guid PermissionId)
        {
            try
            {
                if (repository.GetPermissionById(PermissionId) == null)
                {
                    return NotFound();
                }

                repository.DeletePermission(PermissionId);
                return NoContent();

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

    }
}
