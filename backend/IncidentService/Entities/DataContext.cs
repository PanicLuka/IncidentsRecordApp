﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IncidentService.Entities
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration _configuration;

        private Guid defaultIncidentId = Guid.NewGuid();
        private Guid defaultCategoryId = Guid.NewGuid();
        private Guid defaultUserId = Guid.NewGuid();

        public DataContext() { }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("IncidentDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new
                {
                    CategoryId = defaultCategoryId,
                    CategoryName = "testName"
                });


            modelBuilder.Entity<Incident>()
                .HasData(new
                {
                    IncidentId = defaultIncidentId,
                    Designation = "test01",
                    Significance = 1,
                    Workspace = "test",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = "00:00",
                    Description = "test",
                    ThirdPartyHelp = true,
                    ProblemSolved = "test",
                    FurtherAction = true,
                    FurtherActionPerson = "test",
                    ActionDescription = "test",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "test",
                    Verifies = "test",
                    ReportedBy = "test",
                    UserId = defaultUserId,
                    CategoryId = defaultCategoryId
                });
        }
    }
}
