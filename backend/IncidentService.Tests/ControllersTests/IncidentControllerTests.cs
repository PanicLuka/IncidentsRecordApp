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
    public class IncidentControllerTests
    {
        private readonly IncidentController _incidentController;
        private readonly Mock<IIncidentsService> _mockIncidentsService = new Mock<IIncidentsService>();

        Guid FirstIncidentGuid = Guid.NewGuid();
        Guid SecondIncidentGuid = Guid.NewGuid();
        Guid ThirdIncidentGuid = Guid.NewGuid();
        Guid FourthIncidentGuid = Guid.NewGuid();
        Guid FifthIncidentGuid = Guid.NewGuid();
        Guid SixthIncidentGuid = Guid.NewGuid();
        Guid UserGuid = Guid.NewGuid();
        Guid CategoryGuid = Guid.NewGuid();
        Guid TestGuid = Guid.NewGuid();

        private readonly IncidentOpts _filterParameters = new IncidentOpts
        {
            FirstDate = null,
            SecondDate = null,
            SecondSolvingDate = null,
            SecondTime = null,
            FirstSolvingDate = null,
            CategoryId = null,
            UserId = null,
            FurtherAction = false,
            FirstTime = null,
            ThirdPartyHelp = null,
            Significance = null,
            PageNumber = 1,
            PageSize = 2
        };

        private readonly IncidentOpts _pagedParameters = new IncidentOpts
        {
            PageNumber = 1,
            PageSize = 2
        };
        public IncidentControllerTests()
        {
            _incidentController = new IncidentController(_mockIncidentsService.Object);
        }

        /*[Fact]
        public void GetIncidents_ReturnsAllIncidents_IncidentsExist()
        {
            // Arrange
            var incidentOpts = new IncidentOpts();
            var incidentsDto = GetSampleIncidentDto(incidentOpts);
            _mockIncidentsService.Setup(x => x.GetIncidents(incidentOpts)).Returns(GetSampleIncidentWithIdDto(incidentOpts));

            // Act
            var actionResult = _incidentController.GetIncidents(incidentOpts);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<IncidentDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleIncidentWithIdDto(incidentOpts).Count(), actual.Count());
        }

        [Fact]
        public void GetIncidents_ReturnsPagedListOfIncidents_IncidentsExist()
        {
            // Arrange
            var incidentsDto = GetSampleIncidentDto(_pagedParameters);
            _mockIncidentsService.Setup(x => x.GetIncidents(_pagedParameters)).Returns(GetSampleIncidentWithIdDto(_pagedParameters));

            // Act
            var actionResult = _incidentController.GetIncidents(_pagedParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<IncidentDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleIncidentWithIdDto(_pagedParameters).Count(), actual.Count());
        }

        [Fact]
        public void GetIncidents_ReturnsFilteredPagedListOfIncidents_IncidentsExist()
        {
            // Arrange
            var incidentsDto = GetSampleIncidentDto(_filterParameters);
            _mockIncidentsService.Setup(x => x.GetIncidents(_filterParameters)).Returns(GetSampleIncidentWithIdDto(_filterParameters));

            // Act
            var actionResult = _incidentController.GetIncidents(_filterParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<IncidentDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleIncidentWithIdDto(_filterParameters).Count(), actual.Count());
        }*/

        [Fact]
        public void GetIncidentById_ReturnsIncidentWithIdDto_IncidentWithSpecifiedIdExists()
        {
            // Arrange
            var incidentOpts = new IncidentOpts();
            var incidents = GetSampleIncident(incidentOpts);
            var firstIncident = incidents[0];
            _mockIncidentsService.Setup(x => x.GetIncidentById(FirstIncidentGuid)).Returns(firstIncident.IncidentToIncidentWithIdDto());

            // Act
            var actionResult = _incidentController.GetIncidentById(FirstIncidentGuid);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(firstIncident);
        }

        [Fact]
        public void CreateIncident_ReturnsOk_DatabaseExists()
        {
            // Arrange
            var incidentOpts = new IncidentOpts();
            var incidentsDto = GetSampleIncidentDto(incidentOpts);
            var firstIncident = incidentsDto[0];
            _mockIncidentsService.Setup(x => x.CreateIncident(firstIncident, TestGuid));

            // Act
            var actionResult = _incidentController.CreateIncident(firstIncident);

            // Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact]
        public void UpdateIncident_ReturnsUpdatedIncident_IncidentWithSpecifiedIdExists()
        {
            // Arrange
            var incidentOpts = new IncidentOpts();
            var incidents = GetSampleIncident(incidentOpts);
            var firstIncident = incidents[0];
            var testIncident = firstIncident.IncidentToDto();
            testIncident.Designation = "testDesignation";
            _mockIncidentsService.Setup(x => x.UpdateIncident(firstIncident.IncidentId, testIncident)).Returns(testIncident);

            // Act
            var actionResult = _incidentController.UpdateIncident(firstIncident.IncidentId, testIncident);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(testIncident);
        }

        private List<Incident> GetSampleIncident(IncidentOpts incidentOpts)
        {
            List<Incident> output = new List<Incident>
            {
                new Incident
                {
                    IncidentId = FirstIncidentGuid,
                    Designation = "sample1",
                    Significance = 1,
                    Workspace = "sample1",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample1",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample1",
                    FurtherAction = true,
                    FurtherActionPerson = "sample1",
                    ActionDescription = "sample1",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample1",
                    Verifies = "sample1",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new Incident
                {
                    IncidentId = SecondIncidentGuid,
                    Designation = "sample2",
                    Significance = 1,
                    Workspace = "sample2",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample2",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample2",
                    FurtherAction = true,
                    FurtherActionPerson = "sample2",
                    ActionDescription = "sample2",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample2",
                    Verifies = "sample2",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new Incident
                {
                    IncidentId = ThirdIncidentGuid,
                    Designation = "sample3",
                    Significance = 1,
                    Workspace = "sample3",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample3",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample3",
                    FurtherAction = true,
                    FurtherActionPerson = "sample3",
                    ActionDescription = "sample3",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample3",
                    Verifies = "sample3",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new Incident
                {
                    IncidentId = FourthIncidentGuid,
                    Designation = "sample4",
                    Significance = 1,
                    Workspace = "sample4",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample4",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample4",
                    FurtherAction = true,
                    FurtherActionPerson = "sample4",
                    ActionDescription = "sample4",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample4",
                    Verifies = "sample4",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new Incident
                {
                    IncidentId = FifthIncidentGuid,
                    Designation = "sample5",
                    Significance = 1,
                    Workspace = "sample5",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample5",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample5",
                    FurtherAction = true,
                    FurtherActionPerson = "sample5",
                    ActionDescription = "sample5",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample5",
                    Verifies = "sample5",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new Incident
                {
                    IncidentId = SixthIncidentGuid,
                    Designation = "sample6",
                    Significance = 1,
                    Workspace = "sample6",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample6",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample6",
                    FurtherAction = true,
                    FurtherActionPerson = "sample6",
                    ActionDescription = "sample6",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample6",
                    Verifies = "sample6",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                }
            };
            return output;
        }

        private List<IncidentDto> GetSampleIncidentDto(IncidentOpts incidentOpts)
        {
            List<IncidentDto> output = new List<IncidentDto>
            {
                new IncidentDto
                {
                    Designation = "sample1",
                    Significance = 2,
                    Workspace = "sample1",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample1",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample1",
                    FurtherAction = true,
                    FurtherActionPerson = "sample1",
                    ActionDescription = "sample1",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample1",
                    Verifies = "sample1",
                    ReportedBy = "sample1",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Designation = "sample2",
                    Significance = 3,
                    Workspace = "sample2",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample2",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample2",
                    FurtherAction = true,
                    FurtherActionPerson = "sample2",
                    ActionDescription = "sample2",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample2",
                    Verifies = "sample2",
                    ReportedBy = "sample2",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Designation = "sample3",
                    Significance = 1,
                    Workspace = "sample3",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample3",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample3",
                    FurtherAction = true,
                    FurtherActionPerson = "sample3",
                    ActionDescription = "sample3",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample3",
                    Verifies = "sample3",
                    ReportedBy = "sample3",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Designation = "sample4",
                    Significance = 1,
                    Workspace = "sample4",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample4",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample4",
                    FurtherAction = true,
                    FurtherActionPerson = "sample4",
                    ActionDescription = "sample4",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample4",
                    Verifies = "sample4",
                    ReportedBy = "sample4",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Designation = "sample5",
                    Significance = 1,
                    Workspace = "sample5",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample5",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample5",
                    FurtherAction = false,
                    FurtherActionPerson = "sample5",
                    ActionDescription = "sample5",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample5",
                    Verifies = "sample5",
                    ReportedBy = "sample5",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Designation = "sample6",
                    Significance = 1,
                    Workspace = "sample6",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample6",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample6",
                    FurtherAction = false,
                    FurtherActionPerson = "sample6",
                    ActionDescription = "sample6",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample6",
                    Verifies = "sample6",
                    ReportedBy = "sample6",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                }
            };
            return output;
        }

        private List<IncidentWithIdDto> GetSampleIncidentWithIdDto(IncidentOpts incidentOpts)
        {
            List<IncidentWithIdDto> output = new List<IncidentWithIdDto>
            {
                new IncidentWithIdDto
                {
                    IncidentId = FirstIncidentGuid,
                    Designation = "sample1",
                    Significance = 1,
                    Workspace = "sample1",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample1",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample1",
                    FurtherAction = true,
                    FurtherActionPerson = "sample1",
                    ActionDescription = "sample1",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample1",
                    Verifies = "sample1",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentWithIdDto
                {
                    IncidentId = SecondIncidentGuid,
                    Designation = "sample2",
                    Significance = 1,
                    Workspace = "sample2",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample2",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample2",
                    FurtherAction = true,
                    FurtherActionPerson = "sample2",
                    ActionDescription = "sample2",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample2",
                    Verifies = "sample2",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentWithIdDto
                {
                    IncidentId = ThirdIncidentGuid,
                    Designation = "sample3",
                    Significance = 1,
                    Workspace = "sample3",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample3",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample3",
                    FurtherAction = true,
                    FurtherActionPerson = "sample3",
                    ActionDescription = "sample3",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample3",
                    Verifies = "sample3",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentWithIdDto
                {
                    IncidentId = FourthIncidentGuid,
                    Designation = "sample4",
                    Significance = 1,
                    Workspace = "sample4",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample4",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample4",
                    FurtherAction = true,
                    FurtherActionPerson = "sample4",
                    ActionDescription = "sample4",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample4",
                    Verifies = "sample4",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentWithIdDto
                {
                    IncidentId = FifthIncidentGuid,
                    Designation = "sample5",
                    Significance = 1,
                    Workspace = "sample5",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample5",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample5",
                    FurtherAction = true,
                    FurtherActionPerson = "sample5",
                    ActionDescription = "sample5",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample5",
                    Verifies = "sample5",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentWithIdDto
                {
                    IncidentId = SixthIncidentGuid,
                    Designation = "sample6",
                    Significance = 1,
                    Workspace = "sample6",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "sample6",
                    ThirdPartyHelp = true,
                    ProblemSolved = "sample6",
                    FurtherAction = true,
                    FurtherActionPerson = "sample6",
                    ActionDescription = "sample6",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "sample6",
                    Verifies = "sample6",
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                }
            };
            return output;
        }
    }
}
