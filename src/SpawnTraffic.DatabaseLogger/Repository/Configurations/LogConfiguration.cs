using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpawnTraffic.DatabaseLogger.Repository.Configurations
{
    public class LogConfiguration : IEntityTypeConfiguration<DatabaseLogger.Domain.Log>
    {
        public void Configure(EntityTypeBuilder<DatabaseLogger.Domain.Log> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
