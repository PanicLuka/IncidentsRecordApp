using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncidentService.Services;
using Moq;
using Xunit;

namespace IncidentService.Tests
{
    public class IncidentsServiceTests
    {
        public Mock<IIncidentsService> mock = new Mock<IIncidentsService>();

        [Fact]
        public async void GetIncidentByIdAsync()
        {

        }
    }
}
