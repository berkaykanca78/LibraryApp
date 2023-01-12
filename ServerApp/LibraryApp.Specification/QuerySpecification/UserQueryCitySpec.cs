using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQueryCitySpec : BaseSpecification<User>
    {
        public UserQueryCitySpec(string city)
        {
            if (!string.IsNullOrEmpty(city))
                Criteria = p => p.Address.City == city;
        }
    }
}
