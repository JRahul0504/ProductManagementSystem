namespace ProductManagementSystem.Application.DTOs.Common;

/// <summary>
/// Represents a standardized application response.
/// </summary>
/// <typeparam name="T">The response data type.</typeparam>
public sealed class ApiResponse<T>
{
    /// <summary>
    /// Gets a value indicating whether the request completed successfully.
    /// </summary>
    public bool Succeeded { get; init; }

    /// <summary>
    /// Gets the response message.
    /// </summary>
    public string Message { get; init; } = string.Empty;

    /// <summary>
    /// Gets the response payload.
    /// </summary>
    public T? Data { get; init; }

    /// <summary>
    /// Gets the response errors.
    /// </summary>
    public IReadOnlyList<string> Errors { get; init; } = [];

    /// <summary>
    /// Creates a successful response.
    /// </summary>
    /// <param name="data">The response payload.</param>
    /// <param name="message">The response message.</param>
    /// <returns>A successful API response.</returns>
    public static ApiResponse<T> Success(T data, string message = "Request completed successfully.")
    {
        return new ApiResponse<T>
        {
            Succeeded = true,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// Creates a failed response.
    /// </summary>
    /// <param name="message">The response message.</param>
    /// <param name="errors">The response errors.</param>
    /// <returns>A failed API response.</returns>
    public static ApiResponse<T> Failure(string message, IReadOnlyList<string>? errors = null)
    {
        return new ApiResponse<T>
        {
            Succeeded = false,
            Message = message,
            Errors = errors ?? []
        };
    }
}
