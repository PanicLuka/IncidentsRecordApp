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
            CreateMap<CategoryDto, Category>();
        }
    }
}
