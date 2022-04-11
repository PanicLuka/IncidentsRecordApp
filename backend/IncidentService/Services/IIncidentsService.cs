using System;
using IncidentService.Helpers;
using IncidentService.Models;

namespace IncidentService.Services
{
    public interface IIncidentsService
    {
        PagedList<IncidentDto> GetIncidents(IncidentOpts incidentOpts);
        IncidentWithIdDto GetIncidentById(Guid id);
        IncidentDto UpdateIncident(Guid id, IncidentDto incidentDto);
        void DeleteIncident(Guid id);
        void CreateIncident(IncidentDto incidentDto, Guid userId);
        bool SaveChanges();

    }
}
