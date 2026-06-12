using ProductManagementSystem.Application.DTOs.Items;

namespace ProductManagementSystem.Application.Validators;

/// <summary>
/// Validates item update requests.
/// </summary>
public sealed class UpdateItemValidator : AbstractValidator<UpdateItemDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateItemValidator"/> class.
    /// </summary>
    public UpdateItemValidator()
    {
        RuleFor(request => request.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.");
    }
}
