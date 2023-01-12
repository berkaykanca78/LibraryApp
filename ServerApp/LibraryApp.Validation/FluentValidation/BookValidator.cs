using FluentValidation;
using LibraryApp.Data.Entities;

namespace LibraryApp.Validation.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(c => c.Title).NotEmpty();
        }
    }
}
