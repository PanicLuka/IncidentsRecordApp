using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public interface IRegisterRepository
    {
        Task CreateUserAsync(User User);

        Task<List<User>> GetAllUsersAsync();

        Task<User> GetUserByIdAsync(int UserId);

        Task UpdateUserAsync(User User);

        Task DeleteUserAsync(int UserId);

        Task<bool> SaveChangesAsync();

    }
}
