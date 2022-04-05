using System;
using System.Collections.Generic;
using UserService.Enitites;
using UserService.Models;

namespace UserService.Data
{
    public interface IUserRepository
    {
        void CreateUser(UserDto User);
        
        List<UserDto> GetAllUsers();

        UserDto GetUserById(Guid UserId);

        User GetUserByIdHelper(Guid UserId);
        UserDto GetUserByEmail(string Email);
        UserDto UpdateUser(Guid UserId, UserDto userDto);

        void DeleteUser(Guid UserId);

        Guid GetRoleIdByUserEmail(string Email);

        bool SaveChanges();

    }
}
