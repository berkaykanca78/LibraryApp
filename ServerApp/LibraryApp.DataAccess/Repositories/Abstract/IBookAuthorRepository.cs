using LibraryApp.Data.Entities;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IBookAuthorRepository : IRepository<BookAuthor>
    {
        Task<BookAuthor> GetBookAuthorIdByBookId(int bookId);
    }
}
