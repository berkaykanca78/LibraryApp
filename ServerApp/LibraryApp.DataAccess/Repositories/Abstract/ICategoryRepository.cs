using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<List<DropdownDto>> GetCategoriesForFillDropdown();
        Task<List<PieChartDto>> GetCategoriesForBooksInfo();
    }
}
