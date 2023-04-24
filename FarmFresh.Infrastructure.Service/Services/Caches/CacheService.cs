using FarmFresh.Application.Interfaces.Services.Caches;
using FarmFresh.Application.Models.Caches;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace FarmFresh.Infrastructure.Service.Services.Caches
{
    public class CacheService : ICacheService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly CacheKeysService _cacheKeysService;

        public CacheService(
            IDistributedCache distributedCache,
            CacheKeysService cacheKeysService
        )
        {
            _distributedCache = distributedCache;
            _cacheKeysService = cacheKeysService;
        }

        public async Task SetDataAsync(string cacheKey, object value, TimeSpan duration)
        {
            var data = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(cacheKey, data,
                  new DistributedCacheEntryOptions
                  {
                      SlidingExpiration = duration,
                      AbsoluteExpirationRelativeToNow = duration
                  });

            _cacheKeysService.AddCacheKey(cacheKey);
        }

        public async Task<T> GetDataAsync<T>(string cacheKey)
        {
            var ipInCache = await _distributedCache.GetStringAsync(cacheKey);
            
            if (ipInCache is not null)
            {
                var data = JsonConvert.DeserializeObject<T>(ipInCache);
                return data;
            }
            else
            {
               _cacheKeysService.RemoveCacheKey(cacheKey);
            }
            
            return default;
        }

        public async Task RemoveDataAsync(string cacheKey)
        {
            await _distributedCache.RemoveAsync(cacheKey);
            _cacheKeysService.RemoveCacheKey(cacheKey);
        }

        public Task RemoveByPrefixAsync(string prefix)
        {
            var allKeys = _cacheKeysService.GetCacheKeysByPrefix(prefix);
            Parallel.ForEach(allKeys, async key =>
            {
                await RemoveDataAsync(key);
            });

            return Task.CompletedTask;
        }
    }
}
