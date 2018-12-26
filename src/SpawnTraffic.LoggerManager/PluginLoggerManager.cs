using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Runtime.Loader;
using SpawnTraffic.Logger;
using SpawnTraffic.LoggerManager.Interfaces;

namespace SpawnTraffic.LoggerManager
{
    public class PluginLoggerManager : IPluginLoggerManager
    {
        private readonly string pluginsFolder;

        public PluginLoggerManager(LoggerSetting loggerSetting)
        {
            pluginsFolder = loggerSetting.PluginFolder;
        }

        [ImportMany]
        private IEnumerable<ILogger> AvailableLoggers { get; set; }

        public IEnumerable<ILogger> GetAvailableLoggers()
        {
            AvailableLoggers = new List<ILogger>();

            var pluginDirectory = $"{AppDomain.CurrentDomain.BaseDirectory}\\{pluginsFolder}";

            if (!Directory.Exists(pluginDirectory))
            {
                Directory.CreateDirectory(pluginDirectory);
            }

            var assemblies = Directory
                .GetFiles(pluginDirectory, "*.dll")
                .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath);

            var configuration = new ContainerConfiguration()
                .WithAssemblies(assemblies);

            using (var container = configuration.CreateContainer())
            {
                container.SatisfyImports(this);
            }

            return AvailableLoggers;
        }
    }
}
