namespace ProductManagementSystem.Application.DTOs.Common;

/// <summary>
/// Represents a paginated query request.
/// </summary>
public sealed class PagedRequestDto
{
    /// <summary>
    /// Gets or sets the requested page number.
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Gets or sets the requested page size.
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Gets or sets the optional search term.
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Gets or sets the field used for sorting.
    /// </summary>
    public string? SortBy { get; set; } = "ProductName";

    /// <summary>
    /// Gets or sets the sort direction. Supported values are asc and desc.
    /// </summary>
    public string? SortDirection { get; set; } = "asc";

    /// <summary>
    /// Gets or sets a value indicating whether soft-deleted records should be included.
    /// </summary>
    public bool IncludeDeleted { get; set; }
}
