using FluentValidation;
using FluentValidation.Results;
using System.Collections.Generic;
using ValidationException = FluentValidation.ValidationException;

namespace LibraryApp.Common.Helpers
{
    public static class ValidateHelper
    {
        public static (bool, List<ValidationFailure>) Validate(IValidator validator, object entity)
        {
            var context = new ValidationContext<object>(entity);
            var result = validator.Validate(context);
            if (result.Errors.Count > 0)
            {
                return (false, result.Errors);
            }
            return (true, null);
        }
    }
}
