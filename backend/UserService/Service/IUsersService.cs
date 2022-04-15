using System;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Service
{
    public interface IUsersService
    {
        void CreateUser(UserDto user);
        PagedList<UserDto> GetAllUsers(UserParameters userParameters);
        UserDto GetUserById(Guid userId);
        UserDto GetUserByEmail(string email);
        UserDto UpdateUser(Guid userId, UserDto userDto);
        void DeleteUser(Guid userId);
        Guid GetRoleIdByUserEmail(string email);
        Guid GetUserIdByEmail(string email);
    }
}
