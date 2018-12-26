﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using SpawnTraffic.Common.Cache.Interfaces;
using StackExchange.Redis;

namespace SpawnTraffic.Common.Cache
{
    public class CacheManager : ICacheManager
    {
        private readonly IDatabase redis;

        private readonly JsonSerializerSettings serializerSettings;

        public CacheManager(IDatabase redis)
        {
            this.redis = redis;

            serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            serializerSettings.Converters.Add(new StringEnumConverter(true));

            serializerSettings.Converters.Add(new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss'Z'" });
        }

        public T Get<T>(string cacheKey)
        {
            var cacheValue = redis.StringGet(cacheKey);

            return cacheValue.HasValue
                ? Deserialize<T>(cacheValue)
                : default(T);
        }

        public T Get<T>(string cacheKey, Func<T> resolver, TimeSpan? cacheExpire = null)
        {
            T result;

            var cache = Get<T>(cacheKey);

            if (cache != null)
            {
                result = cache;
            }
            else
            {
                result = resolver();

                if (result != null)
                {
                    Set(cacheKey, result, cacheExpire);
                }
            }

            return result;
        }

        public void Set<T>(string cacheKey, T result, TimeSpan? cacheExpire = null)
        {
            redis.StringSet(cacheKey, Serialize(result), cacheExpire);
        }

        public IList<T> GetHashList<T>(string cacheKey)
        {
            var result = new List<T>();

            var cache = redis.HashGetAll(cacheKey);

            if (cache.Any())
            {
                result = cache.Select(s => Deserialize<T>(s.Value)).ToList();
            }

            return result;
        }

        public IList<T> GetHashList<T>(string cacheKey, Func<IList<T>> resolver, Func<T, string> getKey, TimeSpan cacheExpire)
        {
            var result = GetHashList<T>(cacheKey);

            if (result.Any())
                return result;

            result = resolver();

            SetHashList(cacheKey, result, getKey, cacheExpire);

            return result;
        }

        public void SetHashList<T>(string cacheKey, IList<T> result, Func<T, string> getKey, TimeSpan cacheExpire)
        {
            var hashes = result.Select(s => new HashEntry(getKey(s), Serialize(s))).ToArray();

            redis.HashSet(cacheKey, hashes);

            redis.KeyExpire(cacheKey, cacheExpire);
        }

        public void DeletePatternKey(string pattern)
        {
            var endpoint = redis.Multiplexer.GetEndPoints().First();

            var server = redis.Multiplexer.GetServer(endpoint);

            var keys = server.Keys(redis.Database, pattern).ToArray();

            redis.KeyDelete(keys);
        }

        public IList<T> GetSortedSet<T>(string cacheKey)
        {
            var result = new List<T>();

            var cache = redis.SortedSetScan(cacheKey).ToList();

            if (cache.Any())
            {
                result = cache.Select(s => Deserialize<T>(s.Element)).ToList();
            }

            return result;
        }

        public void SetSortedSet<T>(string cacheKey, IEnumerable<T> result, Func<T, double> getScore)
        {
            var hashes = result.Select(s => new SortedSetEntry(Serialize(s), getScore(s))).ToArray();

            redis.SortedSetAdd(cacheKey, hashes);
        }

        private string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }

        private T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, serializerSettings);
        }
    }
}
