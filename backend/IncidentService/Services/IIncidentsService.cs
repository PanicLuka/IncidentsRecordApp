using System;
using System.Collections.Generic;
using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Services
{
    public interface IIncidentsService
    {
        List<IncidentDto> GetIncidents();
        IncidentWithIdDto GetIncidentById(Guid id);
        Incident GetIncidentForUpdateById(Guid id);
        IncidentDto UpdateIncident(Guid id, IncidentDto incidentDto);
        void DeleteIncident(Guid id);
        void CreateIncident(IncidentDto incidentDto);
        bool SaveChanges();

    }
}
