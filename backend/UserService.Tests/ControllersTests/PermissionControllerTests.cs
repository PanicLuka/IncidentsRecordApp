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
        private readonly Mock<IPermissionService> _mockPermissionService = new Mock<IPermissionService>();
        private Guid testGuid = Guid.NewGuid();

        
        public PermissionControllerTests()
        {
            _permissionController = new PermissionController(_mockPermissionService.Object);
        }

        [Fact]
        public void GetPermissions_ReturnsListOfPermissions_PermissionsExist()
        {
            // Arrange
            var permOpts = new PermissionParameters();
            var permissionDto = GetSamplePermissionDto(permOpts);
            _mockPermissionService.Setup(x => x.GetAllPermissions(permOpts)).Returns(GetSamplePermissionDto(permOpts));

            // Act
            var actionResult = _permissionController.GetAllPermissions(permOpts);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<PermissionDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSamplePermissionDto(permOpts).Count(), actual.Count());
        }

        [Fact]
        public void GetPermissionById_ReturnsPermissionDto_PermissionWithSpecifiedIdExists()
        {
            // Arrange
            var permOpts = new PermissionParameters();

            var permissions = GetSamplePermission(permOpts);
            var firstPermission = permissions[0];
            
            _mockPermissionService.Setup(x => x.GetPermissionById(testGuid)).Returns(firstPermission.PermissionToDto());

            // Act
            var actionResult = _permissionController.GetPermissionById(testGuid);
            var result = actionResult.Result as OkObjectResult;
            var secondPermission = firstPermission.PermissionToDto();
            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(secondPermission);
        }

        [Fact]
        public void UpdatePermission_ReturnsUpdatedPermission_PermissionWithSpecifiedIdExists()
        {
            // Arrange
            var permissionOpts = new QueryStringParameters();
            var permissions = GetSamplePermission(permissionOpts);
            var firstPermission = permissions[0];
            var testPermission = firstPermission.PermissionToDto();
            testPermission.AccessPermission = "testName";
            _mockPermissionService.Setup(x => x.UpdatePermission(firstPermission.PermissionId, testPermission)).Returns(testPermission);

            // Act
            var actionResult = _permissionController.UpdatePermission(firstPermission.PermissionId, testPermission);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(testPermission);
        }
        
        private List<Permission> GetSamplePermission(QueryStringParameters permissionOpts)
        {
            List<Permission> output = new List<Permission>
            {
                new Permission
                {
                   PermissionId = testGuid,
                    AccessPermission = "UserGetById"
                },
                new Permission
                {
                   PermissionId = Guid.NewGuid(),
                    AccessPermission = "UsersGetAll"
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
                  AccessPermission = "UserGetById"
                },
                new PermissionDto
                {
                    AccessPermission = "UserGetAll"
                }
            };

            IQueryable<PermissionDto> queryable = output.AsQueryable();
            return PagedList<PermissionDto>.ToPagedList(queryable, permissionParameters.PageNumber, permissionParameters.PageSize);
        }
    }
}

