using System;
using LibraryApp.Common.Enums;
using LibraryApp.Data.Common;

namespace LibraryApp.Data.Entities
{
    public class Author: AuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public GenderEnum Gender { get; set; }
        public string Introduction { get; set; }

        #region 1-n Relations (Author-BookAuthor)
        public int? BookAuthorId { get; set; }
        public virtual BookAuthor BookAuthor { get; set; }
        #endregion

        #region 1-n Relations (Author-FavouritesAuthor)
        public virtual FavouritesAuthor FavouritesAuthor { get; set; }
        #endregion
    }
}
