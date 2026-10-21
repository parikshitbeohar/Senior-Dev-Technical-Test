using Ixp.Interview.Api.Auth;

namespace Ixp.Interview.Api.Middleware;

public sealed class CurrentUserMiddleware(RequestDelegate next)
{
    public const string ItemKey = "Interview.CurrentUser";

    public async Task InvokeAsync(HttpContext context)
    {
        if (
            context.Request.Path.StartsWithSegments("/swagger")
            || context.Request.Path.StartsWithSegments("/favicon.ico")
        )
        {
            await next(context);
            return;
        }

        var userId = context.Request.Headers["X-User-Id"].FirstOrDefault();
        var rolesHeader = context.Request.Headers["X-Roles"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(rolesHeader))
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
            return;
        }

        var displayName = context.Request.Headers["X-User-Display-Name"].FirstOrDefault() ?? userId;
        var roles = rolesHeader.Split(
            ',',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );

        context.Items[ItemKey] = new CurrentUser(userId, displayName, roles);
        await next(context);
    }
}
