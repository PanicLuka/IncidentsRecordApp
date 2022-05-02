using System;
using System.Collections.Generic;
using IncidentService.Models;

namespace IncidentService.Services
{
    public interface ICategoriesService
    {
        List<CategoryWithIdDto> GetCategories(CategoryOpts categoryOpts);
        CategoryWithIdDto GetCategoryById(Guid id);
        CategoryDto UpdateCategory(Guid CategoryId, CategoryDto categoryDto);
        void DeleteCategory(Guid id);
        CategoryDto CreateCategory(CategoryDto categoryDto);
        int GetCategoryCount();
        bool SaveChanges();
    }
}
