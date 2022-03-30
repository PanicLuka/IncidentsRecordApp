using System.Collections.Generic;
using System.Linq;
using IncidentService.Entities;

namespace IncidentService.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IncidentContext context;

        public CategoryRepository(IncidentContext context)
        {
            this.context = context;
        }
        public void CreateCategory(Category category)
        {
            context.Add(category);
        }

        public void DeleteCategory(int id)
        {
            var category = GetCategoryById(id);
            context.Remove(category);
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.FirstOrDefault(e => e.CategoryId == id);
        }

        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }

        public void UpdateCategory(Category category)
        {

        }
    }
}
