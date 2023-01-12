using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet(Name = "GetCountsForDashboard")]
        public async Task<ServiceResult> GetCountsForDashboard()
        {
            return await _dashboardService.GetCountsForDashboard().ConfigureAwait(false);
        }

        [HttpGet(Name = "GetCategoriesForBooksInfo")]
        public async Task<ServiceResult> GetCategoriesForBooksInfo()
        {
            return await _dashboardService.GetCategoriesForBooksInfo().ConfigureAwait(false);
        }

        [HttpGet(Name = "GetCitiesForUsersInfo")]
        public async Task<ServiceResult> GetCitiesForUsersInfo()
        {
            return await _dashboardService.GetCitiesForUsersInfo().ConfigureAwait(false);
        }

        [HttpGet(Name = "GetBooksForAuthorsInfo")]
        public async Task<ServiceResult> GetBooksForAuthorsInfo()
        {
            return await _dashboardService.GetBooksForAuthorsInfo().ConfigureAwait(false);
        }
    }
}
