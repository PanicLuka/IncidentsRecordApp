using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IUsersService
    {
        void CreateUser(UserDto user);
        List<UserDto> GetAllUsers();
        UserDto GetUserById(Guid userId);
        UserDto GetUserByEmail(string email);
        UserDto UpdateUser(Guid userId, UserDto userDto);
        void DeleteUser(Guid userId);
        Guid GetRoleIdByUserEmail(string email);
        public Guid GetUserIdByEmail(string email);
        bool SaveChanges();
    }
}
