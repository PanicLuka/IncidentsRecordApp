using System;
using System.Collections.Generic;
using FluentValidation;
using IncidentService.Services;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Linq;
using Newtonsoft.Json;

namespace IncidentService.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentsService incidentsService;

        public IncidentController(IIncidentsService incidentsService)
        {
            this.incidentsService = incidentsService;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<List<IncidentDto>> GetIncidents([FromQuery] IncidentParameters incidentParameters)
        { 
            var incidentDtos = incidentsService.GetIncidents(incidentParameters);

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
            var incidentDto = incidentsService.GetIncidentById(IncidentId);

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

                var userEmailClaim = claim.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault();


                /*if (userEmailClaim == null)
                {
                    return BadRequest();
                }*/


                //Must add getUserIdByEmail when implemented in User service

                Guid userId = Guid.NewGuid();

                incidentsService.CreateIncident(incidentDto, userId);

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
        public ActionResult UpdateIncident(Guid IncidentId, [FromBody] IncidentDto incidentDto)
        {
            try
            {
                var newIncident = incidentsService.UpdateIncident(IncidentId, incidentDto);

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
                var incident = incidentsService.GetIncidentById(IncidentId);

                if (incident == null)
                {
                    return NotFound();
                }

                incidentsService.DeleteIncident(IncidentId);

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
