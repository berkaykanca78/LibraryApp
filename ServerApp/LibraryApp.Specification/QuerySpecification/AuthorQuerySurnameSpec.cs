using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class AuthorQuerySurnameSpec : BaseSpecification<Author>
    {
        public AuthorQuerySurnameSpec(string surname)
        {
            if (!string.IsNullOrEmpty(surname))
                Criteria = p => p.LastName.Contains(surname);
        }
    }
}
