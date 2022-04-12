using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Configuration;
using IncidentService.Entities;
using IncidentService.Helpers;
using IncidentService.Models;
using IncidentService.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace IncidentService.Tests.ServicesTests
{
    public class CategoriesServiceTests
    {
        private readonly Mock<DataContext> _mockDataContext = new Mock<DataContext>();
        private readonly Mock<DbSet<Category>> _mockDbSet = new Mock<DbSet<Category>>();
        private CategoriesService _categoriesService;

        Guid FirstCategoryGuid = Guid.NewGuid();
        Guid SecondCategoryGuid = Guid.NewGuid();
        Guid ThirdCategoryGuid = Guid.NewGuid();
        Guid FourthCategoryGuid = Guid.NewGuid();
        Guid FifthCategoryGuid = Guid.NewGuid();
        Guid SixthCategoryGuid = Guid.NewGuid();
        Guid TestGuid = Guid.NewGuid();

        public CategoriesServiceTests()
        {
            _categoriesService = new CategoriesService(_mockDataContext.Object);
        }

        /*[Fact]
        public void GetCategoryById_ReturnsAllOfCategories_CategoriesExist()
        {
            // Arrange
            var categoryOpts = new CategoryOpts();
            var categories = GetSampleCategory(categoryOpts);
            var firstCategory = categories[0];
            _mockDbSet.Setup(s => s.Find(FirstCategoryGuid)).Returns(firstCategory);
            _mockDataContext.Setup(s => s.Set<Category>()).Returns(_mockDbSet.Object);

            // Act
            var category = _categoriesService.GetCategoryById(FirstCategoryGuid);

            // Assert
            Assert.NotNull(category);
            Assert.IsAssignableFrom<CategoryWithIdDto>(category);
        }*/

        /*[Fact]
        public void GetCategoryById_ReturnsAllOfCategories_CategoriesExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: "incidentsDatabase")
            .Options;

            var categoryOpts = new CategoryOpts();

            // Insert seed data into the database using one instance of the context
            using (var context = new DataContext(options))
            {
                context.Categories.Add(new Category { CategoryId = FirstCategoryGuid, CategoryName = "sample1"});
                context.Categories.Add(new Category { CategoryId = SecondCategoryGuid, CategoryName = "sample2" });
                context.Categories.Add(new Category { CategoryId = ThirdCategoryGuid, CategoryName = "sample3" });
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new DataContext(options))
            {
                CategoriesService categoriesService = new CategoriesService(context);
                PagedList<CategoryDto> movies = categoriesService.GetCategories(categoryOpts);
    
            Assert.Equal(3, movies.Count);
            }
        }*/

        private PagedList<Category> GetSampleCategory(CategoryOpts categoryOpts)
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
            return PagedList<Category>.ToPagedList(queryable, categoryOpts.PageNumber, categoryOpts.PageSize);
        }

        private PagedList<CategoryDto> GetSampleCategoryDto(CategoryOpts categoryOpts)
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
            return PagedList<CategoryDto>.ToPagedList(queryable, categoryOpts.PageNumber, categoryOpts.PageSize);
        }
    }
}
