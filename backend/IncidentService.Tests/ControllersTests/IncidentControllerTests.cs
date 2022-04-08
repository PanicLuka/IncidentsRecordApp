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
        private readonly Mock<IIncidentsService> mockIncidentsService = new Mock<IIncidentsService>();

        Guid FirstIncidentGuid = Guid.NewGuid();
        Guid SecondIncidentGuid = Guid.NewGuid();
        Guid ThirdIncidentGuid = Guid.NewGuid();
        Guid FourthIncidentGuid = Guid.NewGuid();
        Guid FifthIncidentGuid = Guid.NewGuid();
        Guid SixthIncidentGuid = Guid.NewGuid();
        Guid UserGuid = Guid.NewGuid();
        Guid CategoryGuid = Guid.NewGuid();
        Guid TestGuid = Guid.NewGuid();

        private readonly IncidentParameters _filterParameters = new IncidentParameters
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

        private readonly IncidentParameters _pagedParameters = new IncidentParameters
        {
            PageNumber = 1,
            PageSize = 2
        };
        public IncidentControllerTests()
        {
            _incidentController = new IncidentController(mockIncidentsService.Object);
        }

        [Fact]
        public void GetIncidents_ReturnsAllIncidents_IncidentsExist()
        {
            // Arrange
            var incidentParameters = new IncidentParameters();
            var incidentsDto = GetSampleIncidentDto(incidentParameters);
            mockIncidentsService.Setup(x => x.GetIncidents(incidentParameters)).Returns(GetSampleIncidentDto(incidentParameters));

            // Act
            var actionResult = _incidentController.GetIncidents(incidentParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<IncidentDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleIncidentDto(incidentParameters).Count(), actual.Count());
        }

        [Fact]
        public void GetIncidents_ReturnsPagedListOfIncidents_IncidentsExist()
        {
            // Arrange
            var incidentsDto = GetSampleIncidentDto(_pagedParameters);
            mockIncidentsService.Setup(x => x.GetIncidents(_pagedParameters)).Returns(GetSampleIncidentDto(_pagedParameters));

            // Act
            var actionResult = _incidentController.GetIncidents(_pagedParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<IncidentDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleIncidentDto(_pagedParameters).Count(), actual.Count());
        }

        [Fact]
        public void GetIncidents_ReturnsFilteredPagedListOfIncidents_IncidentsExist()
        {
            // Arrange
            var incidentsDto = GetSampleIncidentDto(_filterParameters);
            mockIncidentsService.Setup(x => x.GetIncidents(_filterParameters)).Returns(GetSampleIncidentDto(_filterParameters));

            // Act
            var actionResult = _incidentController.GetIncidents(_filterParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<IncidentDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleIncidentDto(_filterParameters).Count(), actual.Count());
        }

        [Fact]
        public void GetIncidentById_ReturnsIncidentWithIdDto_IncidentWithSpecifiedIdExists()
        {
            // Arrange
            var incidentParameters = new IncidentParameters();
            var incidents = GetSampleIncident(incidentParameters);
            var firstIncident = incidents[0];
            mockIncidentsService.Setup(x => x.GetIncidentById(FirstIncidentGuid)).Returns(firstIncident.IncidentToIncidentWithIdDto());

            // Act
            var actionResult = _incidentController.GetIncidentById(FirstIncidentGuid);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(firstIncident);
        }

        [Fact]
        public void GetIncidentById_ReturnsIncidentWithIdDto_IncidentWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var incidentParameters = new IncidentParameters();
            var incidents = GetSampleIncident(incidentParameters);
            var firstIncident = incidents[0];
            mockIncidentsService.Setup(x => x.GetIncidentById(FirstIncidentGuid)).Returns(firstIncident.IncidentToIncidentWithIdDto());

            // Act
            var actionResult = _incidentController.GetIncidentById(TestGuid);
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void DeleteIncident_ReturnsNotFound_IncidentWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var incidentParameters = new IncidentParameters();
            mockIncidentsService.Setup(x => x.DeleteIncident(FirstIncidentGuid));

            // Act
            var actionResult = _incidentController.DeleteIncident(TestGuid);

            // Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        [Fact]
        public void UpdateIncident_ReturnsUpdatedIncident_IncidentWithSpecifiedIdExists()
        {
            // Arrange
            var incidentParameters = new IncidentParameters();
            var incidents = GetSampleIncident(incidentParameters);
            var firstIncident = incidents[0];
            var testIncident = firstIncident.IncidentToDto();
            testIncident.Number = "testNumber";
            mockIncidentsService.Setup(x => x.UpdateIncident(firstIncident.IncidentId, testIncident)).Returns(testIncident);

            // Act
            var actionResult = _incidentController.UpdateIncident(firstIncident.IncidentId, testIncident);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(testIncident);
        }

        private PagedList<Incident> GetSampleIncident(IncidentParameters incidentParameters)
        {
            List<Incident> output = new List<Incident>
            {
                new Incident
                {
                    IncidentId = FirstIncidentGuid,
                    Number = "sample1",
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
                    Number = "sample2",
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
                    Number = "sample3",
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
                    Number = "sample4",
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
                    Number = "sample5",
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
                    Number = "sample6",
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
            IQueryable<Incident> queryable = output.AsQueryable();
            return PagedList<Incident>.ToPagedList(queryable, incidentParameters.PageNumber, incidentParameters.PageSize);
        }

        private PagedList<IncidentDto> GetSampleIncidentDto(IncidentParameters incidentParameters)
        {
            List<IncidentDto> output = new List<IncidentDto>
            {
                new IncidentDto
                {
                    Number = "sample1",
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
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Number = "sample2",
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
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Number = "sample3",
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
                new IncidentDto
                {
                    Number = "sample4",
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
                new IncidentDto
                {
                    Number = "sample5",
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
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                },
                new IncidentDto
                {
                    Number = "sample6",
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
                    UserId = UserGuid,
                    CategoryId = CategoryGuid
                }
            };
            IQueryable<IncidentDto> queryable = output.AsQueryable();
            return PagedList<IncidentDto>.ToPagedList(queryable, incidentParameters.PageNumber, incidentParameters.PageSize);
        }
    }
}
