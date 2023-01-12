using LibraryApp.Entities.Models;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Abstract
{
    public interface IDashboardService
    {
        Task<ServiceResult> GetCountsForDashboard();
        Task<ServiceResult> GetCategoriesForBooksInfo();
        Task<ServiceResult> GetCitiesForUsersInfo();
        Task<ServiceResult> GetBooksForAuthorsInfo();
    }
}
