using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using IncidentService.Entities;
using IncidentService.Helpers;
using IncidentService.Models;
using IncidentService.Validators;

namespace IncidentService.Services
{
    public class CategoriesService : ICategoriesService
    {
        private readonly DataContext _context;
        private readonly CategoryValidator _categoryValidator = new CategoryValidator();

        public CategoriesService(DataContext context)
        {
            _context = context;
        }
        public void CreateCategory(CategoryDto categoryDto)
        {
            Category category = categoryDto.DtoToCategory();

            _categoryValidator.ValidateAndThrow(categoryDto);

            _context.Add(category);

            SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var category = GetCategoryForUpdateById(id);
            _context.Remove(category);
            SaveChanges();
        }

        public CategoryWithIdDto GetCategoryById(Guid id)
        {
            Category category = _context.Categories.FirstOrDefault(e => e.CategoryId == id);

            CategoryWithIdDto categoryWithIdDto = category.CategoryToCategoryWithIdDto();

            return categoryWithIdDto;
        }

        public PagedList<CategoryDto> GetCategories(CategoryOpts categoryOpts)
        {
            List<Category> categories = _context.Categories.ToList();

            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                CategoryDto categoryDto = category.CategoryToDto();

                categoryDtos.Add(categoryDto);
            }

            IQueryable<CategoryDto> queryable = categoryDtos.AsQueryable();

            return PagedList<CategoryDto>.ToPagedList(queryable, categoryOpts.PageNumber, categoryOpts.PageSize);
        }

        private Category GetCategoryForUpdateById (Guid id)
        {
            Category category = _context.Categories.FirstOrDefault(e => e.CategoryId == id);

            return category;
        }

        public CategoryDto UpdateCategory(Guid CategoryId, CategoryDto categoryDto)
        {
            var oldCategory = GetCategoryForUpdateById(CategoryId);

            if (oldCategory == null)
            {
                CreateCategory(categoryDto);
                return categoryDto;
            }
            else
            {
                Category category = categoryDto.DtoToCategory();

                oldCategory.CategoryName = category.CategoryName;

                _categoryValidator.ValidateAndThrow(categoryDto);

                SaveChanges();

                return oldCategory.CategoryToDto();
            }
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() > 0;
        }

    }
}
