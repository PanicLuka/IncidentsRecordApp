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

        Guid FirstCategoryGuid = Guid.NewGuid();
        Guid SecondCategoryGuid = Guid.NewGuid();
        Guid ThirdCategoryGuid = Guid.NewGuid();
        Guid FourthCategoryGuid = Guid.NewGuid();
        Guid FifthCategoryGuid = Guid.NewGuid();
        Guid SixthCategoryGuid = Guid.NewGuid();
        Guid TestGuid = Guid.NewGuid();

        private readonly CategoryParameters _parameters = new CategoryParameters
        {
            PageNumber = 1,
            PageSize = 2
        };

        public CategoryControllerTests()
        {
            _categoryController = new CategoryController(mockCategoriesService.Object);
        }

        [Fact]
        public void GetCategories_ReturnsAllOfCategories_CategoriesExist()
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
        }

        [Fact]
        public void GetCategories_ReturnsPagedListOfCategories_CategoriesExist()
        {
            // Arrange
            var categoriesDto = GetSampleCategoryDto(_parameters);
            mockCategoriesService.Setup(x => x.GetCategories(_parameters)).Returns(GetSampleCategoryDto(_parameters));

            // Act
            var actionResult = _categoryController.GetCategories(_parameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<CategoryDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleCategoryDto(_parameters).Count(), actual.Count());
        }

        [Fact]
        public void GetCategoryById_ReturnsCategoryWithIdDto_CategoryWithSpecifiedIdExists()
        {
            // Arrange
            var categoryParameters = new CategoryParameters();
            var categories = GetSampleCategory(categoryParameters);
            var firstCategory = categories[0];
            mockCategoriesService.Setup(x => x.GetCategoryById(FirstCategoryGuid)).Returns(firstCategory.CategoryToCategoryWithIdDto());

            // Act
            var actionResult = _categoryController.GetCategoryById(FirstCategoryGuid);
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
            mockCategoriesService.Setup(x => x.GetCategoryById(FirstCategoryGuid)).Returns(firstCategory.CategoryToCategoryWithIdDto());

            // Act
            var actionResult = _categoryController.GetCategoryById(TestGuid);
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteCategory_ReturnsNotFound_CategoryWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var categoryParameters = new CategoryParameters();
            mockCategoriesService.Setup(x => x.DeleteCategory(FirstCategoryGuid));

            // Act
            var actionResult = _categoryController.DeleteCategory(TestGuid);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void UpdateCategory_ReturnsUpdatedCategory_CategoryWithSpecifiedIdExists()
        {
            // Arrange
            var categoryParameters = new CategoryParameters();
            var categories = GetSampleCategory(categoryParameters);
            var firstCategory = categories[0];
            var testCategory = firstCategory.CategoryToDto();
            testCategory.CategoryName = "testName";
            mockCategoriesService.Setup(x => x.UpdateCategory(firstCategory.CategoryId, testCategory)).Returns(testCategory);

            // Act
            var actionResult = _categoryController.UpdateCategory(firstCategory.CategoryId, testCategory);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(testCategory);
        }

        private PagedList<Category> GetSampleCategory(CategoryParameters categoryParameters)
        {
            List<Category> output = new List<Category>
            {
                new Category
                {
                    CategoryId = FirstCategoryGuid,
                    CategoryName = "sample1"
                },
                new Category
                {
                    CategoryId = SecondCategoryGuid,
                    CategoryName = "sample2"
                },
                new Category
                {
                    CategoryId = ThirdCategoryGuid,
                    CategoryName = "sample3"
                },
                new Category
                {
                    CategoryId = FourthCategoryGuid,
                    CategoryName = "sample4"
                },
                new Category
                {
                    CategoryId = FifthCategoryGuid,
                    CategoryName = "sample5"
                },
                new Category
                {
                    CategoryId = SixthCategoryGuid,
                    CategoryName = "sample6"
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
                },
                new CategoryDto
                {
                    CategoryName = "sample3"
                },
                new CategoryDto
                {
                    CategoryName = "sample4"
                },
                new CategoryDto
                {
                    CategoryName = "sample5"
                },
                new CategoryDto
                {
                    CategoryName = "sample6"
                }
            };
            IQueryable<CategoryDto> queryable = output.AsQueryable();
            return PagedList<CategoryDto>.ToPagedList(queryable, categoryParameters.PageNumber, categoryParameters.PageSize);
        }
    }
}
