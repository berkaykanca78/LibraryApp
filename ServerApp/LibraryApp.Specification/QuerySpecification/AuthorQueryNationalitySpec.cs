using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class AuthorQueryNationalitySpec : BaseSpecification<Author>
    {
        public AuthorQueryNationalitySpec(string nationality)
        {
            if (!string.IsNullOrWhiteSpace(nationality))
            {
                if(nationality == "Domestic")
                    Criteria = p => p.Country == "Türkiye";
                if (nationality == "Foreign")
                    Criteria = p => p.Country != "Türkiye";
            }
        }
    }
}
