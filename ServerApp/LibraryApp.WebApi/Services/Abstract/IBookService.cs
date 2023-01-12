using LibraryApp.Data.Dtos.Book;
using LibraryApp.Entities.Models;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Abstract
{
    public interface IBookService
    {
        Task<ServiceResult> GetBooks(BookQueryParams bookParams);
        Task<ServiceResult> GetBookById(int id);
        Task<ServiceResult> AddBook(BookForDetailDto model);
        Task<ServiceResult> UpdateBook(BookForDetailDto model);
        Task<ServiceResult> UpdateBookRating(int id, BookForListDto model);
        Task<ServiceResult> DeleteBook(int id);
    }
}
