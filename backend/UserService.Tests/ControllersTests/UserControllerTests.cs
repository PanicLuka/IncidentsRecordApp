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
    public class UserControllerTests
    {
        private readonly UserController _userController;
        private readonly Mock<IUsersService> mockUsersService = new Mock<IUsersService>();
        private Guid testGuid = Guid.NewGuid();

        public UserControllerTests()
        {
            _userController = new UserController(mockUsersService.Object);
        }

        [Fact]
        public void GetUsers_ReturnsListOfUsers_UsersExist()
        {
            // Arrange
            var userParameters = new UserParameters();
            var usersDto = GetSampleUserDto(userParameters);
            mockUsersService.Setup(x => x.GetAllUsers(userParameters)).Returns(GetSampleUserDto(userParameters));

            // Act
            var actionResult = _userController.GetUsers(userParameters);
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<UserDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleUserDto(userParameters).Count(), actual.Count());
        }

        [Fact]
        public void GetUserById_ReturnsUserDto_UserWithSpecifiedIdExists()
        {
            // Arrange
            var users = GetSampleUser();
            var firstUser = users[0];
            mockUsersService.Setup(x => x.GetUserById(testGuid)).Returns(firstUser.UserToDto());

            // Act
            var actionResult = _userController.GetUserById(testGuid);
            var result = actionResult.Result as OkObjectResult;
            var secondUser = firstUser.UserToDto();
            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(secondUser);
        }

        [Fact]
        public void GetUserById_ReturnsUserDto_UserWithSpecifiedIdDoesNotExists()
        {
            // Arrange
            var users = GetSampleUser();
            var firstUser = users[0];
            mockUsersService.Setup(x => x.GetUserById(testGuid)).Returns(firstUser.UserToDto());

            // Act
            var actionResult = _userController.GetUserById(Guid.NewGuid());
            var result = actionResult.Result;

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }



        private List<User> GetSampleUser()
        {
            List<User> output = new List<User>
            {
                new User
                {
                    UserId = testGuid,
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = "123456",
                    RoleId = Guid.NewGuid(),
                },
                new User
                {
                    UserId = Guid.Parse("066c5bab-e88e-4c72-8062-cabd1b79e916"),
                    FirstName = "Petar",
                    LastName = "Peric",
                    Email = "petar@gmail.com",
                    Password = "123456",
                    RoleId = Guid.NewGuid()
                }
            };
            return output;
        }

        private PagedList<UserDto> GetSampleUserDto(UserParameters userParameters)
        {
            List<UserDto> output = new List<UserDto>
            {
                new UserDto
                {
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = "123456"
                },
                new UserDto
                {
                    FirstName = "Petar",
                    LastName = "Peric",
                    Email = "petar@gmail.com",
                    Password = "123456"
                }
            };

            IQueryable<UserDto> queryable = output.AsQueryable();
            return PagedList<UserDto>.ToPagedList(queryable, userParameters.PageNumber, userParameters.PageSize);
        }
    }
}
