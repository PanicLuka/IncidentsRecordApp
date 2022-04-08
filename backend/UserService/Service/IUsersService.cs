using System;
using System.Collections.Generic;
using UserService.Models;

namespace UserService.Service
{
    public interface IUsersService
    {
        void CreateUser(UserDto User);
        List<UserDto> GetAllUsers();
        UserDto GetUserById(Guid UserId);
        UserDto GetUserByEmail(string Email);
        UserDto UpdateUser(Guid UserId, UserDto userDto);
        void DeleteUser(Guid UserId);
        Guid GetRoleIdByUserEmail(string Email);
        public Guid GetUserIdByEmail(string Email);
        bool SaveChanges();
    }
}
