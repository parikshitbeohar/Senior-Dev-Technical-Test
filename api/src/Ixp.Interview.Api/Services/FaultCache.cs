using Ixp.Interview.Api.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Ixp.Interview.Api.Services;

public interface IFaultCache
{
    bool TryGetList(string key, out FaultListResponse? value);
    void SetList(string key, FaultListResponse value);
    void InvalidateLists();
    string BuildListKey(string? location, string? from, string? to, int page, int pageSize, string userId);
}

public sealed class FaultCache(IMemoryCache memoryCache) : IFaultCache
{
    public const string ListPrefix = "ixp:interview:faults:list";
    private CancellationTokenSource _listVersion = new();

    public bool TryGetList(string key, out FaultListResponse? value) =>
        memoryCache.TryGetValue(key, out value);

    public void SetList(string key, FaultListResponse value)
    {
        var options = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(60))
            .AddExpirationToken(new CancellationChangeToken(_listVersion.Token));
        memoryCache.Set(key, value, options);
    }

    public void InvalidateLists()
    {
        var previous = Interlocked.Exchange(ref _listVersion, new CancellationTokenSource());
        previous.Cancel();
        previous.Dispose();
    }

    public string BuildListKey(
        string? location,
        string? from,
        string? to,
        int page,
        int pageSize,
        string userId) =>
        $"{ListPrefix}:{userId}:{location}:{from}:{to}:{page}:{pageSize}";
}
