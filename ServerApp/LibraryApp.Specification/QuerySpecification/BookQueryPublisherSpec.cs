using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryPublisherSpec : BaseSpecification<Book>
    {
        public BookQueryPublisherSpec(int? publisherId)
        {
            if (publisherId.HasValue)
                Criteria = p => p.PublisherId == publisherId.Value;
        }
    }
}
