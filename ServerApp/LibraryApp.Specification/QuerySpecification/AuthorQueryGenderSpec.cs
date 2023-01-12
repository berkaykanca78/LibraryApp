using LibraryApp.Common.Enums;
using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class AuthorQueryGenderSpec : BaseSpecification<Author>
    {
        public AuthorQueryGenderSpec(GenderEnum? gender)
        {
            if (gender.HasValue)
                Criteria = p => p.Gender == gender;
        }
    }
}
