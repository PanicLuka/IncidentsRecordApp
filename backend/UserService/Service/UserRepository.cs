using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public UserRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task CreateUserAsync(User User)
        {
            await context.AddAsync(User);
        }

        public async Task DeleteUserAsync(int UserId)
        {
            var user = await GetUserByIdAsync(UserId);

            context.Remove(user);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await context.register.ToListAsync();
        }

        public async Task<int> GetRoleIdByUserEmail(string Email)
        {
           var user = await context.register.FirstOrDefaultAsync(u => u.Email == Email);

            int roleId = user.RoleId;

            return roleId;
        }

        public async Task<User> GetUserByEmailAsync(string Email)
        {
            return await context.register.FirstOrDefaultAsync(u => u.Email == Email);
        }

        public async Task<User> GetUserByIdAsync(int UserId)
        {
            return await context.register.FirstOrDefaultAsync(e => e.UserId == UserId);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateUserAsync(User User)
        {
           
        }
    }
}
