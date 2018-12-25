using System.Collections.Generic;
using System.Linq;
using SpawnTraffic.Common.Cache.Interfaces;
using SpawnTraffic.DataCache.Interfaces;
using SpawnTraffic.Model;

namespace SpawnTraffic.DataCache
{
    public class SkaterData : ISkaterData
    {
        private const string SkaterCacheKey = "urn:skaters:list";

        private readonly ICacheManager cacheManager;

        public SkaterData(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public List<SkaterModel> Get()
        {
            return cacheManager.GetSortedSet<SkaterModel>(SkaterCacheKey).ToList();
        }

        public void Set(List<SkaterModel> data)
        {
            cacheManager.SetSortedSet(SkaterCacheKey, data, x => x.Created.Ticks);
        }
    }
}
