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

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentsService _incidentsService;

        public IncidentController(IIncidentsService incidentsService)
        {
            this._incidentsService = incidentsService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<IncidentDto>> GetIncidents([FromQuery] IncidentOpts incidentOpts)
        { 
            var incidentDtos = _incidentsService.GetIncidents(incidentOpts);

            var metdata = new
            {
                incidentDtos.TotalCount,
                incidentDtos.PageSize,
                incidentDtos.CurrentPage,
                incidentDtos.HasNext,
                incidentDtos.HasPrevious
            };

            if (incidentDtos == null || incidentDtos.Count == 0)
            {
                return NoContent();
            }

            return Ok(incidentDtos);
        }

        [HttpGet("{IncidentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IncidentDto> GetIncidentById(Guid IncidentId)
        {
            var incidentDto = _incidentsService.GetIncidentById(IncidentId);

            if (incidentDto == null)
            {
                return NotFound();
            }

            return Ok(incidentDto);

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult CreateIncident([FromBody] IncidentDto incidentDto)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                IEnumerable<Claim> claim = identity.Claims;

                var userIdClaim = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();

                Guid userId = Guid.NewGuid();

                _incidentsService.CreateIncident(incidentDto, userId);

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

                if (newIncident == null)
                {
                    return NotFound();
                }

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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{IncidentId}")]
        public IActionResult DeleteIncident(Guid IncidentId)
        {
            try
            {
                var incident = _incidentsService.GetIncidentById(IncidentId);

                if (incident == null)
                {
                    return NotFound();
                }

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
    }
}
