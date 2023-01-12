using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Common
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        bool IsDeleted { get; set; }
    }
}
