using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace AMAK.Application.Providers.Cache {
    public class CacheService : ICacheService {

        private readonly IDistributedCache _distributedCache;

        public CacheService(IDistributedCache distributedCache) {
            _distributedCache = distributedCache;
        }

        public async Task<T?> GetData<T>(string key) {
            var cachedData = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedData)) {
                return default;
            }
            return JsonConvert.DeserializeObject<T>(cachedData);
        }

        public async Task<bool> RemoveData(string key) {
            await _distributedCache.RemoveAsync(key);
            return true;
        }

        public async Task<object> SetData<T>(string key, T value, DateTimeOffset time) {
            var options = new DistributedCacheEntryOptions {
                AbsoluteExpiration = time
            };
            var serializedData = JsonConvert.SerializeObject(value);
            await _distributedCache.SetStringAsync(key, serializedData, options);
            return true;
        }
    }
}