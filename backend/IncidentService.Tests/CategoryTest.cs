using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncidentService.Controllers;
using IncidentService.Data;
using IncidentService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace IncidentService.Tests
{
    public class CategoryTest
    {
        public Mock<ICategoryRepository> mock = new Mock<ICategoryRepository>();

        [Fact]
        public async void GetCategoryByIdAsync()
        {

        }
    }
}
