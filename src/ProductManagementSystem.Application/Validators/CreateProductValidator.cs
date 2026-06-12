using ProductManagementSystem.Application.DTOs.Products;

namespace ProductManagementSystem.Application.Validators;

/// <summary>
/// Validates product creation requests.
/// </summary>
public sealed class CreateProductValidator : AbstractValidator<CreateProductDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateProductValidator"/> class.
    /// </summary>
    public CreateProductValidator()
    {
        RuleFor(request => request.ProductName)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(255)
            .WithMessage("Product name must not exceed 255 characters.");

        RuleFor(request => request.CreatedBy)
            .NotEmpty()
            .WithMessage("Created by is required.")
            .MaximumLength(100)
            .WithMessage("Created by must not exceed 100 characters.");
    }
}
