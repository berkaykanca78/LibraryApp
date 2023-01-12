using LibraryApp.Data.Context;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class FavouriteAuthorRepository : Repository<FavouritesAuthor>, IFavouriteAuthorRepository
    {
        private readonly LibraryDbContext _context;
        private new readonly DbSet<FavouritesAuthor> _dbSet;
        public FavouriteAuthorRepository(LibraryDbContext context) : base(context)
        {
           _context= context;
           _dbSet = context.Set<FavouritesAuthor>();
        }

        public async Task<FavouritesAuthor> GetByAuthorIdAndUserId(int authorId, int userId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.AuthorId == authorId && x.IsDeleted == false);
        }
    }
}
