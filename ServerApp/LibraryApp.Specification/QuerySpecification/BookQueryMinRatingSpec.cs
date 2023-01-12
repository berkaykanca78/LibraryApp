using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryMinRatingSpec : BaseSpecification<Book>
    {
        public BookQueryMinRatingSpec(int? minRating)
        {
            if (minRating.HasValue)
                Criteria = p => p.Rating >= minRating;
        }
    }
}
