using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class AuthorQueryOrderBySpec : BaseSpecification<Author>
    {
        public AuthorQueryOrderBySpec(string orderBy, string ordering)
        {
            if (ordering == "desc")
            {
                if (orderBy == "age")
                    AddOrderBy(x => x.DateOfBirth);
                if (orderBy == "bookNumber")
                    AddOrderByDescending(x => x.BookAuthor.Books.Count());
                if (orderBy == "lastBookDate")
                    AddOrderByDescending(x => x.BookAuthor.Books.Select(field => field.PublishedDate).First());
            }
            else if (ordering == "asc")
            {
                if (orderBy == "age")
                    AddOrderByDescending(x => x.DateOfBirth);
                if (orderBy == "bookNumber")
                    AddOrderBy(x => x.BookAuthor.Books.Count());
                if (orderBy == "lastBookDate")
                    AddOrderBy(x => x.BookAuthor.Books.Select(field => field.PublishedDate).First());
            }

           
        }
    }
}
