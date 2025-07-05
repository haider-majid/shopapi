using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Application.Services
{
    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache _cache;
        private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(30);

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var cachedValue = await _cache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedValue))
                return default;

            try
            {
                return JsonSerializer.Deserialize<T>(cachedValue);
            }
            catch
            {
                return default;
            }
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? _defaultExpiration
            };

            var serializedValue = JsonSerializer.Serialize(value);
            await _cache.SetStringAsync(key, serializedValue, options);
        }

        public async Task RemoveAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task RemoveByPatternAsync(string pattern)
        {
            // Note: This is a simplified implementation
            // In a production environment, you might want to use Redis SCAN command
            // For now, we'll just remove the specific key if it matches the pattern
            await _cache.RemoveAsync(pattern);
        }

        public string GenerateKey(string entity, string operation, params object[] parameters)
        {
            var key = $"{entity}:{operation}";
            if (parameters.Length > 0)
            {
                key += ":" + string.Join(":", parameters);
            }
            return key.ToLowerInvariant();
        }
    }
}