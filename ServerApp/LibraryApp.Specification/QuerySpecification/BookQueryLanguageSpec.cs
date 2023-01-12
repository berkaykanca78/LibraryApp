using LibraryApp.Common.Enums;
using LibraryApp.Common.Specification.Concrete;
using LibraryApp.Data.Entities;

namespace LibraryApp.Specification.QuerySpecification
{
    public class BookQueryLanguageSpec : BaseSpecification<Book>
    {
        public BookQueryLanguageSpec(LanguagesEnum? languageId)
        {
            if (languageId.HasValue)
                Criteria = p => p.Language == languageId;
        }
    }
}
