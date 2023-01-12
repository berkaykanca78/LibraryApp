using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryMaxRatingSpec : BaseSpecification<Book>
    {
        public BookQueryMaxRatingSpec(int? maxRating)
        {
            if (maxRating.HasValue)
                Criteria = p => p.Rating <= maxRating;
        }
    }
}
