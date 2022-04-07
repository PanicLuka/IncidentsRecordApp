using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using IncidentService.Controllers;
using IncidentService.Entities;
using IncidentService.Helpers;
using IncidentService.Models;
using IncidentService.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IncidentService.Tests.ControllersTests
{
    public class CategoryControllerTests
    {

        private readonly CategoryController _categoryController;
        private readonly Mock<ICategoriesService> mockCategoriesService = new Mock<ICategoriesService>();

        public CategoryControllerTests()
        {
            _categoryController = new CategoryController(mockCategoriesService.Object);
        }

        /*[Fact]
        public void GetCategories_ReturnsListOfCategories_CategoriesExist()
        {
            // Arrange
            var categoryParameters = new CategoryParameters();
            var categoriesDto = GetSampleCategoryDto(categoryParameters);
            mockCategoriesService.Setup(x => x.GetCategories(categoryParameters)).Returns(GetSampleCategoryDto(categoryParameters));

            // Act
            var actionResult = _categoryController.GetCategories(categoryParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<CategoryDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleCategoryDto(categoryParameters).Count(), actual.Count());
        }*/

        [Fact]
        public void GetCategoryById_ReturnsCategoryWithIdDto_CategoryWithSpecifiedIdExists()
        {
            // Arrange
            var categoryParameters = new CategoryParameters();
            var categories = GetSampleCategory(categoryParameters);
            var firstCategory = categories[0];
            mockCategoriesService.Setup(x => x.GetCategoryById(Guid.Parse("f17d3536-bb13-47ce-bffd-828ed875d254"))).Returns(firstCategory.CategoryToCategoryWithIdDto());

            // Act
            var actionResult = _categoryController.GetCategoryById(Guid.Parse("f17d3536-bb13-47ce-bffd-828ed875d254"));
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(firstCategory);
        }

        [Fact]
        public void GetCategoryById_ReturnsCategoryWithIdDto_CategoryWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var categoryParameters = new CategoryParameters();
            var categories = GetSampleCategory(categoryParameters);
            var firstCategory = categories[0];
            mockCategoriesService.Setup(x => x.GetCategoryById(Guid.Parse("f17d3536-bb13-47ce-bffd-828ed875d254"))).Returns(firstCategory.CategoryToCategoryWithIdDto());

            // Act
            var actionResult = _categoryController.GetCategoryById(Guid.Parse("f55d3536-bb13-47ce-bffd-828ed875d254"));
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        private PagedList<Category> GetSampleCategory(CategoryParameters categoryParameters)
        {
            List<Category> output = new List<Category>
            {
                new Category
                {
                    CategoryId = Guid.Parse("f17d3536-bb13-47ce-bffd-828ed875d254"),
                    CategoryName = "sample1"
                },
                new Category
                {
                    CategoryId = Guid.Parse("1514856c-cf11-4941-87f8-3400e0b304a9"),
                    CategoryName = "sample2"
                }
            };
            IQueryable<Category> queryable = output.AsQueryable();
            return PagedList<Category>.ToPagedList(queryable, categoryParameters.PageNumber, categoryParameters.PageSize);
        }

        private PagedList<CategoryDto> GetSampleCategoryDto(CategoryParameters categoryParameters)
        {
            List<CategoryDto> output = new List<CategoryDto>
            {
                new CategoryDto
                {
                    CategoryName = "sample1"
                },
                new CategoryDto
                {
                    CategoryName = "sample2"
                }
            };
            IQueryable<CategoryDto> queryable = output.AsQueryable();
            return PagedList<CategoryDto>.ToPagedList(queryable, categoryParameters.PageNumber, categoryParameters.PageSize);
        }
    }
}
