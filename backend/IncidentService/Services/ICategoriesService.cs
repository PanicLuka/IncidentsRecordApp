using System;
using IncidentService.Helpers;
using IncidentService.Models;

namespace IncidentService.Services
{
    public interface ICategoriesService
    {
        PagedList<CategoryDto> GetCategories(CategoryParameters categoryParameters);
        CategoryWithIdDto GetCategoryById(Guid id);
        CategoryDto UpdateCategory(Guid CategoryId, CategoryDto categoryDto);
        void DeleteCategory(Guid id);
        void CreateCategory(CategoryDto categoryDto);
        bool SaveChanges();
    }
}
