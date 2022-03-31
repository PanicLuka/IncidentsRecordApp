﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IncidentService.Data;
using IncidentService.Entities;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace IncidentService.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository incidentRepository;
        private readonly IMapper mapper;

        public IncidentController(IIncidentRepository incidentRepository, IMapper mapper)
        {
            this.incidentRepository = incidentRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        [HttpHead]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<IncidentDto>>> GetIncidents()
        {
            var incidents = await incidentRepository.GetIncidentsAsync();


            if (incidents == null || incidents.Count == 0)
            {
                return NoContent();
            }


            List<IncidentDto> incidentDtos = new List<IncidentDto>();

            foreach (var incident in incidents)
            {
                IncidentDto incidentDto = mapper.Map<IncidentDto>(incident);

                incidentDtos.Add(incidentDto);
            }

            return Ok(incidentDtos);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{IncidentId}")]
        public async Task<ActionResult<IncidentDto>> GetIncidentByIdAsync(int IncidentId)
        {
            var incident = await incidentRepository.GetIncidentByIdAsync(IncidentId);

            if (incident == null)
            {
                return NotFound();
            }

            IncidentDto incidentDto = mapper.Map<IncidentDto>(incident);

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
                Incident incident = mapper.Map<Incident>(incidentDto);

                await incidentRepository.CreateIncidentAsync(incident);

                await incidentRepository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpPut("{IncidentId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Incident>> UpdateIncidentAsync(int incidentId, [FromBody] Incident incident)
        {
            try
            {
                var oldIncident = await incidentRepository.GetIncidentByIdAsync(incidentId);

                if (oldIncident == null)
                {
                    return NotFound();
                }

                mapper.Map(incident, oldIncident);

                await incidentRepository.UpdateIncidentAsync(incident);

                return Ok(incident);
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
                var incident = await incidentRepository.GetIncidentByIdAsync(IncidentId);

                if (incident == null)
                {
                    return NotFound();

                }

                await incidentRepository.DeleteIncidentAsync(IncidentId);
                await incidentRepository.SaveChangesAsync();
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
