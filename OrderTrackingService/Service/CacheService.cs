using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderTrackingService.Service
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _cache;

        public CacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromHours(1) // Örnek geçerlilik süresi
            };

            var jsonData = JsonConvert.SerializeObject(value);
            await _cache.SetStringAsync(key, jsonData, options);
        }

        public async Task<T> GetCacheAsync<T>(string key)
        {
            var jsonData = await _cache.GetStringAsync(key);
            return jsonData == null ? default : JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
