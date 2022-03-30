using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public RoleRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task CreateRoleAysnc(Role Role)
        {
            await context.AddAsync(Role);
        }

        public async Task DeleteRoleAsync(int RoleId)
        {
            var role = await GetRoleByIdAsync(RoleId);

            context.Remove(role);
        }

        public async Task<List<Role>> GetAllRolesAsync()
        {
            return await context.roles.ToListAsync();
        }

        public async Task<Role> GetRoleByIdAsync(int RoleId)
        {
            return await context.roles.FirstOrDefaultAsync(e => e.RoleId == RoleId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateRoleAsync(Role Role)
        {
            
        }
    }
}
