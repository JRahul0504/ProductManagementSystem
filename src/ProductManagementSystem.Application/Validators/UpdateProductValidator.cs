using ProductManagementSystem.Application.DTOs.Products;

namespace ProductManagementSystem.Application.Validators;

/// <summary>
/// Validates product update requests.
/// </summary>
public sealed class UpdateProductValidator : AbstractValidator<UpdateProductDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateProductValidator"/> class.
    /// </summary>
    public UpdateProductValidator()
    {
        RuleFor(request => request.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(255)
            .WithMessage("Product name must not exceed 255 characters.");

        RuleFor(request => request.ModifiedBy)
            .NotEmpty()
            .WithMessage("Modified by is required.")
            .MaximumLength(100)
            .WithMessage("Modified by must not exceed 100 characters.");
    }
}
