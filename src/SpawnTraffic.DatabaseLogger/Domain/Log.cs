using System;

namespace SpawnTraffic.DatabaseLogger.Domain
{
    public class Log
    {
        public int Id { get; set; }

        public DateTimeOffset Date { get; set; }

        public int MessageType { get; set; }

        public string Message { get; set; }
    }
}
