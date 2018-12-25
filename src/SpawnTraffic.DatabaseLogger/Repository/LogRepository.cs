using SpawnTraffic.DatabaseLogger.Domain;
using SpawnTraffic.DatabaseLogger.Repository.Contexts;
using SpawnTraffic.DatabaseLogger.Repository.Interfaces;

namespace SpawnTraffic.DatabaseLogger.Repository
{
    public class LogRepository : RepositoryBase<Log, int>, ILogRepository
    {
        public LogRepository(SpawnTrafficContext context) : base(context)
        {
        }

        public void Save(Log log)
        {
            Insert(log);
        }
    }
}
