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
        private readonly Mock<IRoleService> _mockRoleService = new Mock<IRoleService>();
        private Guid testGuid = Guid.NewGuid();
        public RoleControllerTests()
        {
            _roleController = new RoleController(_mockRoleService.Object);
        }

        [Fact]
        public void GetRoles_ReturnsListOfRoles_RolesExist()
        {
            // Arrange
            var roleOpts = new QueryStringParameters();
            var rolesDto = GetSampleRoleDto(roleOpts);
            _mockRoleService.Setup(x => x.GetAllRoles()).Returns(GetSampleRoleDto(roleOpts));

            // Act
            var actionResult = _roleController.GetAllRoles();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<RoleDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleRoleDto(roleOpts).Count(), actual.Count());
        }

        [Fact]
        public void GetRoleById_ReturnsRoleDto_RoleWithSpecifiedIdExists()
        {
            // Arrange
            var roleOpts = new QueryStringParameters();
            var roles = GetSampleRole(roleOpts);
            var firstRole = roles[0];
            _mockRoleService.Setup(x => x.GetRoleById(testGuid)).Returns(firstRole.RoleToDto());

            // Act
            var actionResult = _roleController.GetRoleById(testGuid);
            var result = actionResult.Result as OkObjectResult;
            var secondRole = firstRole.RoleToDto();
            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(secondRole);
        }

        [Fact]
        public void UpdateRole_ReturnsUpdatedRole_RoleWithSpecifiedIdExists()
        {
            // Arrange
            var roleOpts = new QueryStringParameters();
            var roles = GetSampleRole(roleOpts);
            var firstRole = roles[0];
            var testRole = firstRole.RoleToDto();
            testRole.UserType = "testName";
            _mockRoleService.Setup(x => x.UpdateRole(firstRole.RoleId, testRole)).Returns(testRole);

            // Act
            var actionResult = _roleController.UpdateRole(firstRole.RoleId, testRole);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(testRole);
        }
       
        private List<Role> GetSampleRole(QueryStringParameters roleOpts)
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

        private List<RoleDto> GetSampleRoleDto(QueryStringParameters roleOpts)
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

