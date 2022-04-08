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

        public UserControllerTests()
        {
            _userController = new UserController(mockUsersService.Object);
        }

        [Fact]
        public void GetUsers_ReturnsListOfUsers_UsersExist()
        {
            // Arrange
            var usersDto = GetSampleUserDto();
            mockUsersService.Setup(x => x.GetAllUsers()).Returns(GetSampleUserDto);

            // Act
            var actionResult = _userController.GetUsers();
            var result = actionResult.Result as OkObjectResult;
            var actual = result.Value as IEnumerable<UserDto>;

            // Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(GetSampleUserDto().Count(), actual.Count());
        }

        [Fact]
        public void GetUserById_ReturnsUserDto_UserWithSpecifiedIdExists()
        {
            // Arrange
            var users = GetSampleUser();
            var firstUser = users[0];
            mockUsersService.Setup(x => x.GetUserById(Guid.Parse("2d678968-325e-49e8-83f3-cf5ba21efdd6"))).Returns(firstUser.UserToDto());

            // Act
            var actionResult = _userController.GetUserById(Guid.Parse("2d678968-325e-49e8-83f3-cf5ba21efdd6"));
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
            mockUsersService.Setup(x => x.GetUserById(Guid.Parse("2d678968-325e-49e8-83f3-cf5ba21efdd6"))).Returns(firstUser.UserToDto());

            // Act
            var actionResult = _userController.GetUserById(Guid.Parse("8781d187-0951-4978-a258-b3871323c382"));
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
                    UserId = Guid.Parse("2d678968-325e-49e8-83f3-cf5ba21efdd6"),
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = "123456",
                    RoleId = Guid.Parse("12ff9586-1ae4-46ca-be4d-6633bc60ca14"),
                },
                new User
                {
                    UserId = Guid.Parse("066c5bab-e88e-4c72-8062-cabd1b79e916"),
                    FirstName = "Petar",
                    LastName = "Peric",
                    Email = "petar@gmail.com",
                    Password = "123456",
                    RoleId = Guid.Parse("12ff9586-1ae4-46ca-be4d-6633bc60ca14")
                }
            };
            return output;
        }

        private List<UserDto> GetSampleUserDto()
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
            return output;
        }
    }
}
