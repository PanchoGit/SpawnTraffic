using System;
using System.Composition;
using SpawnTraffic.Common;
using SpawnTraffic.Common.Domains;
using SpawnTraffic.Logger;

namespace SpawnTraffic.ConsoleLogger
{
    [Export(typeof(ILogger))]
    public class ConsoleLogger : ILogger
    {
        public Result Log(string message, MessageType type)
        {
            var result = new Result();

            Console.WriteLine(CommonResource.LogFormatMessage, type, message);

            result.AddSuccess(string.Format(CommonResource.LogSuccess, nameof(ConsoleLogger)));

            return result;
        }
    }
}
