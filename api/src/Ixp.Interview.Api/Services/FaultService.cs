using Ixp.Interview.Api.Auth;
using Ixp.Interview.Api.Data;
using Ixp.Interview.Api.Models;

namespace Ixp.Interview.Api.Services;

public interface IFaultService
{
    FaultListResponse GetFaults(string? location, string? from, string? to, int page, int pageSize, string correlationId);
    Fault? GetFault(Guid id);
    Fault CreateFault(CreateFaultRequest request);
}

public sealed class FaultService(
    FaultStore store,
    IFaultCache cache,
    ICurrentUserAccessor currentUser,
    IHttpContextAccessor httpContextAccessor) : IFaultService
{
    public const string FaultsReaderRole = "Forms.FaultsReader";

    public FaultListResponse GetFaults(
        string? location,
        string? from,
        string? to,
        int page,
        int pageSize,
        string correlationId)
    {
        var user = currentUser.User;
        var cacheKey = cache.BuildListKey(location, from, to, page, pageSize, user.Id);
        if (cache.TryGetList(cacheKey, out var cached) && cached is not null)
        {
            SetCacheHeader("HIT");
            cached.CorrelationId = correlationId;
            return cached;
        }

        SetCacheHeader("MISS");

        IEnumerable<Fault> faults = store.Snapshot();

        var canReadAll = !user.Roles.Contains(FaultsReaderRole);
        if (!canReadAll)
        {
            faults = faults.Where(f => f.UserId == user.Id);
        }

        if (!string.IsNullOrWhiteSpace(location))
        {
            faults = faults.Where(f => f.Location.Equals(location, StringComparison.OrdinalIgnoreCase));
        }

        if (DateOnly.TryParse(from, out var fromDate))
        {
            faults = faults.Where(f => DateOnly.FromDateTime(f.CreatedAtUtc.UtcDateTime) >= fromDate);
        }

        if (DateOnly.TryParse(to, out var toDate))
        {
            faults = faults.Where(f => DateOnly.FromDateTime(f.CreatedAtUtc.UtcDateTime) <= toDate);
        }

        var ordered = faults
            .OrderByDescending(f => f.SubmittedAtUtc ?? f.CreatedAtUtc)
            .ToList();

        var start = page * pageSize;
        var items = ordered.Skip(start).Take(pageSize).ToList();

        var response = new FaultListResponse
        {
            Items = items,
            Total = ordered.Count,
            Page = page,
            PageSize = pageSize,
            CorrelationId = correlationId,
        };

        cache.SetList(cacheKey, response);
        return response;
    }

    public Fault? GetFault(Guid id) => store.Get(id);

    public Fault CreateFault(CreateFaultRequest request)
    {
        var user = currentUser.User;
        var now = DateTimeOffset.UtcNow;
        var fault = new Fault
        {
            Id = Guid.NewGuid(),
            ResponseId = Guid.NewGuid(),
            Description = request.Description,
            Category = request.Category,
            Area = request.Area,
            Location = request.Location,
            Title = string.IsNullOrWhiteSpace(request.Title) ? "*" : request.Title,
            CreatedAtUtc = now,
            SubmittedAtUtc = now,
            IsSubmitted = true,
            UserId = user.Id,
            UserDisplayName = user.DisplayName,
        };

        store.Add(fault);
        return fault;
    }

    private void SetCacheHeader(string value)
    {
        var response = httpContextAccessor.HttpContext?.Response;
        if (response is not null && !response.HasStarted)
        {
            response.Headers["X-Cache"] = value;
        }
    }
}
