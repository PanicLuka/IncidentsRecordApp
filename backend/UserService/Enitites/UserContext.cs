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
                    RoleId = Guid.Parse("66c72bcd-43ac-4914-a2e9-856fac56b891"),
                    UserType = "User"
                });

            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = Guid.Parse("e4711e98-5cb3-4ebc-8c2e-1f90c54f9255"),
                    UserType = "Admin"
                });

            modelBuilder.Entity<User>()
                .Property(b => b.RoleId)
                .HasDefaultValue(Guid.Parse("66c72bcd-43ac-4914-a2e9-856fac56b891"));



            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = Guid.Parse("f0077164-4446-45d6-9185-aa2d60aee928"),
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = "123456",
                    RoleId = Guid.Parse("66c72bcd-43ac-4914-a2e9-856fac56b891")
                });

            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = Guid.Parse("089c377b-6464-4692-987e-483d556753dc"),
                    FirstName = "Nikola",
                    LastName = "Milic",
                    Email = "Nikola@gmail.com",
                    Password = "123456",
                    RoleId = Guid.Parse("e4711e98-5cb3-4ebc-8c2e-1f90c54f9255")
                });
        }

    }
}
