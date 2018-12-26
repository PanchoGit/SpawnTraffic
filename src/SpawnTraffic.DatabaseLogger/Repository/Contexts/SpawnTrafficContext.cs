using Microsoft.EntityFrameworkCore;
using SpawnTraffic.DatabaseLogger.Contexts;

namespace SpawnTraffic.DatabaseLogger.Repository.Contexts
{
    public class SpawnTrafficContext : DbContext
    {
        private const string AppSchema = "app";

        public SpawnTrafficContext(DbContextOptions<SpawnTrafficContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema(AppSchema);

            this.AddMappingConfigurations(modelBuilder);
        }
    }
}
