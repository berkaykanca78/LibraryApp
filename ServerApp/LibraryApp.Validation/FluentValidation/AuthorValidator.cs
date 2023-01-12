using FluentValidation;
using LibraryApp.Data.Entities;

namespace LibraryApp.Validation.FluentValidation
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.LastName).NotEmpty();
        }
    }
}
