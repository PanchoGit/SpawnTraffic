using System;

namespace SpawnTraffic.Model
{
    public class SkaterModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Brand { get; set; }

        public DateTimeOffset Created { get; set; }
    }
}
