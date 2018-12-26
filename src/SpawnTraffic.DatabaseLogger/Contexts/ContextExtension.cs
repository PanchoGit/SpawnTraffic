using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SpawnTraffic.DatabaseLogger.Repository;

namespace SpawnTraffic.DatabaseLogger.Contexts
{
    public static class ContextExtension
    {
        public static void AddMappingConfigurations(this DbContext dbContext, ModelBuilder modelBuilder)
        {
            var types = dbContext.GetType().Assembly.GetTypes()
                .Where(x => x.FullName.EndsWith(RepositoryConstant.MappingClassPostfix));


            foreach (var type in types)
            {
                dynamic entityConfiguration = Activator.CreateInstance(type);

                modelBuilder.ApplyConfiguration(entityConfiguration);
            }
        }
    }
}
