using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Abstract
{
    public interface IUserRepository : IRepository<User>
    {
        Task<List<UserForListDto>> GetUsers(UserQueryParams userParams);
        Task<List<UserForDetailDto>> GetUsersForDashboard();
        Task<List<DoughnutChartDto>> GetCitiesForUsersInfo();
    }
}
