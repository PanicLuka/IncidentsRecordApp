using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Controllers;
using UserService.Enitites;
using UserService.Helpers;
using UserService.Models;
using UserService.Service;
using Xunit;

namespace UserService.Tests.ControllersTests
{
    public class PermissionControllerTests
    {
        private readonly PermissionController _permissionController;
        private readonly Mock<IPermissionService> mockPermissionService = new Mock<IPermissionService>();

        public PermissionControllerTests()
        {
            _permissionController = new PermissionController(mockPermissionService.Object);
        }

        [Fact]
        public void GetPermissions_ReturnsListOfPermissions_PermissionsExist()
        {
            // Arrange
            var permissionDto = GetSamplePermissionDto();
            mockPermissionService.Setup(x => x.GetAllPermissions()).Returns(GetSamplePermissionDto);

            // Act
            var actionResult = _permissionController.GetAllPermissions();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<PermissionDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSamplePermissionDto().Count(), actual.Count());
        }

        [Fact]
        public void GetPermissionById_ReturnsPermissionDto_PermissionWithSpecifiedIdExists()
        {
            // Arrange
            var permissions = GetSamplePermission();
            var firstPermission = permissions[0];
            mockPermissionService.Setup(x => x.GetPermissionById(Guid.Parse("adcd6be0-76b5-4a38-8877-f1e1764cd591"))).Returns(firstPermission.PermissionToDto());

            // Act
            var actionResult = _permissionController.GetPermissionById(Guid.Parse("adcd6be0-76b5-4a38-8877-f1e1764cd591"));
            var result = actionResult.Result as OkObjectResult;
            var secondPermission = firstPermission.PermissionToDto();
            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(secondPermission);
        }

        [Fact]
        public void GetPermissionById_ReturnsPermissionDto_PermissionWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var permissions = GetSamplePermission();
            var firstPermission = permissions[0];
            mockPermissionService.Setup(x => x.GetPermissionById(Guid.Parse("adcd6be0-76b5-4a38-8877-f1e1764cd591"))).Returns(firstPermission.PermissionToDto());

            // Act
            var actionResult = _permissionController.GetPermissionById(Guid.Parse("8781d187-0951-4978-a258-b3871323c381"));
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        private List<Permission> GetSamplePermission()
        {
            List<Permission> output = new List<Permission>
            {
                new Permission
                {
                   PermissionId = Guid.Parse("adcd6be0-76b5-4a38-8877-f1e1764cd591"),
                    AccessPermissions = "CanAccessGetByIdUser"
                },
                new Permission
                {
                   PermissionId = Guid.Parse("0a3bd2ce-b980-4591-8ff2-db5bf7633294"),
                    AccessPermissions = "CanAccessGetAllUsers"
                }
            };
            return output;
        }

        private List<PermissionDto> GetSamplePermissionDto()
        {
            List<PermissionDto> output = new List<PermissionDto>
            {
                new PermissionDto
                {
                  AccessPermissions = "CanAccessGetByIdUser"
                },
                new PermissionDto
                {
                    AccessPermissions = "CanAccessGetAllUsers"
                }
            };
            return output;
        }
    }
}

