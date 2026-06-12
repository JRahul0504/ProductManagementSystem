using System.Net;
using FluentValidation;
using ProductManagementSystem.Domain.Exceptions;

namespace ProductManagementSystem.API.Middleware;

/// <summary>
/// Converts unhandled exceptions into standardized API responses.
/// </summary>
public sealed class GlobalExceptionMiddleware(
    RequestDelegate next,
    ILogger<GlobalExceptionMiddleware> logger)
{
    /// <summary>
    /// Executes the middleware.
    /// </summary>
    /// <param name="context">The current HTTP context.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, message, errors) = exception switch
        {
            ValidationException validationException => (
                HttpStatusCode.BadRequest,
                "Validation failed.",
                validationException.Errors.Select(error => error.ErrorMessage).ToArray()),

            BadRequestException badRequestException => (
                HttpStatusCode.BadRequest,
                badRequestException.Message,
                Array.Empty<string>()),

            NotFoundException notFoundException => (
                HttpStatusCode.NotFound,
                notFoundException.Message,
                Array.Empty<string>()),

            UnauthorizedAccessException unauthorizedAccessException => (
                HttpStatusCode.Unauthorized,
                unauthorizedAccessException.Message,
                Array.Empty<string>()),

            _ => (
                HttpStatusCode.InternalServerError,
                "An unexpected error occurred while processing the request.",
                Array.Empty<string>())
        };

        if (statusCode == HttpStatusCode.InternalServerError)
        {
            logger.LogError(exception, "Unhandled exception occurred while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
        }
        else
        {
            logger.LogWarning(exception, "Handled exception occurred while processing {Method} {Path}.", context.Request.Method, context.Request.Path);
        }

        var response = ApiResponse<object>.Failure(message, errors);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(response);
    }
}
