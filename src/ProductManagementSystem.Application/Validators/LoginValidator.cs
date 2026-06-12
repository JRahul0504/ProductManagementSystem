using ProductManagementSystem.Application.DTOs.Auth;

namespace ProductManagementSystem.Application.Validators;

/// <summary>
/// Validates login requests.
/// </summary>
public sealed class LoginValidator : AbstractValidator<LoginRequestDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginValidator"/> class.
    /// </summary>
    public LoginValidator()
    {
        RuleFor(request => request.UserNameOrEmail)
            .NotEmpty()
            .WithMessage("User name or email is required.")
            .MaximumLength(256)
            .WithMessage("User name or email must not exceed 256 characters.");

        RuleFor(request => request.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(100)
            .WithMessage("Password must not exceed 100 characters.");
    }
}
