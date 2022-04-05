﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserService.Enitites;

namespace UserService.Migrations
{
    [DbContext(typeof(UserContext))]
    [Migration("20220405111247_UserMigration")]
    partial class UserMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserService.Enitites.Role", b =>
                {
                    b.Property<Guid>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("roles");

                    b.HasData(
                        new
                        {
                            RoleId = new Guid("66c72bcd-43ac-4914-a2e9-856fac56b891"),
                            UserType = "User"
                        },
                        new
                        {
                            RoleId = new Guid("e4711e98-5cb3-4ebc-8c2e-1f90c54f9255"),
                            UserType = "Admin"
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
                        .HasDefaultValue(new Guid("66c72bcd-43ac-4914-a2e9-856fac56b891"));

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("register");

                    b.HasData(
                        new
                        {
                            UserId = new Guid("f0077164-4446-45d6-9185-aa2d60aee928"),
                            Email = "marko@gmail.com",
                            FirstName = "Marko",
                            LastName = "Milic",
                            Password = "123456",
                            RoleId = new Guid("66c72bcd-43ac-4914-a2e9-856fac56b891")
                        },
                        new
                        {
                            UserId = new Guid("089c377b-6464-4692-987e-483d556753dc"),
                            Email = "Nikola@gmail.com",
                            FirstName = "Nikola",
                            LastName = "Milic",
                            Password = "123456",
                            RoleId = new Guid("e4711e98-5cb3-4ebc-8c2e-1f90c54f9255")
                        });
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
#pragma warning restore 612, 618
        }
    }
}
