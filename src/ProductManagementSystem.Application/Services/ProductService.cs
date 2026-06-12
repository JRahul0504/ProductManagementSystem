using ProductManagementSystem.Application.DTOs.Products;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.Application.Services;

/// <summary>
/// Provides product application operations.
/// </summary>
public sealed class ProductService(IUnitOfWork unitOfWork, IMapper mapper) : IProductService
{
    private const int MaximumPageSize = 100;

    /// <inheritdoc />
    public async Task<ApiResponse<PagedResponse<ProductDto>>> GetProductsAsync(
        PagedRequestDto request,
        CancellationToken cancellationToken = default)
    {
        var pageNumber = Math.Max(request.PageNumber, 1);
        var pageSize = Math.Clamp(request.PageSize, 1, MaximumPageSize);
        var searchTerm = request.SearchTerm?.Trim();

        var products = await unitOfWork.Products.FindAsync(
            product =>
                (request.IncludeDeleted || !product.IsDeleted) &&
                (string.IsNullOrWhiteSpace(searchTerm) || product.ProductName.Contains(searchTerm)),
            cancellationToken);

        var sortedProducts = SortProducts(products, request.SortBy, request.SortDirection);
        var totalRecords = sortedProducts.Count;
        var pagedProducts = sortedProducts
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var productDtos = mapper.Map<IReadOnlyList<ProductDto>>(pagedProducts);
        var pagedResponse = PagedResponse<ProductDto>.Success(
            productDtos,
            pageNumber,
            pageSize,
            totalRecords,
            "Products retrieved successfully.");

        return ApiResponse<PagedResponse<ProductDto>>.Success(
            pagedResponse,
            "Products retrieved successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<ProductDto>> GetProductByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product is null || product.IsDeleted)
        {
            return ApiResponse<ProductDto>.Failure("Product was not found.");
        }

        return ApiResponse<ProductDto>.Success(
            mapper.Map<ProductDto>(product),
            "Product retrieved successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<ProductDto>> CreateProductAsync(
        CreateProductDto request,
        CancellationToken cancellationToken = default)
    {
        var product = mapper.Map<Product>(request);
        product.CreatedOn = DateTime.UtcNow;
        product.IsDeleted = false;

        await unitOfWork.Products.AddAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<ProductDto>.Success(
            mapper.Map<ProductDto>(product),
            "Product created successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<ProductDto>> UpdateProductAsync(
        int id,
        UpdateProductDto request,
        CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product is null || product.IsDeleted)
        {
            return ApiResponse<ProductDto>.Failure("Product was not found.");
        }

        mapper.Map(request, product);
        product.ModifiedOn = DateTime.UtcNow;

        unitOfWork.Products.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<ProductDto>.Success(
            mapper.Map<ProductDto>(product),
            "Product updated successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<bool>> DeleteProductAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var product = await unitOfWork.Products.GetByIdAsync(id, cancellationToken);

        if (product is null)
        {
            return ApiResponse<bool>.Failure("Product was not found.");
        }

        product.IsDeleted = true;
        product.DeletedBy = product.ModifiedBy ?? product.CreatedBy;
        product.DeletedOn = DateTime.UtcNow;
        product.ModifiedOn = product.DeletedOn;

        unitOfWork.Products.Update(product);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Success(true, "Product deleted successfully.");
    }

    private static IReadOnlyList<Product> SortProducts(
        IReadOnlyList<Product> products,
        string? sortBy,
        string? sortDirection)
    {
        var isDescending = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase);

        return NormalizeSortBy(sortBy) switch
        {
            "id" => ApplySort(products, product => product.Id, isDescending),
            "createdon" => ApplySort(products, product => product.CreatedOn, isDescending),
            "modifiedon" => ApplySort(products, product => product.ModifiedOn, isDescending),
            "deletedon" => ApplySort(products, product => product.DeletedOn, isDescending),
            "createdby" => ApplySort(products, product => product.CreatedBy, isDescending),
            "productname" or _ => ApplySort(products, product => product.ProductName, isDescending)
        };
    }

    private static IReadOnlyList<Product> ApplySort<TKey>(
        IEnumerable<Product> products,
        Func<Product, TKey> keySelector,
        bool isDescending)
    {
        return (isDescending
                ? products.OrderByDescending(keySelector)
                : products.OrderBy(keySelector))
            .ToList();
    }

    private static string NormalizeSortBy(string? sortBy)
    {
        return string.IsNullOrWhiteSpace(sortBy)
            ? "productname"
            : sortBy.Trim().Replace("_", string.Empty, StringComparison.Ordinal).ToLowerInvariant();
    }
}
