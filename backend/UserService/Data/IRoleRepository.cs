using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public interface IRoleRepository
    {
        Task CreateRoleAysnc (Role Role);

        Task<List<Role>> GetAllRolesAsync();

        Task<Role> GetRoleByIdAsync(int RoleId);

        Task UpdateRoleAsync(Role Role);

        Task DeleteRoleAsync(int RoleId);

        Task<bool> SaveChangesAsync();

    }
}
