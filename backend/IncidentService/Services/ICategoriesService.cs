using System.Collections.Generic;
using System.Threading.Tasks;
using IncidentService.Entities;

namespace IncidentService.Services
{
    public interface ICategoriesService
    {
        Task<List<Category>> GetCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task UpdateCategoryAsync(Category category);
        Task DeleteCategoryAsync(int id);
        Task CreateCategoryAsync(Category category);
        Task<bool> SaveChangesAsync();
    }
}
