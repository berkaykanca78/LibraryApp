using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace LibraryApp.Common.Attributes
{
    public class LastActiveActionFilter : IAsyncActionFilter
    {
        private readonly UserManager<User> _userManager;

        public LastActiveActionFilter(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ActionExecutedContext resultContext = await next();

            int id = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            IUserRepository? repository = (IUserRepository)resultContext.HttpContext.RequestServices.GetService(typeof(IUserRepository));

            User user = await repository.GetByIdAsync(id);
            user.LastActiveDate = DateTime.Now;

            IdentityResult result = await _userManager.UpdateAsync(user);
        }
    }
}
