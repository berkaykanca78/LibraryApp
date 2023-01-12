using FluentValidation;
using LibraryApp.Data.Entities;

namespace LibraryApp.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Surname).NotEmpty();
            RuleFor(c => c.Email).NotEmpty();
        }
    }
}
