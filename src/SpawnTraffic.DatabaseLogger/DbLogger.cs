using System;
using System.Composition;
using Microsoft.EntityFrameworkCore;
using SpawnTraffic.Common;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.DatabaseLogger.Domain;
using SpawnTraffic.DatabaseLogger.Repository;
using SpawnTraffic.DatabaseLogger.Repository.Contexts;
using SpawnTraffic.DatabaseLogger.Repository.Interfaces;
using SpawnTraffic.Logger;

namespace SpawnTraffic.DatabaseLogger
{
    [Export(typeof(ILogger))]
    public class DbLogger : ILogger
    {
        private const string ConnectionString =
            "Server=localhost\\SQLExpress;Database=SpawnTraffic;Trusted_Connection=True;MultipleActiveResultSets=true";

        private readonly ILogRepository repository;

        public DbLogger()
        {
            var dbBuilder = new DbContextOptionsBuilder<SpawnTrafficContext>();

            dbBuilder.UseSqlServer(ConnectionString);

            repository = new LogRepository(new SpawnTrafficContext(dbBuilder.Options));
        }

        public DbLogger(ILogRepository repository)
        {
            this.repository = repository;
        }

        public Result Log(string message, MessageType type)
        {
            var result = new Result();

            try
            {
                repository.Save(new Log
                {
                    Date = DateTimeOffset.Now,
                    MessageType = (int)type,
                    Message = message
                });

                result.AddSuccess(string.Format(CommonResource.LogSuccess, nameof(DbLogger)));
            }
            catch (Exception exception)
            {
                result.AddError(string.Format(CommonResource.LogError, nameof(DbLogger), exception.Message));
            }

            return result;
        }
    }
}
