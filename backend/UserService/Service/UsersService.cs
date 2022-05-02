using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using UserService.Enitites;
using UserService.Entities;
using UserService.Helpers;
using UserService.Models;
using UserService.Validators;
using BC = BCrypt.Net.BCrypt;

namespace UserService.Service
{
    public class UsersService : IUsersService
    {
        private readonly DataContext _context;
        private readonly UserValidator _userValidator;
        private static int _count;
        public UsersService(DataContext context, UserValidator userValidator)
        {
            _context = context;
            _userValidator = userValidator;
        }

        public int GetUsersCount()
        {
            return _count;
        }
        public void CreateUser(UserDto userDto)
        {
            _userValidator.ValidateAndThrow(userDto);

            User userEntity = userDto.DtoToUser();

            userEntity.Password = BC.HashPassword(userEntity.Password);
        
           _context.Add(userEntity);
            
            SaveChanges();
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetUserByIdHelper(userId);

            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Remove(user);

            SaveChanges();
        }

        public PagedList<UserWithIdDto> GetAllUsers(UserParameters userParameters)
        {
            var users  = _context.Users.ToList();

            _count = users.Count();

            if (users == null || users.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            List<UserWithIdDto> userDtos = new List<UserWithIdDto>();

            foreach (var user in users)
            {     
                UserWithIdDto userDto = user.UserToUserWithIdDto();

                userDtos.Add(userDto);
            }
            IQueryable<UserWithIdDto> queryable = userDtos.AsQueryable();

            return PagedList<UserWithIdDto>.ToPagedList(queryable, userParameters.PageNumber, userParameters.PageSize);
        }

       
        public UserDto GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(e => e.Email == email);

            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var userDto = user.UserToDto();

            return userDto;
        }

        public UserDto GetUserById(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(e => e.UserId == userId);
            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            var userDto = user.UserToDto();

            return userDto;
        }

        public Guid GetUserIdByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Guid userId = user.UserId;

            return userId;
        }
        private bool SaveChanges()
        {
           return _context.SaveChanges() > 0;
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

                oldUserDto.Password = BC.HashPassword(oldUserDto.Password);



                if (oldUserDto.UserToDto() == null)
                {
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                }

                
                SaveChanges();
                return oldUserDto.UserToDto();
            }

        }

        public Guid GetRoleIdByUserEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if(user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Guid roleId = user.RoleId;

            return roleId;
        }

        private User GetUserByIdHelper(Guid userId)
        {
            var user = _context.Users.FirstOrDefault(e => e.UserId == userId);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user;
        }

    }
}
