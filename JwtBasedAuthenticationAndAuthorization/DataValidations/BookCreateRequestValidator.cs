using FluentValidation;
using JwtBasedAuthenticationAndAuthorization.Payloads;

namespace JwtBasedAuthenticationAndAuthorization.DataValidations
{
    public class BookCreateRequestValidator : AbstractValidator<BookCreateRequest>
    {
        public BookCreateRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required");
            
            RuleFor(x => x.Price)
                .InclusiveBetween(5, 100)
                .WithMessage("Price must be between 5 and 100");
        }
    }
}
