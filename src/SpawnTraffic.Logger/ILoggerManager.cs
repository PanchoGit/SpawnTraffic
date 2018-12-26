using SpawnTraffic.Common.Domains;

namespace SpawnTraffic.Logger
{
    public interface ILoggerManager
    {
        Result LogWarn(string message);

        Result LogError(string message);

        Result LogSuccess(string message);
    }
}