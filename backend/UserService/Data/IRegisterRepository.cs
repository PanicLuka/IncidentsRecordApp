using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public interface IRegisterRepository
    {
        Task CreateUserAsync(Register User);

        Task<List<Register>> GetAllUsersAsync();

        Task<Register> GetUserByIdAsync(int UserId);

        Task UpdateUserAsync(Register User);

        Task DeleteUserAsync(int UserId);

        Task<bool> SaveChangesAsync();

    }
}
