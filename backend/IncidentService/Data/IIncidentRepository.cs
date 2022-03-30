using System.Collections.Generic;
using System.Threading.Tasks;
using IncidentService.Entities;

namespace IncidentService.Data
{
    public interface IIncidentRepository
    {
        Task<List<Incident>> GetIncidentsAsync();
        Task<Incident> GetIncidentByIdAsync(int id);
        Task UpdateIncidentAsync(Incident incident);
        Task DeleteIncidentAsync(int id);
        Task CreateIncidentAsync(Incident incident);
        Task<bool> SaveChangesAsync();

    }
}
