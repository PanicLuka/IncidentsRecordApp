﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Entities;

namespace UserService.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserService.Enitites.Permission", b =>
                {
                    b.Property<Guid>("PermissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AccessPermission")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PermissionId");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            PermissionId = new Guid("9dca1b7b-df23-4cb4-a827-2bd4edc8062f"),
                            AccessPermission = "UserGetAll"
                        },
                        new
                        {
                            PermissionId = new Guid("8ef2df52-1bd4-4983-b1f0-16a96a28b17d"),
                            AccessPermission = "UserDelete"
                        },
                        new
                        {
                            PermissionId = new Guid("5e9db9d3-2bba-4fc8-adc5-e654e51dd4f3"),
                            AccessPermission = "UserUpdate"
                        },
                        new
                        {
                            PermissionId = new Guid("34841212-ff54-42d2-8fc7-15cc4ef5c664"),
                            AccessPermission = "UserGetById"
                        },
                        new
                        {
                            PermissionId = new Guid("60508954-c6c1-48c7-b1d3-5e9ec4fc3439"),
                            AccessPermission = "UserCreateUser"
                        },
                        new
                        {
                            PermissionId = new Guid("8cd39687-7d78-4272-a7c3-43f78ae13b23"),
                            AccessPermission = "IncidentsGetAll"
                        },
                        new
                        {
                            PermissionId = new Guid("c49d33ae-1d20-4bbc-bf0e-f2a1bdf10b0d"),
                            AccessPermission = "IncidentsGetById"
                        },
                        new
                        {
                            PermissionId = new Guid("ac8dbcf9-eb93-472b-ae1a-55492dd013eb"),
                            AccessPermission = "IncidentsUpdate"
                        },
                        new
                        {
                            PermissionId = new Guid("facb87f7-f189-46a5-b434-629edd44d7f2"),
                            AccessPermission = "IncidentsDelete"
                        },
                        new
                        {
                            PermissionId = new Guid("1e780ba9-d58c-4878-b69a-a2588ba93af2"),
                            AccessPermission = "IncidentsCreate"
                        },
                        new
                        {
                            PermissionId = new Guid("2dd89718-a3fc-40e4-b140-936b5bfc6e24"),
                            AccessPermission = "PromoteToAdmin"
                        });
                });

            modelBuilder.Entity("UserService.Enitites.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("ac58b954-9521-4416-8b64-e8f42b297afd"),
                            UserType = "User"
                        },
                        new
                        {
                            RoleId = new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d"),
                            UserType = "Admin"
                        });
                });

            modelBuilder.Entity("UserService.Enitites.RolePermission", b =>
                {
                    b.Property<Guid?>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("ac58b954-9521-4416-8b64-e8f42b297afd"),
                            PermissionId = new Guid("8cd39687-7d78-4272-a7c3-43f78ae13b23")
                        },
                        new
                        {
                            RoleId = new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d"),
                            PermissionId = new Guid("8cd39687-7d78-4272-a7c3-43f78ae13b23")
                        },
                        new
                        {
                            RoleId = new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d"),
                            PermissionId = new Guid("c49d33ae-1d20-4bbc-bf0e-f2a1bdf10b0d")
                        },
                        new
                        {
                            RoleId = new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d"),
                            PermissionId = new Guid("ac8dbcf9-eb93-472b-ae1a-55492dd013eb")
                        },
                        new
                        {
                            RoleId = new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d"),
                            PermissionId = new Guid("facb87f7-f189-46a5-b434-629edd44d7f2")
                        });
                });

            modelBuilder.Entity("UserService.Enitites.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("ac58b954-9521-4416-8b64-e8f42b297afd"));

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("d63aa8e8-7b20-4290-9f6d-7ed77cce7290"),
                            Email = "marko@gmail.com",
                            FirstName = "Marko",
                            LastName = "Milic",
                            Password = "$2a$11$PhR6MTU8hBwVJsB1qVGbTOU1gCXU08MRzOStIAqoTq/WZLCbXRFKu",
                            RoleId = new Guid("ac58b954-9521-4416-8b64-e8f42b297afd")
                        },
                        new
                        {
                            UserId = new Guid("cac1a4bc-7468-41e3-a213-30371559abca"),
                            Email = "nikola@gmail.com",
                            FirstName = "Nikola",
                            LastName = "Milic",
                            Password = "$2a$11$JsaYHicNKu1PlyIcFO8bSeYjgIDyr9VkD4i6Cq1mZcBFgxVvYHi86",
                            RoleId = new Guid("02e05a56-3c26-4bbc-bfa3-2d6e696b364d")
                        });
                });

            modelBuilder.Entity("UserService.Enitites.UserPermission", b =>
                {
                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("PermissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("UserPermissions");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("d63aa8e8-7b20-4290-9f6d-7ed77cce7290"),
                            PermissionId = new Guid("facb87f7-f189-46a5-b434-629edd44d7f2")
                        },
                        new
                        {
                            UserId = new Guid("cac1a4bc-7468-41e3-a213-30371559abca"),
                            PermissionId = new Guid("1e780ba9-d58c-4878-b69a-a2588ba93af2")
                        });
                });

            modelBuilder.Entity("UserService.Enitites.RolePermission", b =>
                {
                    b.HasOne("UserService.Enitites.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserService.Enitites.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UserService.Enitites.User", b =>
                {
                    b.HasOne("UserService.Enitites.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("UserService.Enitites.UserPermission", b =>
                {
                    b.HasOne("UserService.Enitites.Permission", "Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserService.Enitites.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
