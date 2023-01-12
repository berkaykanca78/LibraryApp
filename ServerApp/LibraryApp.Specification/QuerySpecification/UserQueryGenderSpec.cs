using LibraryApp.Common.Enums;
using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Specification.QuerySpecification
{
    public class UserQueryGenderSpec : BaseSpecification<User>
    {
        public UserQueryGenderSpec(GenderEnum? gender)
        {
            if (gender.HasValue)
                Criteria = p => (GenderEnum)p.Gender == gender;
        }
    }
}
