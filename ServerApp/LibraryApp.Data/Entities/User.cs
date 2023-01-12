using LibraryApp.Common.Enums;
using LibraryApp.Data.Common;
using Microsoft.AspNetCore.Identity;
using System;

namespace LibraryApp.Data.Entities
{
    public class User : IdentityUser<int>, IBaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public GenderEnum Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActiveDate { get; set; }
        public string Introduction { get; set; }
        public string Hobbies { get; set; }
        public string ImageUrl { get; set; }

        public long? CreatedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? CreatedRoleId { get; set; }
        public long? UpdatedRoleId { get; set; }
        public bool IsDeleted { get ; set ; }

        #region 1-1 Relations (StudentAdress-Student)
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        #endregion

        #region 1-n Relations (User-FavouritesAuthor)
        public virtual FavouritesAuthor FavouritesAuthor { get; set; }
        #endregion
    }
}
