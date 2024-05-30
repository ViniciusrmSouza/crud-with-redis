using CRUDDockerizado.CRUDDockerizado.Domain.Caching;
using Microsoft.Extensions.Caching.Distributed;

namespace CRUDDockerizado.CRUDDockerizado.Infrastructure.Caching;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(60)
        };
    }

    public async Task SetAsync(string key, string value)
    {
        await _cache.SetStringAsync(key, value, _options);
    }

    public async Task<string> GetAsync(string key)
    {
        return await _cache.GetStringAsync(key) ?? "Nenhum valor encontrado";
    }
}