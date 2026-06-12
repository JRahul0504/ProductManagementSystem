using System.Diagnostics;

namespace ProductManagementSystem.API.Middleware;

/// <summary>
/// Logs incoming HTTP requests and completed responses.
/// </summary>
public sealed class RequestLoggingMiddleware(
    RequestDelegate next,
    ILogger<RequestLoggingMiddleware> logger)
{
    /// <summary>
    /// Executes the middleware.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = Stopwatch.StartNew();

        logger.LogInformation(
            "HTTP request started: {Method} {Path}.",
            context.Request.Method,
            context.Request.Path);

        await next(context);

        stopwatch.Stop();

        logger.LogInformation(
            "HTTP request completed: {Method} {Path} responded {StatusCode} in {ElapsedMilliseconds} ms.",
            context.Request.Method,
            context.Request.Path,
            context.Response.StatusCode,
            stopwatch.ElapsedMilliseconds);
    }
}
