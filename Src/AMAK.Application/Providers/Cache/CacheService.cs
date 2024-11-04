using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AMAK.Application.Providers.Cache {
    public class CacheService(IDistributedCache distributedCache) : ICacheService
    {
        public async Task<T?> GetData<T>(string key) {
            var cachedData = await distributedCache.GetStringAsync(key);
            return string.IsNullOrEmpty(cachedData) ? default : JsonConvert.DeserializeObject<T>(cachedData);
        }

        public async Task<bool> RemoveData(string key) {
            await distributedCache.RemoveAsync(key);
            return true;
        }

        public async Task<object> SetData<T>(string key, T value, DateTimeOffset time) {
            var options = new DistributedCacheEntryOptions {
                AbsoluteExpiration = time
            };
            var serializedData = JsonConvert.SerializeObject(value);
            await distributedCache.SetStringAsync(key, serializedData, options);
            return true;
        }
    }
}