using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class AuthorQueryNameSpec : BaseSpecification<Author>
    {
        public AuthorQueryNameSpec(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Criteria = p => p.FirstName.Contains(name);
        }
    }
}
