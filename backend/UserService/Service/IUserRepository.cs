using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public interface IUserRepository
    {
        Task CreateUserAsync(User User);
        
        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int UserId);

        Task<User> GetUserByEmailAsync(string Email);
        Task UpdateUserAsync(User User);

        Task DeleteUserAsync(int UserId);

        Task<int> GetRoleIdByUserEmail(string Email);

        Task<bool> SaveChangesAsync();

    }
}
