using LibraryApp.Data.Dtos.Author;
using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<List<DropdownDto>> GetAuthorsForFillDropdown();
        Task<List<AuthorForListDto>> GetAuthors(AuthorQueryParams authorParams);
    }
}
