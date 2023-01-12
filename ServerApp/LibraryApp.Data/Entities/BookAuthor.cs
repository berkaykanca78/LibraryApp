using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Data.Common;

namespace LibraryApp.Data.Entities
{
    public class BookAuthor : AuditableEntity
    {
        public int? BookId { get; set; }
        public int? AuthorId { get; set; }

        #region 1-n Relations (Book-BookAuthor)
        public virtual ICollection<Book> Books { get; set; }
        #endregion       
        
        #region 1-n Relations (Author-BookAuthor)
        public virtual ICollection<Author> Authors { get; set; }
        #endregion
    }
}
