using Microsoft.Extensions.Options;
using ProductManagementSystem.API.Configuration;

namespace ProductManagementSystem.API.Middleware;

/// <summary>
/// Applies common HTTP security headers to API responses.
/// </summary>
public sealed class SecurityHeadersMiddleware(
    RequestDelegate next,
    IOptions<SecurityHeadersOptions> options)
{
    private readonly SecurityHeadersOptions securityHeadersOptions = options.Value;

    /// <summary>
    /// Executes the middleware.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        context.Response.Headers.TryAdd("X-Content-Type-Options", "nosniff");
        context.Response.Headers.TryAdd("X-Frame-Options", "DENY");
        context.Response.Headers.TryAdd("X-XSS-Protection", "0");
        context.Response.Headers.TryAdd("Referrer-Policy", securityHeadersOptions.ReferrerPolicy);
        context.Response.Headers.TryAdd("Permissions-Policy", securityHeadersOptions.PermissionsPolicy);

        if (!context.Request.Path.StartsWithSegments("/swagger"))
        {
            context.Response.Headers.TryAdd("Content-Security-Policy", securityHeadersOptions.ContentSecurityPolicy);
        }

        await next(context);
    }
}
