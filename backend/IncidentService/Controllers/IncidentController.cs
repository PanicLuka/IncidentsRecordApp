using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using IncidentService.Services;
using IncidentService.Entities;
using IncidentService.Models;
using IncidentService.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using IncidentService.Helpers;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentsService incidentsService;
        //private readonly IMapper mapper;
        private readonly IncidentValidator incidentValidator;

        public IncidentController(IIncidentsService incidentsService, /*IMapper mapper,*/ IncidentValidator incidentValidator)
        {
            this.incidentsService = incidentsService;
            //this.mapper = mapper;
            this.incidentValidator = incidentValidator;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<IncidentDto>>> GetIncidents()
        {
            var incidents = await incidentsService.GetIncidentsAsync();


            if (incidents == null || incidents.Count == 0)
            {
                return NoContent();
            }


            List<IncidentDto> incidentDtos = new List<IncidentDto>();

            foreach (var incident in incidents)
            {
                //IncidentDto incidentDto = mapper.Map<IncidentDto>(incident);

                IncidentDto incidentDto = incident.IncidentToDto();

                incidentDtos.Add(incidentDto);
            }

            return Ok(incidentDtos);
        }

        [HttpGet("{IncidentId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IncidentDto>> GetIncidentByIdAsync(int IncidentId)
        {
            var incident = await incidentsService.GetIncidentByIdAsync(IncidentId);

            if (incident == null)
            {
                return NotFound();
            }

            //IncidentDto incidentDto = mapper.Map<IncidentDto>(incident);

            IncidentDto incidentDto = incident.IncidentToDto();

            return Ok(incidentDto);

        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> CreateIncidentAsync([FromBody] IncidentDto incidentDto)
        {
            try
            {
                //Incident incident = mapper.Map<Incident>(incidentDto);

                Incident incident = incidentDto.DtoToIncident();

                incidentValidator.ValidateAndThrow(incident);

                await incidentsService.CreateIncidentAsync(incident);

                await incidentsService.SaveChangesAsync();

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
        public async Task<ActionResult> UpdateIncidentAsync(int IncidentId, [FromBody] IncidentDto incidentDto)
        {
            try
            {
                var oldIncident = await incidentsService.GetIncidentByIdAsync(IncidentId);

                if (oldIncident == null)
                {
                    return NotFound();
                }

                //Incident incident = mapper.Map<Incident>(incidentDto);

                Incident incident = incidentDto.DtoToIncident();

                //mapper.Map(incident, oldIncident);

                IncidentDto oldIncidentNew = oldIncident.IncidentToDto();

                incidentValidator.ValidateAndThrow(incident);

                await incidentsService.SaveChangesAsync();

                //return Ok(mapper.Map<IncidentDto>(oldIncident));

                return Ok(oldIncident.IncidentToDto());
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
        public async Task<IActionResult> DeleteIncidentAsync(int IncidentId)
        {
            try
            {
                var incident = await incidentsService.GetIncidentByIdAsync(IncidentId);

                if (incident == null)
                {
                    return NotFound();

                }

                await incidentsService.DeleteIncidentAsync(IncidentId);
                await incidentsService.SaveChangesAsync();
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
