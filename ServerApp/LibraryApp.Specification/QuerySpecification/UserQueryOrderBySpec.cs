using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQueryOrderBySpec : BaseSpecification<User>
    {
        public UserQueryOrderBySpec(string orderBy, string ordering)
        {
            if (ordering == "desc")
            {
                if (orderBy == "age")
                    AddOrderBy(x => x.DateOfBirth);
                if (orderBy == "created")
                    AddOrderByDescending(x => x.CreatedDate);
                if (orderBy == "lastActive")
                    AddOrderByDescending(x => x.LastActiveDate);
            }
            else if (ordering == "asc")
            {
                if (orderBy == "age")
                    AddOrderByDescending(x => x.DateOfBirth);
                if (orderBy == "created")
                    AddOrderBy(x => x.CreatedDate);
                if (orderBy == "lastActive")
                    AddOrderBy(x => x.LastActiveDate);
            }

            
        }
    }
}
