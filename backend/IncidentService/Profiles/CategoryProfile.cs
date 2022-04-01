using AutoMapper;
using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>().ForMember(x => x.CategoryId, y => y.Ignore());
            CreateMap<Category, Category>().ForMember(x => x.CategoryId, y => y.Ignore());
            CreateMap<CategoryDto, CategoryDto>();
        }
    }
}
