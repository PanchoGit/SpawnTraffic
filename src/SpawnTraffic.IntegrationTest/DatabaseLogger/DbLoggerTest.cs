using Microsoft.EntityFrameworkCore;
using SpawnTraffic.DatabaseLogger;
using SpawnTraffic.DatabaseLogger.Repository;
using SpawnTraffic.DatabaseLogger.Repository.Contexts;
using SpawnTraffic.Logger;
using Xunit;

namespace SpawnTraffic.IntegrationTest.DatabaseLogger
{
    public class DbLoggerTest
    {
        private const string ConnectionString =
            "Server=localhost\\SQLExpress;Database=SpawnTrafficTest;Trusted_Connection=True;MultipleActiveResultSets=true";

        private readonly ILogger sut;

        private readonly SpawnTrafficContext context;

        public DbLoggerTest()
        {
            var dbBuilder = new DbContextOptionsBuilder<SpawnTrafficContext>();

            dbBuilder.UseSqlServer(ConnectionString);

            context = new SpawnTrafficContext(dbBuilder.Options);

            var logRepository = new LogRepository(context);

            sut = new DbLogger(logRepository);
        }

        [Fact]
        public void ShouldPassSave()
        {
            context.Database.BeginTransaction();

            var actual = sut.Log("MessageOne", MessageType.Success);

            Assert.False(actual.HasErrors);
        }
    }
}
