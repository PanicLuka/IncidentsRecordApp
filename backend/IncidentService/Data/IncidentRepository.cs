using System.Collections.Generic;
using System.Linq;
using IncidentService.Entities;

namespace IncidentService.Data
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly IncidentContext context;

        public IncidentRepository(IncidentContext context)
        {
            this.context = context;
        }
        public void CreateIncident(Incident incident)
        {
            context.Add(incident);
        }

        public void DeleteIncident(int id)
        {
            var incident = GetIncidentById(id);
            context.Remove(incident);
        }

        public Incident GetIncidentById(int id)
        {
            return context.Incidents.FirstOrDefault(e => e.IncidentId == id);
        }

        public List<Incident> GetIncidents()
        {
            return context.Incidents.ToList();
        }

        public void UpdateIncident(Incident incident)
        {

        }
    }
}
