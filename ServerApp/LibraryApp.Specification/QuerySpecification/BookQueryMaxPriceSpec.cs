using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryMaxPriceSpec : BaseSpecification<Book>
    {
        public BookQueryMaxPriceSpec(int? maxPrice)
        {
            if (maxPrice.HasValue)
                Criteria = p => p.Price <= maxPrice;
        }
    }
}
