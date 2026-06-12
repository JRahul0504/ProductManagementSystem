using ProductManagementSystem.Application.DTOs.Items;
using ProductManagementSystem.Application.Interfaces.Services;

namespace ProductManagementSystem.Application.Services;

/// <summary>
/// Provides item application operations.
/// </summary>
public sealed class ItemService(IUnitOfWork unitOfWork, IMapper mapper) : IItemService
{
    /// <inheritdoc />
    public async Task<ApiResponse<IReadOnlyList<ItemDto>>> GetItemsByProductIdAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        var productExists = await unitOfWork.Products.AnyAsync(
            product => product.Id == productId && !product.IsDeleted,
            cancellationToken);

        if (!productExists)
        {
            return ApiResponse<IReadOnlyList<ItemDto>>.Failure("Product was not found.");
        }

        var items = await unitOfWork.Items.FindAsync(
            item => item.ProductId == productId,
            cancellationToken);

        return ApiResponse<IReadOnlyList<ItemDto>>.Success(
            mapper.Map<IReadOnlyList<ItemDto>>(items),
            "Items retrieved successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<ItemDto>> GetItemByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var item = await unitOfWork.Items.GetByIdAsync(id, cancellationToken);

        if (item is null)
        {
            return ApiResponse<ItemDto>.Failure("Item was not found.");
        }

        return ApiResponse<ItemDto>.Success(
            mapper.Map<ItemDto>(item),
            "Item retrieved successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<ItemDto>> CreateItemAsync(
        CreateItemDto request,
        CancellationToken cancellationToken = default)
    {
        var productExists = await unitOfWork.Products.AnyAsync(
            product => product.Id == request.ProductId && !product.IsDeleted,
            cancellationToken);

        if (!productExists)
        {
            return ApiResponse<ItemDto>.Failure("Product was not found.");
        }

        var item = mapper.Map<Item>(request);

        await unitOfWork.Items.AddAsync(item, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<ItemDto>.Success(
            mapper.Map<ItemDto>(item),
            "Item created successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<ItemDto>> UpdateItemAsync(
        int id,
        UpdateItemDto request,
        CancellationToken cancellationToken = default)
    {
        var item = await unitOfWork.Items.GetByIdAsync(id, cancellationToken);

        if (item is null)
        {
            return ApiResponse<ItemDto>.Failure("Item was not found.");
        }

        mapper.Map(request, item);

        unitOfWork.Items.Update(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<ItemDto>.Success(
            mapper.Map<ItemDto>(item),
            "Item updated successfully.");
    }

    /// <inheritdoc />
    public async Task<ApiResponse<bool>> DeleteItemAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var item = await unitOfWork.Items.GetByIdAsync(id, cancellationToken);

        if (item is null)
        {
            return ApiResponse<bool>.Failure("Item was not found.");
        }

        unitOfWork.Items.Delete(item);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse<bool>.Success(true, "Item deleted successfully.");
    }
}
