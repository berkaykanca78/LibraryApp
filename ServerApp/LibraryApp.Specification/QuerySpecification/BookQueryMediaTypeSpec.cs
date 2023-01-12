using LibraryApp.Common.Enums;
using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryMediaTypeSpec : BaseSpecification<Book>
    {
        public BookQueryMediaTypeSpec(MediaTypesEnum? mediaTypeId)
        {
            if (mediaTypeId.HasValue)
                Criteria = p => p.MediaType == mediaTypeId;
        }
    }
}
