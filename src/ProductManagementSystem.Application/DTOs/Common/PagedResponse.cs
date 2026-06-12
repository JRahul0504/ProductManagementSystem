namespace ProductManagementSystem.Application.DTOs.Common;

/// <summary>
/// Represents a standardized paginated application response.
/// </summary>
/// <typeparam name="T">The item type contained in the page.</typeparam>
public sealed class PagedResponse<T>
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
    /// Gets the paged data.
    /// </summary>
    public IReadOnlyList<T> Data { get; init; } = [];

    /// <summary>
    /// Gets the current page number.
    /// </summary>
    public int PageNumber { get; init; }

    /// <summary>
    /// Gets the page size.
    /// </summary>
    public int PageSize { get; init; }

    /// <summary>
    /// Gets the total number of matching records.
    /// </summary>
    public int TotalRecords { get; init; }

    /// <summary>
    /// Gets the total number of pages.
    /// </summary>
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalRecords / (double)PageSize);

    /// <summary>
    /// Creates a successful paged response.
    /// </summary>
    /// <param name="data">The response data.</param>
    /// <param name="pageNumber">The current page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="totalRecords">The total record count.</param>
    /// <param name="message">The response message.</param>
    /// <returns>A successful paged response.</returns>
    public static PagedResponse<T> Success(
        IReadOnlyList<T> data,
        int pageNumber,
        int pageSize,
        int totalRecords,
        string message = "Request completed successfully.")
    {
        return new PagedResponse<T>
        {
            Succeeded = true,
            Message = message,
            Data = data,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalRecords = totalRecords
        };
    }
}
