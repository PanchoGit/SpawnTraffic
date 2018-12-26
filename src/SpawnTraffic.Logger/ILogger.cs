using SpawnTraffic.Common.Domains;

namespace SpawnTraffic.Logger
{
    public interface ILogger
    {
        Result Log(string message, MessageType type);
    }
}