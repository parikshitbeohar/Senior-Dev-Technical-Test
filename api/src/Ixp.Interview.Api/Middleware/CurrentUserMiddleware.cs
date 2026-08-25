using Ixp.Interview.Api.Auth;

namespace Ixp.Interview.Api.Middleware;

public sealed class CurrentUserMiddleware(RequestDelegate next)
{
    public const string ItemKey = "Interview.CurrentUser";

    public async Task InvokeAsync(HttpContext context)
    {
        var userId = context.Request.Headers["X-User-Id"].FirstOrDefault()
            ?? "entra-guid-inspector-001";
        var displayName = context.Request.Headers["X-User-Display-Name"].FirstOrDefault()
            ?? "Alex Patel";
        var roles = (context.Request.Headers["X-Roles"].FirstOrDefault() ?? "Forms.Inspector")
            .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        context.Items[ItemKey] = new CurrentUser(userId, displayName, roles);
        await next(context);
    }
}
