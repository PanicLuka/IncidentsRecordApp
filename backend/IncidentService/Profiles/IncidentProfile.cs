using AutoMapper;
using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Profiles
{
    public class IncidentProfile : Profile
    {
        public IncidentProfile()
        {
            CreateMap<Incident, IncidentDto>();
            CreateMap<IncidentDto, Incident>();
        }
    }
}
