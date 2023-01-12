using LibraryApp.Common.Enums;
using LibraryApp.Data.Dtos.Base;

namespace LibraryApp.Data.Dtos.User
{
    public class UserQueryParams: BaseQueryParams
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public GenderEnum? Gender { get; set; }
        public string City { get; set; }
        public int? MinAge { get; set; }
        public int? MaxAge { get; set; }
    }
}
