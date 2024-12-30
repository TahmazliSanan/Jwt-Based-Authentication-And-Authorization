using FluentValidation;
using JwtBasedAuthenticationAndAuthorization.Payloads.Login;

namespace JwtBasedAuthenticationAndAuthorization.DataValidations.Login
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required")
                .MaximumLength(100)
                .WithMessage("Maximum length cannot be greater than 100");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required")
                .MinimumLength(8)
                .WithMessage("Minimum length cannot be less than 8")
                .MaximumLength(50)
                .WithMessage("Maximum length cannot be greater than 50");
        }
    }
}
