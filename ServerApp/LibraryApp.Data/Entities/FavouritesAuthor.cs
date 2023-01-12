using LibraryApp.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Entities
{
    public class FavouritesAuthor : AuditableEntity
    {
        public int UserId { get; set; }
        public int AuthorId { get; set; }

        #region 1-n Relations (Users-FavouritesAuthor)
        public virtual ICollection<User> Users { get; set; }
        #endregion

        #region 1-n Relations (Author-FavouritesAuthor)
        public virtual ICollection<Author> Authors { get; set; }
        #endregion
    }
}
