using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryAuthorSpec : BaseSpecification<Book>
    {
        public BookQueryAuthorSpec(int? authorId)
        {
            if (authorId.HasValue)
                Criteria = p => p.BookAuthor.AuthorId == authorId;
        }
    }
}
