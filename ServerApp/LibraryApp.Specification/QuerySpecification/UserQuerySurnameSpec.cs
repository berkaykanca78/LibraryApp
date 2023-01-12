using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQuerySurnameSpec : BaseSpecification<User>
    {
        public UserQuerySurnameSpec(string surname)
        {
            if (!string.IsNullOrEmpty(surname))
                Criteria = p => p.Name.Contains(surname);
        }
    }
}
