using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IPublisherRepository : IRepository<Publisher>
    {
        Task<List<DropdownDto>> GetPublishersForFillDropdown();
    }
}
