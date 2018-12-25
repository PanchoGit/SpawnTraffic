using SpawnTraffic.DatabaseLogger.Domain;

namespace SpawnTraffic.DatabaseLogger.Repository.Interfaces
{
    public interface ILogRepository
    {
        void Save(Log log);
    }
}