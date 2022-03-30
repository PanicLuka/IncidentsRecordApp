using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncidentService.Entities;
using Microsoft.EntityFrameworkCore;

namespace IncidentService.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IncidentContext context;

        public CategoryRepository(IncidentContext context)
        {
            this.context = context;
        }
        public async Task CreateCategoryAsync(Category category)
        {
            await context.AddAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            var category = await GetCategoryByIdAsync(id);
            context.Remove(category);
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await context.Categories.FirstOrDefaultAsync(e => e.CategoryId == id);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await context.Categories.ToListAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {

        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

    }
}
