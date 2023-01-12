using LibraryApp.Data.Context;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class AdressRepository : Repository<Address>, IAddressRepository
    {
        public AdressRepository(LibraryDbContext context) : base(context)
        {
        }
    }
}
