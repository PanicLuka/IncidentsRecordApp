using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using EnumClassLibrary;
using System.Collections.Generic;
using UserService.Helpers;

namespace UserService.Enitites
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<User> User { get; set; }

        public DbSet<Role> Role { get; set; }

        public DbSet<Permission> Permission { get; set; }

        public DbSet<UserPermission> UserPermissions { get; set; }

        public DbSet<RolePermission> rolePermissions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("UserRegisterDB"));
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

            #region

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[0],
                    AccessPermissions = permissionNames[0]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[1],
                    AccessPermissions = permissionNames[1]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[2],
                    AccessPermissions = permissionNames[2]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[3],
                    AccessPermissions = permissionNames[3]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[4],
                    AccessPermissions = permissionNames[4]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[5],
                    AccessPermissions = permissionNames[5]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[6],
                    AccessPermissions = permissionNames[6]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[7],
                    AccessPermissions = permissionNames[7]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[8],
                    AccessPermissions = permissionNames[8]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[9],
                    AccessPermissions = permissionNames[9]
                });

            modelBuilder.Entity<Permission>()
                .HasData(new
                {
                    PermissionId = permissionGuids[10],
                    AccessPermissions = permissionNames[10]
                });

            #endregion


            #region

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

            #endregion

            #region

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

            #endregion

            #region

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

            #endregion


            #region

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
                    Password = "123456",
                    RoleId = defaultGuidUser
                });

            modelBuilder.Entity<User>()
                .HasData(new
                {
                    UserId = defaultUserTwoGuid,
                    FirstName = "Nikola",
                    LastName = "Milic",
                    Email = "Nikola@gmail.com",
                    Password = "123456",
                    RoleId = defaultGuidAdmin
                });

            #endregion
        }

    }
}
