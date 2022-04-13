using System;
using System.Collections.Generic;
using FluentValidation;
using IncidentService.Services;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using IncidentService.Attributes;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/incidents")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentsService _incidentsService;

        public IncidentController(IIncidentsService incidentsService)
        {
            _incidentsService = incidentsService;
        }

        [MicroserviceAuth]
        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<IncidentDto>> GetIncidents([FromQuery] IncidentOpts incidentOpts)
        {
            try
            {
                var incidentDtos = _incidentsService.GetIncidents(incidentOpts);

                return Ok(incidentDtos);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [MicroserviceAuth]
        [Route("count")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<int> GetIncidentCount()
        {
            try
            {
                var count = _incidentsService.GetIncidentCount();

                return Ok(count);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [MicroserviceAuth]
        [HttpGet("{IncidentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IncidentDto> GetIncidentById(Guid IncidentId)
        {
            try
            {
                var incidentDto = _incidentsService.GetIncidentById(IncidentId);

                return Ok(incidentDto);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }

        [MicroserviceAuth]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateIncident([FromBody] IncidentDto incidentDto)
        {
            try
            {
                var userId = GetUserId();

                var createdIncident = _incidentsService.CreateIncident(incidentDto, userId);

                return Ok(createdIncident);
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
        [HttpPut("{IncidentId}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IncidentDto> UpdateIncident(Guid IncidentId, [FromBody] IncidentDto incidentDto)
        {
            try
            {
                var newIncident = _incidentsService.UpdateIncident(IncidentId, incidentDto);

                return Ok(newIncident);
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
        [HttpDelete("{IncidentId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteIncident(Guid IncidentId)
        {
            try
            {
                var incident = _incidentsService.GetIncidentById(IncidentId);

                _incidentsService.DeleteIncident(IncidentId);

                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpOptions]
        public IActionResult GetIncidentOptions()
        {
            Response.Headers.Add("Allow", "GET, POST, PUT, DELETE");
            return Ok();
        }

        private Guid GetUserId()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            IEnumerable<Claim> claim = identity.Claims;

            var userIdClaim = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().ToString();

            return Guid.Parse(userIdClaim);
        }
    }
}
