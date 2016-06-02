using System;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace RedisCacheWebPoc.Services
{
    public class CacheService : ICacheService
    {
        public void Add(string key, object value)
        {
            Cache.StringSet(key, JsonConvert.SerializeObject(value), expiry: new TimeSpan(5, 0, 0, 0));
        }

        public T Get<T>(string key)
        {
            var result = Cache.StringGet(key);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect("transaction.redis.cache.windows.net:6380,password=AoAo9CkdCyUXn8fMYwX+6xbZvn3Mez98uEdy7aNZZJQ=,ssl=True,abortConnect=False"));

        private IDatabase Cache => _lazyConnection.Value.GetDatabase();
    }
}
