using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IncidentService.Controllers;
using IncidentService.Entities;
using IncidentService.Helpers;
using IncidentService.Models;
using IncidentService.Services;
using IncidentService.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using Newtonsoft.Json;
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

        [Fact]
        public void GetCategories_ReturnsListOfCategories_CategoriesExist()
        {
            // Arrange
            var categoriesDto = GetSampleCategoryDto();
            mockCategoriesService.Setup(x => x.GetCategories()).Returns(GetSampleCategoryDto);

            // Act
            var actionResult = _categoryController.GetCategories();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<CategoryDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleCategoryDto().Count(), actual.Count());
        }

        [Fact]
        public void GetCategoryById_ReturnsCategoryWithIdDto_CategoryWithSpecifiedIdExists()
        {
            // Arrange
            var categories = GetSampleCategory();
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
            var categories = GetSampleCategory();
            var firstCategory = categories[0];
            mockCategoriesService.Setup(x => x.GetCategoryById(Guid.Parse("f17d3536-bb13-47ce-bffd-828ed875d254"))).Returns(firstCategory.CategoryToCategoryWithIdDto());

            // Act
            var actionResult = _categoryController.GetCategoryById(Guid.Parse("f55d3536-bb13-47ce-bffd-828ed875d254"));
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        private List<Category> GetSampleCategory()
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
            return output;
        }

        private List<CategoryDto> GetSampleCategoryDto()
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
            return output;
        }
    }
}
