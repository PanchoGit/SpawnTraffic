using System.Collections.Generic;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Logger;
using SpawnTraffic.LoggerManager.Interfaces;

namespace SpawnTraffic.LoggerManager
{
    public class LoggerManager : ILoggerManager
    {
        private IEnumerable<ILogger> Loggers { get; }

        public LoggerManager(IPluginLoggerManager pluginLoggerManager)
        {
            Loggers = pluginLoggerManager.GetAvailableLoggers();
        }

        public Result LogWarn(string message)
        {
            return ProcessLog(message, MessageType.Warm);
        }

        public Result LogError(string message)
        {
            return ProcessLog(message, MessageType.Error);
        }

        public Result LogSuccess(string message)
        {
            return ProcessLog(message, MessageType.Success);
        }

        private Result ProcessLog(string message, MessageType type)
        {
            var result = new Result();

            foreach (var logger in Loggers)
            {
                var logResult = logger.Log(message, type);

                result.AddMessages(logResult.Messages);
            }

            return result;
        }
    }
}