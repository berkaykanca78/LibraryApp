using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryCategorySpec : BaseSpecification<Book>
    {
        public BookQueryCategorySpec(int? categoryId)
        {
            if (categoryId.HasValue)
                Criteria = p => p.CategoryId == categoryId.Value;
        }
    }
}
