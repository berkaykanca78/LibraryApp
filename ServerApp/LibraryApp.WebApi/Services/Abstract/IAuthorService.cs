using LibraryApp.Data.Dtos.Author;
using LibraryApp.Data.Dtos.BookAuthor;
using LibraryApp.Entities.Models;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Abstract
{
    public interface IAuthorService
    {
        Task<ServiceResult> GetAuthors(AuthorQueryParams authorParams);
        Task<ServiceResult> GetAuthorsForFillDropdown();
        Task<ServiceResult> GetAuthorById(int id);
        Task<ServiceResult> AddAuthor(BookAuthorRegisterDto model);
        Task<ServiceResult> UpdateAuthor(AuthorForDetailDto model);
        Task<ServiceResult> AddOrUpdateAuthorToFavourite(AuthorForListDto model);
        Task<ServiceResult> DeleteAuthor(int id);
    }
}
