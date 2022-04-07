﻿using System;
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

        public IncidentControllerTests()
        {
            _incidentController = new IncidentController(mockIncidentsService.Object);
        }

        /*[Fact]
        public void GetIncidents_ReturnsListOfIncidents_IncidentsExist()
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
        }*/

        [Fact]
        public void GetIncidentById_ReturnsIncidentWithIdDto_IncidentWithSpecifiedIdExists()
        {
            // Arrange
            var incidentParameters = new IncidentParameters();
            var incidents = GetSampleIncident(incidentParameters);
            var firstIncident = incidents[0];
            mockIncidentsService.Setup(x => x.GetIncidentById(Guid.Parse("e0624bc9-66a9-44a6-8f7f-8f0a5730d8d6"))).Returns(firstIncident.IncidentToIncidentWithIdDto());

            // Act
            var actionResult = _incidentController.GetIncidentById(Guid.Parse("e0624bc9-66a9-44a6-8f7f-8f0a5730d8d6"));
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
            mockIncidentsService.Setup(x => x.GetIncidentById(Guid.Parse("e0624bc9-66a9-44a6-8f7f-8f0a5730d8d6"))).Returns(firstIncident.IncidentToIncidentWithIdDto());

            // Act
            var actionResult = _incidentController.GetIncidentById(Guid.Parse("e5524bc9-66a9-44a6-8f7f-8f0a5730d8d6"));
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        private PagedList<Incident> GetSampleIncident(IncidentParameters incidentParameters)
        {
            List<Incident> output = new List<Incident>
            {
                new Incident
                {
                    IncidentId = Guid.Parse("e0624bc9-66a9-44a6-8f7f-8f0a5730d8d6"),
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
                    UserId = Guid.Parse("df2a60f1-e711-4a91-bccd-08188b54440b"),
                    CategoryId = Guid.Parse("df2a59f1-e711-4a91-bccd-08188b54440b")
                },
                new Incident
                {
                    IncidentId = Guid.Parse("6ac7eee9-e40d-40be-a3c1-419be141b019"),
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
                    UserId = Guid.Parse("df2a60f1-e711-4a91-bccd-08188b54440b"),
                    CategoryId = Guid.Parse("df2a59f1-e711-4a91-bccd-08188b54440b")
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
                    UserId = Guid.Parse("df2a60f1-e711-4a91-bccd-08188b54440b"),
                    CategoryId = Guid.Parse("df2a59f1-e711-4a91-bccd-08188b54440b")
                },
                new IncidentDto
                {
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
                    UserId = Guid.Parse("df2a60f1-e711-4a91-bccd-08188b54440b"),
                    CategoryId = Guid.Parse("df2a59f1-e711-4a91-bccd-08188b54440b")
                }
            };
            IQueryable<IncidentDto> queryable = output.AsQueryable();
            return PagedList<IncidentDto>.ToPagedList(queryable, incidentParameters.PageNumber, incidentParameters.PageSize);
        }
    }
}
