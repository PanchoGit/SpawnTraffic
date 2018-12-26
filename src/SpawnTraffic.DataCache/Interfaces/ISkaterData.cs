using System.Collections.Generic;
using SpawnTraffic.Model;

namespace SpawnTraffic.DataCache.Interfaces
{
    public interface ISkaterData
    {
        List<SkaterModel> Get();

        void Set(List<SkaterModel> data);
    }
}