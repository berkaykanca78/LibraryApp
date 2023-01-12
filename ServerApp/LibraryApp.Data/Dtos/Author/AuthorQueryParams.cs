using LibraryApp.Common.Enums;
using LibraryApp.Data.Dtos.Base;

namespace LibraryApp.Data.Dtos.Author
{
    public class AuthorQueryParams: BaseQueryParams
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Nationality  { get; set; }
        public GenderEnum? Gender { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}
