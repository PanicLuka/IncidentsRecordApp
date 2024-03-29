﻿using FluentAssertions;
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
        private readonly Mock<IUsersService> _mockUsersService = new Mock<IUsersService>();
        private Guid testGuid = Guid.NewGuid();

        public UserControllerTests()
        {
            _userController = new UserController(_mockUsersService.Object);
        }

      
        [Fact]
        public void GetUserById_ReturnsUserDto_UserWithSpecifiedIdExists()
        {
            // Arrange
            var userOpts = new QueryStringParameters();
            var users = GetSampleUser(userOpts);
            var firstUser = users[0];
            _mockUsersService.Setup(x => x.GetUserById(testGuid)).Returns(firstUser.UserToDto());

            // Act
            var actionResult = _userController.GetUserById(testGuid);
            var result = actionResult.Result as OkObjectResult;
            var secondUser = firstUser.UserToDto();
            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(secondUser);
        }
        [Fact]
        public void UpdateUser_ReturnsUpdatedUser_UserWithSpecifiedIdExists()
        {
            // Arrange
            var userOpts = new QueryStringParameters();
            var users = GetSampleUser(userOpts);
            var firstUser = users[0];
            var testUser = firstUser.UserToDto();
            testUser.FirstName = "testName";
            _mockUsersService.Setup(x => x.UpdateUser(firstUser.UserId, testUser)).Returns(testUser);

            // Act
            var actionResult = _userController.UpdateUser(firstUser.UserId, testUser);
            var result = actionResult.Result as OkObjectResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

            result.Value.Should().BeEquivalentTo(testUser);
        }
        

        private List<User> GetSampleUser(QueryStringParameters userOpts)
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

        private PagedList<UserWithIdDto> GetSampleUserWihtIdDto(UserParameters userParameters)
        {
            List<UserWithIdDto> output = new List<UserWithIdDto>
            {
                new UserWithIdDto
                {
                    UserId = testGuid,
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = "123456",
                },
                new UserWithIdDto
                {
                    UserId = Guid.Parse("066c5bab-e88e-4c72-8062-cabd1b79e916"),
                    FirstName = "Petar",
                    LastName = "Peric",
                    Email = "petar@gmail.com",
                    Password = "123456",
                }
            };
        

            IQueryable<UserWithIdDto> queryable = output.AsQueryable();
            return PagedList<UserWithIdDto>.ToPagedList(queryable, userParameters.PageNumber, userParameters.PageSize);
        }
    }
}
