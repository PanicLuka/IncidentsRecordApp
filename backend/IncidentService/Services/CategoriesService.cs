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
        private readonly DataContext context;
        private readonly CategoryValidator categoryValidator = new CategoryValidator();

        public CategoriesService(DataContext context)
        {
            this.context = context;
        }
        public void CreateCategory(CategoryDto categoryDto)
        {
            Category category = categoryDto.DtoToCategory();

            categoryValidator.ValidateAndThrow(categoryDto);

            context.Add(category);

            SaveChanges();
        }

        public void DeleteCategory(Guid id)
        {
            var category = GetCategoryForUpdateById(id);
            context.Remove(category);
            SaveChanges();
        }

        public CategoryWithIdDto GetCategoryById(Guid id)
        {
            Category category = context.Categories.FirstOrDefault(e => e.CategoryId == id);

            CategoryWithIdDto categoryWithIdDto = category.CategoryToCategoryWithIdDto();

            return categoryWithIdDto;
        }

        public List<CategoryDto> GetCategories()
        {
            List<Category> categories = context.Categories.ToList();

            List<CategoryDto> categoryDtos = new List<CategoryDto>();

            foreach (var category in categories)
            {
                CategoryDto categoryDto = category.CategoryToDto();

                categoryDtos.Add(categoryDto);
            }
            return categoryDtos;
        }

        private Category GetCategoryForUpdateById (Guid id)
        {
            Category category = context.Categories.FirstOrDefault(e => e.CategoryId == id);

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

                categoryValidator.ValidateAndThrow(categoryDto);

                SaveChanges();

                return oldCategory.CategoryToDto();
            }
        }

        public bool SaveChanges()
        {
            return context.SaveChanges() > 0;
        }

    }
}
