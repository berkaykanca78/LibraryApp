using LibraryApp.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Entities
{
    public class Address:AuditableEntity
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string FullAddress { get; set; }

        #region 1-1 Relations (Adress-User)
        public virtual User User { get; set; }
        #endregion
    }
}
