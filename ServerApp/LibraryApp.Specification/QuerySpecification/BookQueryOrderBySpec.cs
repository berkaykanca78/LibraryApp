using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryOrderBySpec : BaseSpecification<Book>
    {
        public BookQueryOrderBySpec(string orderBy, string ordering)
        {
            if (ordering == "desc")
            {
                if (orderBy == "price")
                    AddOrderByDescending(x => x.Price);
                if (orderBy == "created")
                    AddOrderByDescending(x => x.CreatedDate);
                if (orderBy == "rating")
                    AddOrderByDescending(x => x.Rating);
            }
            else if (ordering == "asc")
            {
                if (orderBy == "price")
                    AddOrderBy(x => x.Price);
                if (orderBy == "created")
                    AddOrderBy(x => x.CreatedDate);
                if (orderBy == "rating")
                    AddOrderBy(x => x.Rating);
            }

           
        }
    }
}
