using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Enitites;

namespace UserService.Data
{
    public class RegisterRepository : IRegisterRepository
    {
        private readonly UserContext context;
        private readonly IMapper mapper;

        public RegisterRepository(UserContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task CreateUserAsync(Register User)
        {
            await context.AddAsync(User);
        }

        public async Task DeleteUserAsync(int UserId)
        {
            var user = await GetUserByIdAsync(UserId);

            context.Remove(user);
        }

        public async Task<List<Register>> GetAllUsersAsync()
        {
            return await context.register.ToListAsync();
        }

        public async Task<Register> GetUserByIdAsync(int UserId)
        {
            return await context.register.FirstOrDefaultAsync(e => e.UserId == UserId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public async Task UpdateUserAsync(Register User)
        {

        }
    }
}
