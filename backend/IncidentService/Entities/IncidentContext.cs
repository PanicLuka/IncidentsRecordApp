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
                    CategoryId = Guid.Parse("df2a59f1-e711-4a91-bccd-08188b54440b"),
                    CategoryName = "testName"
                });


            modelBuilder.Entity<Incident>()
                .HasData(new
                {
                    IncidentId = Guid.Parse("89fe62bb-18ed-4eba-8fcd-1b7dbfd20c38"),
                    Number = "test01",
                    Significance = 1,
                    Workspace = "test",
                    Date = DateTime.Parse("2022-03-30T00:00:00"),
                    Time = DateTime.Parse("2022-03-30T00:00:00"),
                    Description = "test",
                    ThirdPartyHelp = true,
                    ProblemSolved = "test",
                    FurtherAction = true,
                    FurtherActionPerson = "test",
                    ActionDescription = "test",
                    SolvingDate = DateTime.Parse("2022-03-30T00:00:00"),
                    Remarks = "test",
                    Verifies = "test",
                    UserId = Guid.Parse("8edc8128-dc5e-4202-ba18-0a623f954729"),
                    CategoryId = Guid.Parse("df2a59f1-e711-4a91-bccd-08188b54440b")
                });
        }
    }
}
