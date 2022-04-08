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
        private Guid testGuid = Guid.NewGuid();

        
        public PermissionControllerTests()
        {
            _permissionController = new PermissionController(mockPermissionService.Object);
        }

        [Fact]
        public void GetPermissions_ReturnsListOfPermissions_PermissionsExist()
        {
            // Arrange
            var permissionParameters = new PermissionParameters();
            var permissionDto = GetSamplePermissionDto(permissionParameters);
            mockPermissionService.Setup(x => x.GetAllPermissions(permissionParameters)).Returns(GetSamplePermissionDto(permissionParameters));

            // Act
            var actionResult = _permissionController.GetAllPermissions(permissionParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<PermissionDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSamplePermissionDto(permissionParameters).Count(), actual.Count());
        }

        [Fact]
        public void GetPermissionById_ReturnsPermissionDto_PermissionWithSpecifiedIdExists()
        {
            // Arrange
            var permissions = GetSamplePermission();
            var firstPermission = permissions[0];
            
            mockPermissionService.Setup(x => x.GetPermissionById(testGuid)).Returns(firstPermission.PermissionToDto());

            // Act
            var actionResult = _permissionController.GetPermissionById(testGuid);
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
            mockPermissionService.Setup(x => x.GetPermissionById(testGuid)).Returns(firstPermission.PermissionToDto());

            // Act
            var actionResult = _permissionController.GetPermissionById(Guid.NewGuid());
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
                   PermissionId = testGuid,
                    AccessPermissions = "UserGetById"
                },
                new Permission
                {
                   PermissionId = Guid.NewGuid(),
                    AccessPermissions = "UsersGetAll"
                }
            };
            return output;
        }

        private PagedList<PermissionDto> GetSamplePermissionDto(PermissionParameters permissionParameters)
        {
            List<PermissionDto> output = new List<PermissionDto>
            {
                new PermissionDto
                {
                  AccessPermissions = "UserGetById"
                },
                new PermissionDto
                {
                    AccessPermissions = "UserGetAll"
                }
            };

            IQueryable<PermissionDto> queryable = output.AsQueryable();
            return PagedList<PermissionDto>.ToPagedList(queryable, permissionParameters.PageNumber, permissionParameters.PageSize);
        }
    }
}

