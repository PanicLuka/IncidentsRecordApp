using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncidentService.Controllers;
using IncidentService.Services;
using IncidentService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IncidentService.Tests
{
    public class CategoriesServiceTests
    {
        public Mock<ICategoriesService> mock = new Mock<ICategoriesService>();

        [Fact]
        public async void GetCategoryByIdAsync()
        {

        }
    }
}
