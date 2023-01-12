using LibraryApp.Common.Enums;
using LibraryApp.Data.Dtos.Base;

namespace LibraryApp.Data.Dtos.Book
{
    public class BookQueryParams: BaseQueryParams
    {
        public string Title { get; set; }
        public int? Category { get; set; }
        public int? Publisher { get; set; }
        public int? Author { get; set; }
        public MediaTypesEnum? MediaType { get; set; }
        public LanguagesEnum? Language { get; set; }
        public int? MinRating { get; set; }
        public int? MaxRating { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}
