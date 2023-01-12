using LibraryApp.Data.Dtos.User;
using LibraryApp.Entities.Models;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Abstract
{
    public interface IUserService
    {
        Task<ServiceResult> GetUsers(UserQueryParams userParams);
        Task<ServiceResult> GetUsersForDashboard();
        Task<ServiceResult> GetUserById(int id);
        Task<ServiceResult> AddUser(UserForRegisterDto model);
        Task<ServiceResult> UpdateUser(UserForDetailDto model);
        Task<ServiceResult> DeleteUser(int id);
        Task<ServiceResult> GetUserForRegister(UserForRegisterDto model);
    }
}
