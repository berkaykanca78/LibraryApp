using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;
using static System.Net.Mime.MediaTypeNames;

namespace LibraryApp.Specification.QuerySpecification
{
    public class AuthorQueryMinMaxAgeSpec : BaseSpecification<Author>
    {
        public AuthorQueryMinMaxAgeSpec(int? minAgeParams, int? maxAgeParams)
        {
            DateTime today = DateTime.Now;
            if(minAgeParams.HasValue)
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
