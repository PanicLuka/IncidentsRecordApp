using System;
using System.Collections.Generic;
using IncidentService.Entities;
using IncidentService.Models;

namespace IncidentService.Services
{
    public interface ICategoriesService
    {
        List<CategoryDto> GetCategories();
        CategoryWithIdDto GetCategoryById(Guid id);
        Category GetCategoryForUpdateById(Guid id);
        CategoryDto UpdateCategory(Guid CategoryId, CategoryDto categoryDto);
        void DeleteCategory(Guid id);
        void CreateCategory(CategoryDto categoryDto);
        bool SaveChanges();
    }
}
