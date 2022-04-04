using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Enitites
{
    public class UserContext : DbContext
    {
        private readonly IConfiguration configuration;

        public UserContext(DbContextOptions<UserContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<User> register { get; set; }

        public DbSet<Role> roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserRegisterDB"));
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = 2,
                    UserType = "User"
                });

            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = 1,
                    UserType = "Admin"
                });

            modelBuilder.Entity<User>()
                .Property(b => b.RoleId)
                .HasDefaultValue(2);



            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = 1,
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = "123456",
                    RoleId = 2
                });

            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = 2,
                    FirstName = "Nikola",
                    LastName = "Milic",
                    Email = "Nikola@gmail.com",
                    Password = "123456",
                    RoleId = 1
                });
        }

    }
}
