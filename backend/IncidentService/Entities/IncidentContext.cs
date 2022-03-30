using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IncidentService.Entities
{
    public class IncidentContext : DbContext
    {
        private readonly IConfiguration configuration;

        public IncidentContext(DbContextOptions<IncidentContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Incident> Incidents { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("IncidentDB"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasData(new
                {
                    CategoryId = 1,
                    CategoryName = "testName"
                });


            modelBuilder.Entity<Incident>()
                .HasData(new
                {
                    IncidentId = 1,
                    Number = "test01",
                    Significance = 1,
                    Workspace = "test",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "test",
                    ThirdPartyHelp = "test",
                    ProblemSolved = true,
                    FurtherAction = "test",
                    FurtherActionPerson = "test",
                    ActionDescription = "test",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "test",
                    Verifies = "test",
                    UserId = 1,
                    CategoryId = 1
                });
        }
    }
}
