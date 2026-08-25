using Ixp.Interview.Api.Middleware;

namespace Ixp.Interview.Api.Auth;

public sealed record CurrentUser(string Id, string DisplayName, IReadOnlyList<string> Roles);

public interface ICurrentUserAccessor
{
    CurrentUser User { get; }
}

public sealed class CurrentUserAccessor(IHttpContextAccessor accessor) : ICurrentUserAccessor
{
    public CurrentUser User =>
        accessor.HttpContext?.Items[CurrentUserMiddleware.ItemKey] as CurrentUser
        ?? throw new InvalidOperationException("Current user was not set for this request.");
}
