using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;
using SpawnTraffic.Common.Helpers;
using SpawnTraffic.Model.ModelMaps;
using StackExchange.Redis;
using Module = Autofac.Module;

namespace SpawnTraffic.AppCmd.Infrastructures
{
    public class DefaultModule : Module
    {
        private const string DataAssemblyEndName = "Data";
        private const string ManagerAssemblyEndName = "Manager";
        private const string ViewAssemblyEndName = "View";
        private const string WorkflowAssemblyEndName = "Workflow";

        public IConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AssemblyHelper.GetReferencingAssemblies(nameof(SpawnTraffic));

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(DataAssemblyEndName))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(ManagerAssemblyEndName))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(ViewAssemblyEndName))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assemblies)
                .Where(s => s.Name.EndsWith(WorkflowAssemblyEndName))
                .AsImplementedInterfaces();

            builder.RegisterType<AppMain>();

            RegisterRedisDependencies(builder);

            RegisterAutoMapper(builder);

            RegisterLog4Net();
        }

        private void RegisterRedisDependencies(ContainerBuilder builder)
        {
            builder.Register(c =>
                {
                    var redisConfig = Configuration["Redis:ConnectionString"];

                    var lazyConnection =
                        new Lazy<ConnectionMultiplexer>(
                            () => ConnectionMultiplexer.Connect(redisConfig));
                    return lazyConnection.Value;
                })
                .As<ConnectionMultiplexer>()
                .SingleInstance();

            builder.Register(c => c.Resolve<Lazy<ConnectionMultiplexer>>().Value.GetDatabase())
                .As<IDatabase>();
        }

        private static void RegisterAutoMapper(ContainerBuilder builder)
        {
            var profiles = typeof(SkaterModelMap).Assembly.GetTypes().Where(x => typeof(Profile).IsAssignableFrom(x)).ToList();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>();
        }

        private void RegisterLog4Net()
        {
            var logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());

            XmlConfigurator.Configure(logRepository, new FileInfo(Configuration["log4NetConfig"]));
        }
    }
}
