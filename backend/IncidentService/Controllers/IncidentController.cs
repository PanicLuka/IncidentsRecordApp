using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using IncidentService.Data;
using IncidentService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
