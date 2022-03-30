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
    }
}
