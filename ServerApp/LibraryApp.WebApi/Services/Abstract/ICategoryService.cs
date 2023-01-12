using LibraryApp.Entities.Models;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Abstract
{
    public interface ICategoryService
    {
        Task<ServiceResult> GetCategoriesForFillDropdown();
    }
}
