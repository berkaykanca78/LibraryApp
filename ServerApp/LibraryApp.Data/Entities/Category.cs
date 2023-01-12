using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryApp.Data.Common;

namespace LibraryApp.Data.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; }

        #region 1-n Relations (Book-Category)
        public virtual ICollection<Book> Books { get; set; } 
        #endregion
    }
}
