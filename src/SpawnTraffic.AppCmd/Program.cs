using Autofac;
using Microsoft.Extensions.Configuration;
using SpawnTraffic.AppCmd.Infrastructures;

namespace SpawnTraffic.AppCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup().Resolve<AppMain>().Run();
        }

        private static IContainer Setup()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false)
                .Build();

            var containerBuilder = new ContainerBuilder();

            containerBuilder.RegisterModule(new DefaultModule { Configuration = configuration });

            return containerBuilder.Build();
        }
    }
}
