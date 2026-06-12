using ProductManagementSystem.Application.DTOs.Items;

namespace ProductManagementSystem.Application.Validators;

/// <summary>
/// Validates item creation requests.
/// </summary>
public sealed class CreateItemValidator : AbstractValidator<CreateItemDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateItemValidator"/> class.
    /// </summary>
    public CreateItemValidator()
    {
        RuleFor(request => request.ProductId)
            .GreaterThan(0)
            .WithMessage("Product id must be greater than zero.");

        RuleFor(request => request.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}
