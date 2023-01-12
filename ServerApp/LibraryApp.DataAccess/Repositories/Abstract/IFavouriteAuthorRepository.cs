using LibraryApp.Data.Entities;
using LibraryApp.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IFavouriteAuthorRepository : IRepository<FavouritesAuthor>
    {
        Task<FavouritesAuthor> GetByAuthorIdAndUserId(int authorId, int userId);
    }
}
