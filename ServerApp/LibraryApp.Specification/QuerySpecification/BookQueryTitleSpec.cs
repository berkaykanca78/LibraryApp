using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryTitleSpec : BaseSpecification<Book>
    {
        public BookQueryTitleSpec(string title)
        {
            if (!string.IsNullOrEmpty(title))
                Criteria = p => p.Title.Contains(title);
        }
    }
}
