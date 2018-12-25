using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Logger;

namespace SpawnTraffic.LoggerManager
{
    public class LoggerManager : ILoggerManager
    {
        private const string PluginsFolder = "plugins";

        [ImportMany]
        private IEnumerable<ILogger> Loggers { get; set; }

        public LoggerManager()
        {
            RegisterLogger();
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
            RegisterLogger();

            var result = new Result();

            foreach (var logger in Loggers)
            {
                var logResult = logger.Log(message, type);

                result.AddMessages(logResult.Messages);
            }

            return result;
        }

        private void RegisterLogger()
        {
            Loggers = new List<ILogger>();

            var pluginDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\{PluginsFolder}";

            var assemblies = Directory
                .GetFiles(pluginDirectory, "*.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using (var container = configuration.CreateContainer())
            {
                container.SatisfyImports(this);
            }
        }
    }
}