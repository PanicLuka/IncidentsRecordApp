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
            mockRoleService.Setup(x => x.GetRoleById(Guid.Parse("e17ba131-8407-4db5-a34a-a630c58718b3"))).Returns(firstRole.RoleToDto());

            // Act
            var actionResult = _roleController.GetRoleById(Guid.Parse("e17ba131-8407-4db5-a34a-a630c58718b3"));
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
            mockRoleService.Setup(x => x.GetRoleById(Guid.Parse("e17ba131-8407-4db5-a34a-a630c58718b3"))).Returns(firstRole.RoleToDto());

            // Act
            var actionResult = _roleController.GetRoleById(Guid.Parse("8781d187-0951-4978-a258-b3871323c381"));
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
                    RoleId = Guid.Parse("e17ba131-8407-4db5-a34a-a630c58718b3"),
                    UserType = "User"
                },
                new Role
                {
                   RoleId = Guid.Parse("7b4f91b1-c264-4654-a860-4c16e4dd259c"),
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

