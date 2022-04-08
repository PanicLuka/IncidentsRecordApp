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
    public class RoleControllerTests
    {
        private readonly RoleController _roleController;
        private readonly Mock<IRoleService> mockRoleService = new Mock<IRoleService>();
        private Guid testGuid = Guid.NewGuid();
        public RoleControllerTests()
        {
            _roleController = new RoleController(mockRoleService.Object);
        }

        [Fact]
        public void GetRoles_ReturnsListOfRoles_RolesExist()
        {
            // Arrange
            var rolesDto = GetSampleRoleDto();
            mockRoleService.Setup(x => x.GetAllRoles()).Returns(GetSampleRoleDto);

            // Act
            var actionResult = _roleController.GetAllRoles();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<RoleDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleRoleDto().Count(), actual.Count());
        }

        [Fact]
        public void GetRoleById_ReturnsRoleDto_RoleWithSpecifiedIdExists()
        {
            // Arrange
            var roles = GetSampleRole();
            var firstRole = roles[0];
            mockRoleService.Setup(x => x.GetRoleById(testGuid)).Returns(firstRole.RoleToDto());

            // Act
            var actionResult = _roleController.GetRoleById(testGuid);
            var result = actionResult.Result as OkObjectResult;
            var secondRole = firstRole.RoleToDto();
            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(secondRole);
        }

        [Fact]
        public void GetRoleById_ReturnsRoleDto_RoleWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var roles = GetSampleRole();
            var firstRole = roles[0];
            mockRoleService.Setup(x => x.GetRoleById(testGuid)).Returns(firstRole.RoleToDto());

            // Act
            var actionResult = _roleController.GetRoleById(Guid.NewGuid());
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        private List<Role> GetSampleRole()
        {
            List<Role> output = new List<Role>
            {
                new Role
                {
                    RoleId = testGuid,
                    UserType = "User"
                },
                new Role
                {
                   RoleId = Guid.NewGuid(),
                    UserType = "Admin"
                }
            };
            return output;
        }

        private List<RoleDto> GetSampleRoleDto()
        {
            List<RoleDto> output = new List<RoleDto>
            {
                new RoleDto
                {
                   UserType = "User"
                },
                new RoleDto
                {
                   UserType = "Admin"
                }
            };
            return output;
        }
    }
}

