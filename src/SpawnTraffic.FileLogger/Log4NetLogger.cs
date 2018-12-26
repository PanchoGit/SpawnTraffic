using System;
using System.Collections.Generic;
using System.Composition;
using log4net;
using SpawnTraffic.Common;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Logger;

namespace SpawnTraffic.FileLogger
{
    [Export(typeof(ILogger))]
    public class Log4NetLogger : ILogger
    {
        private readonly ILog log;

        private Dictionary<MessageType, Action<string>> logTypes;

        public Log4NetLogger()
        {
            log = LogManager.GetLogger(typeof(Log4NetLogger));

            InitLogTypes();
        }

        public Result Log(string message, MessageType type)
        {
            var result = new Result();

            if (!logTypes.ContainsKey(type))
            {
                result.AddError(FileLoggerResource.MessageTypeNotFound);

                return result;
            }

            try
            {
                logTypes[type](message);

                result.AddSuccess(string.Format(CommonResource.LogSuccess, nameof(Log4NetLogger)));
            }
            catch (Exception ex)
            {
                result.AddError(string.Format(CommonResource.LogError, nameof(Log4NetLogger), ex.Message));
            }

            return result;
        }

        private void InitLogTypes()
        {
            logTypes = new Dictionary<MessageType, Action<string>>
            {
                {MessageType.Error, log.Error},
                {MessageType.Warm, log.Warn},
                {MessageType.Success, log.Info}
            };
        }
    }
}
