using System.Collections.Generic;
using IncidentService.Entities;

namespace IncidentService.Data
{
    public interface ICategoryRepository
    {
        List<Category> GetCategories();
        Category GetCategoryById(int id);
        void UpdateCategory(Category category);
        void DeleteCategory(int id);
        void CreateCategory(Category category);
    }
}
