using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Helpers;
using UserService.Models;
using UserService.Validators;
using BC = BCrypt.Net.BCrypt;

namespace UserService.Service
{
    public class UsersService : IUsersService
    {
        private readonly DataContext context;
        private readonly UserValidator userValidator;

        public UsersService(DataContext context, UserValidator userValidator)
        {
            this.context = context;
            this.userValidator = userValidator;
        }

        public void CreateUser(UserDto userDto)
        {
            userValidator.ValidateAndThrow(userDto);

            User userEntity = userDto.DtoToUser();

            userEntity.Password = BC.HashPassword(userEntity.Password);

            context.Add(userEntity);

            SaveChanges();
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetUserByIdHelper(userId);

            context.Remove(user);

            SaveChanges();
        }

        public List<UserDto> GetAllUsers()
        {
            var users  = context.User.ToList();

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {     
                UserDto userDto = user.UserToDto();

                userDtos.Add(userDto);
            }

            return userDtos;
        }

       
        public UserDto GetUserByEmail(string email)
        {
            var user = context.User.FirstOrDefault(e => e.Email == email);

            var userDto = user.UserToDto();

            return userDto;
        }

        public UserDto GetUserById(Guid userId)
        {
            var user = context.User.FirstOrDefault(e => e.UserId == userId);

            var userDto = user.UserToDto();

            return userDto;
        }

        public Guid GetUserIdByEmail(string email)
        {
            var user = context.User.FirstOrDefault(u => u.Email == email);

            Guid userId = user.UserId;

            return userId;
        }
        public bool SaveChanges()
        {
           return context.SaveChanges() > 0;
        }

        public UserDto UpdateUser(Guid userId, UserDto userDto)
        {
            var oldUserDto = GetUserByIdHelper(userId);

            if (oldUserDto == null)
            {
                CreateUser(userDto);
                return oldUserDto.UserToDto();
            }
            else
            {
                User user = userDto.DtoToUser();

                oldUserDto.FirstName = user.FirstName;
                oldUserDto.LastName = user.LastName;
                oldUserDto.Email = user.Email;
                oldUserDto.Password = user.Password;


                SaveChanges();
                return oldUserDto.UserToDto();
            }

        }

        public Guid GetRoleIdByUserEmail(string email)
        {
            var user = context.User.FirstOrDefault(u => u.Email == email);

            Guid roleId = user.RoleId;

            return roleId;
        }

        private User GetUserByIdHelper(Guid userId)
        {
            var user = context.User.FirstOrDefault(e => e.UserId == userId);

            return user;
        }

    }
}
