using System.Collections.Generic;
using IncidentService.Entities;

namespace IncidentService.Data
{
    public interface IIncidentRepository
    {
        List<Incident> GetIncidents();
        Incident GetIncidentById(int id);
        void UpdateIncident(Incident incident);
        void DeleteIncident(int id);
        void CreateIncident(Incident incident);
        
    }
}
