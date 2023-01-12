using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQueryUserNameSpec : BaseSpecification<User>
    {
        public UserQueryUserNameSpec(string userName)
        {
            if (!string.IsNullOrEmpty(userName))
                Criteria = p => p.Name.Contains(userName);
        }
    }
}
