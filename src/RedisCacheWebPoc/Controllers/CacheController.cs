using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RedisCacheWebPoc.Model;
using StackExchange.Redis;
using System;
using System.Diagnostics;
using System.Text;

namespace RedisCacheWebPoc.Controllers
{
    public class CacheController : Controller
    {
        public IActionResult Add(int count)
        {
            var response = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                var transaction = new TransactionModel
                {
                    TransactionId = Guid.NewGuid(),
                    AcquirerTransactionId = i.ToString(),
                    Amount = new Random(i).Next()
                };

                try
                {
                    var timer = new Stopwatch();
                    timer.Start();
                    Cache.StringSet(i.ToString(), JsonConvert.SerializeObject(transaction), expiry: new TimeSpan(0, 1, 0));
                    timer.Stop();

                    response.AppendFormat("#{0} added in {1}ms\r\n", i, timer.ElapsedMilliseconds);
                }
                catch (Exception)
                {
                    response.AppendFormat("#{0} error", i);
                }
            }

            return Content(response.ToString());
        }

        private readonly Lazy<ConnectionMultiplexer> _lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
            ConnectionMultiplexer.Connect("transaction.redis.cache.windows.net:6380,password=AoAo9CkdCyUXn8fMYwX+6xbZvn3Mez98uEdy7aNZZJQ=,ssl=True,abortConnect=False"));

        private IDatabase Cache => _lazyConnection.Value.GetDatabase();
    }
}
