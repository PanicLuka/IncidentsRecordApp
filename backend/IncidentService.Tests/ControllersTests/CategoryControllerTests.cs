using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using IncidentService.Controllers;
using IncidentService.Entities;
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

        /*private readonly CategoryController _categoryController;
        private readonly Mock<ICategoriesService> service = new Mock<ICategoriesService>();

        public CategoryControllerTests()
        {
            _categoryController = new CategoryController(service.Object);
        }*/
        
        /*[Fact]
        public async Task DeleteCategories_ReturnsNoContent()
        {

            service.SetupGet(mock => mock.DeleteCategoryAsync(It.IsAny<int>())).Returns(Task.FromResult(0));
            // arrange
            CategoryController categoryController = new CategoryController(service.Object, validator.Object);
            
            //act
            var actionResult = await categoryController.DeleteCategoryAsync(7);
            var statusCodeResult = (IStatusCodeActionResult)actionResult;
            
            //assert
            Assert.Equal(StatusCodes.Status204NoContent, statusCodeResult.StatusCode);
        }*/

        /*[Fact]
        public async Task GetCategoriesByIdAsync_ReturnsCategoryDto_CategoryWithSpecificeIdExistsAsync()
        {
            // Arrange

            // Act

            // Assert
        }*/

        /*[Fact]
        public void Put_Test()
        {
            CategoryDto r = new CategoryDto()
            {
                CategoryName = "new name",
            };
            var mockRepo = new Mock<ICategoriesService>();
            mockRepo.Setup(repo => repo.UpdateCategoryAsync(It.IsAny<Category>()));
            var controller = new CategoryController(mockRepo.Object, validator.Object);

            // Act
            var result = controller.UpdateCategoryAsync(7, r);

            // Assert
            var category = Assert.IsType<CategoryDto>(result);
            Assert.Equal("new name", category.CategoryName);
        }*/

        /*[Fact]
        public async Task GetCategoryByIdAsync_CategoryDto_CategoryWithSpecificeIdExistsAsync()
        {
            //arrange
            var categories = await GetSampleCategory();
            var firstCategory = categories[0];
            service.Setup(x => x.GetCategoryByIdAsync(1)).Returns(firstCategory);
            var controller = new CategoryController(service.Object, validator.Object);

            //act
            var actionResult = controller.GetCategoryByIdAsync(1);
            var result = actionResult.Result as OkObjectResult;

            //Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(firstCategory);
        }*/
        /*private async Task<List<Category>> GetSampleCategory()
        {
            List<Category> output = new List<Category>
            {
                new Category
                {
                    CategoryId = 1,
                    CategoryName = "sample1"
                },
                new Category
                {
                    CategoryId = 2,
                    CategoryName = "sample2"
                }
            };
            return output;
        }*/
    }
}
