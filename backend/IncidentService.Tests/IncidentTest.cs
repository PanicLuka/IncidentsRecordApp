using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IncidentService.Data;
using Moq;
using Xunit;

namespace IncidentService.Tests
{
    public class IncidentTest
    {
        public Mock<IIncidentRepository> mock = new Mock<IIncidentRepository>();

        [Fact]
        public async void GetIncidentByIdAsync()
        {

        }
    }
}
