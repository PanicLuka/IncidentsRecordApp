using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncidentService.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncidentService.Data
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly IncidentContext context;

        public IncidentRepository(IncidentContext context)
        {
            this.context = context;
        }
        public async Task CreateIncidentAsync(Incident incident)
        {
            await context.AddAsync(incident);
        }

        public async Task DeleteIncidentAsync(int id)
        {
            var incident = await GetIncidentByIdAsync(id);
            context.Remove(incident);
        }

        public async Task<Incident> GetIncidentByIdAsync(int id)
        {
            return await context.Incidents.FirstOrDefaultAsync(e => e.IncidentId == id);
        }

        public async Task<List<Incident>> GetIncidentsAsync()
        {
            return await context.Incidents.ToListAsync();
        }

        public async Task UpdateIncidentAsync(Incident incident)
        {
            
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

    }
}
