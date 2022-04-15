using System;
using System.Collections.Generic;
using IncidentService.Models;

namespace IncidentService.Services
{
    public interface IIncidentsService
    {
        List<IncidentWithIdDto> GetIncidents(IncidentOpts incidentOpts);
        IncidentWithIdDto GetIncidentById(Guid id);
        IncidentDto UpdateIncident(Guid id, IncidentDto incidentDto);
        void DeleteIncident(Guid id);
        IncidentWithIdDto CreateIncident(IncidentDto incidentDto, Guid userId);
        int GetIncidentCount();
        bool SaveChanges();

    }
}
