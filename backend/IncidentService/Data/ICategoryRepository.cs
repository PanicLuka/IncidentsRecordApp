using System.Collections.Generic;
using IncidentService.Entities;

namespace IncidentService.Data
{
    public interface ICategoryRepository
    {
        List<Category> GetIncidents();
        Incident GetIncidentById(int id);
        void UpdateIncident(Category category);
        void DeleteIncident(int id);
        void CreateIncident(Category category);
    }
}
