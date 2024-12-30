using FluentValidation;
using JwtBasedAuthenticationAndAuthorization.Payloads.User;

namespace JwtBasedAuthenticationAndAuthorization.DataValidations.User
{
    public class UserRegisterRequestValidator : AbstractValidator<UserRegisterRequest>
    {
        public UserRegisterRequestValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("First name is required")
                .MaximumLength(50)
                .WithMessage("Maximum length cannot be greater than 50");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last name is required")
                .MaximumLength(50)
                .WithMessage("Maximum length cannot be greater than 50");

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
            
            RuleFor(x => x.BirthDateTime)
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Birth date must be before or same current date");
        }
    }
}
