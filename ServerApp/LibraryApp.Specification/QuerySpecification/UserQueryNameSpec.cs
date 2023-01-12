using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQueryNameSpec : BaseSpecification<User>
    {
        public UserQueryNameSpec(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Criteria = p => p.Name.Contains(name);
        }
    }
}
