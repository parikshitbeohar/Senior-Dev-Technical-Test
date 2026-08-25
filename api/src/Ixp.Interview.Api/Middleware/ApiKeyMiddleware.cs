namespace Ixp.Interview.Api.Middleware;

public sealed class ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
{
    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger")
            || context.Request.Path.StartsWithSegments("/favicon.ico"))
        {
            await next(context);
            return;
        }

        var expected = configuration["Interview:ApiKey"];
        var provided = context.Request.Headers["X-Api-Key"].FirstOrDefault();
        if (string.IsNullOrEmpty(expected) || provided != expected)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(new { message = "Unauthorized" });
            return;
        }

        await next(context);
    }
}
