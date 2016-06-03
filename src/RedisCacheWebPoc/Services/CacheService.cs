using System;
using System.Linq;
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

        public long Count()
        {
            var info = _lazyConnection.Value.GetServer("transaction.redis.cache.windows.net", 6380).Info();
            var result = info[7].First(x => x.Key == "db0").Value;

            var indexOfCount = result.IndexOf('=') + 1;
            var countSize = result.IndexOf(',') - indexOfCount;


            return long.Parse(result.Substring(indexOfCount, countSize));
        }

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect("transaction.redis.cache.windows.net:6380,password=AoAo9CkdCyUXn8fMYwX+6xbZvn3Mez98uEdy7aNZZJQ=,ssl=True,abortConnect=False,allowAdmin=true"));

        private IDatabase Cache => _lazyConnection.Value.GetDatabase();
    }
}
