using System.Text.Json;
using CargoCoordinationPlatform.Infrastructure.Interfaces;
using StackExchange.Redis;

namespace CargoCoordinationPlatform.Infrastructure.Services;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    public RedisCacheService()
    {
        ConnectionMultiplexer _redis = ConnectionMultiplexer.Connect("localhost:6379");
        _database = _redis.GetDatabase();
    }
    public async Task<T> GetOrCreateAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
    {
        var cachedValue = await _database.StringGetAsync(key);
        if (cachedValue.HasValue)
        {
            var result = JsonSerializer.Deserialize<T>(cachedValue.ToString());
            if (result is not null)
            {
                return result;
            }
        }

        var value = await factory();
        if (value != null)
        {
            await _database.StringSetAsync(key, JsonSerializer.Serialize(value), expiration);
        }
        return value;
    }
}