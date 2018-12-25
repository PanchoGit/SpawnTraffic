using System.Collections.Generic;
using SpawnTraffic.Logger;

namespace SpawnTraffic.LoggerManager.Interfaces
{
    public interface IPluginLoggerManager
    {
        IEnumerable<ILogger> GetAvailableLoggers();
    }
}