using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
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
        private static int _count;

        public CategoriesService(DataContext context)
        {
            _context = context;
        }

        public CategoryDto CreateCategory(CategoryDto categoryDto)
        {
            Category category = categoryDto.DtoToCategory();

            _categoryValidator.ValidateAndThrow(categoryDto);

            _context.Add(category);

            SaveChanges();

            return category.CategoryToDto();
        }

        public void DeleteCategory(Guid id)
        {
            var category = GetCategoryForUpdateById(id);
            
            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(category);
            
            SaveChanges();
        }

        public CategoryWithIdDto GetCategoryById(Guid id)
        {
            Category category = _context.Categories.FirstOrDefault(e => e.CategoryId == id);

            CategoryWithIdDto categoryWithIdDto = category.CategoryToCategoryWithIdDto();

            if (categoryWithIdDto == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return categoryWithIdDto;
        }

        public List<CategoryDto> GetCategories(CategoryOpts categoryOpts)
        {
            var filteredCategories = FilterCategories(categoryOpts);

            List<Category> categories = filteredCategories.ToList();

            if (categories == null)
            {
                throw new HttpResponseException(HttpStatusCode.NoContent);
            }

            List<CategoryDto> categoryDtos = categories.Select(category => category.CategoryToDto()).ToList();

            return categoryDtos;
        }

        private IQueryable<Category> FilterCategories(CategoryOpts categoryOpts)
        {

            IQueryable<Category> categoryList = _context.Categories;

            _count = categoryList.Count();

            categoryList = categoryList.Skip((categoryOpts.PageNumber - 1) * categoryOpts.PageSize).Take(categoryOpts.PageSize).AsQueryable();

            return categoryList;
        }

        public int GetCategoryCount()
        {
            return _count;
        }

        private Category GetCategoryForUpdateById (Guid id)
        {
            Category category = _context.Categories.FirstOrDefault(e => e.CategoryId == id);

            if (category == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return category;
        }

        public CategoryDto UpdateCategory(Guid CategoryId, CategoryDto categoryDto)
        {
            var oldCategory = GetCategoryForUpdateById(CategoryId);

            if (oldCategory == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
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
