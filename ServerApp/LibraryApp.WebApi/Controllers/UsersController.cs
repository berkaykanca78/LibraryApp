using LibraryApp.Common.Attributes;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Data.Entities;
using LibraryApp.Entities.Enums;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Api.Controllers
{
    [ServiceFilter(typeof(LastActiveActionFilter))]
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly UserManager<User> _userManager;

        public UsersController(IUserService userService, UserManager<User> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet(Name = "GetUsers")]
        public async Task<ServiceResult> GetUsers([FromQuery] UserQueryParams userParams)
        {
            return await _userService.GetUsers(userParams).ConfigureAwait(false);
        }

        [HttpGet(Name = "GetUsersForDashboard")]
        public async Task<ServiceResult> GetUsersForDashboard()
        {
            return await _userService.GetUsersForDashboard().ConfigureAwait(false);
        }

        [HttpGet("{id}", Name = "GetUser")]
        public async Task<ServiceResult> GetUser(int id)
        {
            return await _userService.GetUserById(id).ConfigureAwait(false);
        }

        [HttpPost(Name = "AddUser")]
        public async Task<ServiceResult> AddUser(UserForRegisterDto model)
        {
            return await _userService.AddUser(model).ConfigureAwait(false);
        }

        [HttpPut(Name = "UpdateUser")]
        public async Task<ServiceResult> UpdateUser(UserForDetailDto model)
        {
            ServiceResult res =  await _userService.UpdateUser(model).ConfigureAwait(false);
            User user = res.Data as User;

            IdentityResult result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return new ServiceResult() { Data = result, Messages = null, ResultType = ResultType.Success };

            return new ServiceResult() { Data = "", Messages = new List<string>() { "Error while updating profile" }, ResultType = ResultType.Error };
        }

        [HttpDelete("{id}", Name = "DeleteUser")]
        public async Task<ServiceResult> DeleteUser(int id)
        {
            return await _userService.DeleteUser(id).ConfigureAwait(false);
        }



    }
}
