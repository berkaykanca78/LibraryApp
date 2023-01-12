using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<BookForListDto>> GetBooks(BookQueryParams bookParams);
        Task<List<BookForDetailDto>> GetBooksByAuthorId(int authorId);
        Task<List<BarChartDto>> GetBooksForAuthorsInfo();
    }
}
