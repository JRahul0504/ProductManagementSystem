using ProductManagementSystem.API.Middleware;

namespace ProductManagementSystem.API.Extensions;

/// <summary>
/// Provides middleware registration extensions.
/// </summary>
public static class MiddlewareApplicationBuilderExtensions
{
    /// <summary>
    /// Registers API middleware in the required order.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseApiMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<GlobalExceptionMiddleware>();
        app.UseMiddleware<SecurityHeadersMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        return app;
    }
}
