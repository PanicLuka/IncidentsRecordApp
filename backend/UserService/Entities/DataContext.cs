using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using EnumClassLibrary;
using System.Collections.Generic;
using UserService.Helpers;
using UserService.Enitites;
using BC = BCrypt.Net.BCrypt;

namespace UserService.Entities
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("UserRegisterDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RolePermission>()
                .HasKey(a => new { a.RoleId, a.PermissionId });

            modelBuilder.Entity<UserPermission>()
                .HasKey(a => new { a.UserId, a.PermissionId });

            Guid defaultGuidUser = Guid.NewGuid();
            Guid defaultGuidAdmin = Guid.NewGuid();


            Guid defaultUserOneGuid = Guid.NewGuid();
            Guid defaultUserTwoGuid = Guid.NewGuid();

            List<string> permissionNames = new List<string>();
            List<Guid> permissionGuids = new List<Guid>();

            permissionGuids = GuidHelper.GetGuids();

            foreach (string i in Enum.GetNames(typeof(EnumPermissions.Permissions)))
            {
                permissionNames.Add(i);
            }

        

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[0],
                    AccessPermission = permissionNames[0]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[1],
                    AccessPermission = permissionNames[1]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[2],
                    AccessPermission = permissionNames[2]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[3],
                    AccessPermission = permissionNames[3]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[4],
                    AccessPermission = permissionNames[4]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[5],
                    AccessPermission = permissionNames[5]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[6],
                    AccessPermission = permissionNames[6]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[7],
                    AccessPermission = permissionNames[7]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[8],
                    AccessPermission = permissionNames[8]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[9],
                    AccessPermission = permissionNames[9]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[10],
                    AccessPermission = permissionNames[10]
                });



            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = defaultGuidUser,
                    UserType = "User"
                });

            modelBuilder.Entity<Role>()
                .HasData(new
                {
                    RoleId = defaultGuidAdmin,
                    UserType = "Admin"
                });

         

            modelBuilder.Entity<RolePermission>()
                .HasData(new
                {
                    RoleId = defaultGuidUser,
                    PermissionId = permissionGuids[5]

                });

            modelBuilder.Entity<RolePermission>()
                .HasData(new
                {
                    RoleId = defaultGuidAdmin,
                    PermissionId = permissionGuids[5]

                });

            modelBuilder.Entity<RolePermission>()
               .HasData(new
               {
                   RoleId = defaultGuidAdmin,
                   PermissionId = permissionGuids[6]

               });

            modelBuilder.Entity<RolePermission>()
               .HasData(new
               {
                   RoleId = defaultGuidAdmin,
                   PermissionId = permissionGuids[7]

               });

            modelBuilder.Entity<RolePermission>()
               .HasData(new
               {
                   RoleId = defaultGuidAdmin,
                   PermissionId = permissionGuids[8]

               });

            

            modelBuilder.Entity<UserPermission>()
               .HasData(new
               {
                   UserId = defaultUserOneGuid,
                   PermissionId = permissionGuids[8]

               });

            modelBuilder.Entity<UserPermission>()
               .HasData(new
               {
                   UserId = defaultUserTwoGuid,
                   PermissionId = permissionGuids[9]

               });

          

            modelBuilder.Entity<User>()
                .Property(b => b.RoleId)
                .HasDefaultValue(defaultGuidUser);



            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = defaultUserOneGuid,
                    FirstName = "Marko",
                    LastName = "Milic",
                    Email = "marko@gmail.com",
                    Password = BC.HashPassword("Test12345"),
                    RoleId = defaultGuidUser
                });

            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = defaultUserTwoGuid,
                    FirstName = "Nikola",
                    LastName = "Milic",
                    Email = "nikola@gmail.com",
                    Password = BC.HashPassword("Test12345"),
                    RoleId = defaultGuidAdmin
                });

        }

    }
}
