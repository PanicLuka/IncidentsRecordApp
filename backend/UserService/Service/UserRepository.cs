
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using UserService.Enitites;
using UserService.Helpers;
using UserService.Models;
using UserService.Validators;
using BC = BCrypt.Net.BCrypt;

namespace UserService.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        private readonly UserValidator userValidator;

        public UserRepository(UserContext context, UserValidator userValidator)
        {
            this.context = context;
            this.userValidator = userValidator;
        }

        public void CreateUser(UserDto UserDto)
        {

            userValidator.ValidateAndThrow(UserDto);

            User userEntity = UserDto.DtoToUser();

            userEntity.Password = BC.HashPassword(userEntity.Password);

            context.Add(userEntity);

            SaveChanges();
        }

        public void DeleteUser(Guid UserId)
        {
            var user = GetUserByIdHelper(UserId);

            context.Remove(user);

            SaveChanges();
        }

        public List<UserDto> GetAllUsers()
        {
            var users  = context.register.ToList();

            List<UserDto> userDtos = new List<UserDto>();

            foreach (var user in users)
            {
               
                UserDto userDto = user.UserToDto();

                userDtos.Add(userDto);
            }

            return userDtos;
        }

        

        public Guid GetRoleIdByUserEmail(string Email)
        {
           var user = context.register.FirstOrDefault(u => u.Email == Email);

            Guid roleId = user.RoleId;

            return roleId;
        }

        public UserDto GetUserByEmail(string Email)
        {
            var user = context.register.FirstOrDefault(e => e.Email == Email);

            var userDto = user.UserToDto();

            return userDto;
        }

        public UserDto GetUserById(Guid UserId)
        {
           var user = context.register.FirstOrDefault(e => e.UserId == UserId);

            var userDto = user.UserToDto();

            return userDto;
        }

        public bool SaveChanges()
        {
           return context.SaveChanges() > 0;
        }

        public User GetUserByIdHelper(Guid UserId)
        {
            var user = context.register.FirstOrDefault(e => e.UserId == UserId);

            return user;
        }
        public UserDto UpdateUser(Guid UserId, UserDto userDto)
        {
            var oldUserDto = GetUserByIdHelper(UserId);

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

        
    }
}
