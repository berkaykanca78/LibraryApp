using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryMinPriceSpec : BaseSpecification<Book>
    {
        public BookQueryMinPriceSpec(int? minPrice)
        {
            if (minPrice.HasValue)
                Criteria = p => p.Price >= minPrice;
        }
    }
}
