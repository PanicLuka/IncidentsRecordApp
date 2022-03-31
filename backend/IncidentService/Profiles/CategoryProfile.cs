using AutoMapper;
using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<CategoryDto, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, Category>();
            CreateMap<CategoryDto, CategoryDto>();
            CreateMap<CategoryUpdateDto, CategoryUpdateDto>();
        }
    }
}
