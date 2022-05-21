﻿// <auto-generated />
using System;
using IncidentService.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IncidentService.Migrations
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

            modelBuilder.Entity("IncidentService.Entities.Category", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryId = new Guid("5e6bb3b0-4662-4536-8ce2-d12068c7fd7c"),
                            CategoryName = "testName"
                        });
                });

            modelBuilder.Entity("IncidentService.Entities.Incident", b =>
                {
                    b.Property<Guid>("IncidentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Designation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("FurtherAction")
                        .HasColumnType("bit");

                    b.Property<string>("FurtherActionPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProblemSolved")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Significance")
                        .HasColumnType("int");

                    b.Property<DateTime>("SolvingDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("ThirdPartyHelp")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Verifies")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Workspace")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IncidentId");

                    b.ToTable("Incidents");

                    b.HasData(
                        new
                        {
                            IncidentId = new Guid("50b646d5-91c3-43e9-b470-f0e7d3614302"),
                            ActionDescription = "test",
                            CategoryId = new Guid("5e6bb3b0-4662-4536-8ce2-d12068c7fd7c"),
                            Date = new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "test",
                            Designation = "test01",
                            FurtherAction = true,
                            FurtherActionPerson = "test",
                            ProblemSolved = "test",
                            Remarks = "test",
                            ReportedBy = "test",
                            Significance = 1,
                            SolvingDate = new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ThirdPartyHelp = true,
                            Time = new DateTime(2022, 3, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            UserId = new Guid("d4de8118-d006-4b8b-b322-638213f17f79"),
                            Verifies = "test",
                            Workspace = "test"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
