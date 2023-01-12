using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQueryMinMaxAgeSpec : BaseSpecification<User>
    {
        public UserQueryMinMaxAgeSpec(int? minAgeParams, int? maxAgeParams)
        {
            DateTime today = DateTime.Now;
            if (minAgeParams.HasValue)
            {
                int minAge = minAgeParams.Value;
                DateTime minDate = today.AddYears(-(minAge));
                Criteria = p => p.DateOfBirth <= minDate;
            }
            if (maxAgeParams.HasValue)
            {
                int maxAge = maxAgeParams.Value;
                DateTime maxDate = today.AddYears(-(maxAge));
                Criteria = p => p.DateOfBirth >= maxDate;
            }
        }
    }
}
