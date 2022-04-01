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
            CreateMap<IncidentDto, Incident>().ForMember(x => x.IncidentId, y => y.Ignore());
            CreateMap<Incident, Incident>().ForMember(x => x.IncidentId, y => y.Ignore()).ForMember(x => x.CategoryId, y => y.Ignore());
            CreateMap<IncidentDto, IncidentDto>();
        }
    }
}
