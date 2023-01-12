using LibraryApp.Common.Helpers;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Data.Entities;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IUserService _userService;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IUserService userService, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _roleManager = roleManager;
        }

        [HttpPost(Name = "Register")]
        public async Task<IActionResult> Register(UserForRegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ServiceResult userResult = await _userService.GetUserForRegister(model).ConfigureAwait(false);
            User user = userResult.Data as User;

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return StatusCode(201);

            return BadRequest(result.Errors);
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(UserForLoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            User user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
                return BadRequest(new { message = "Username is Incorrect" });

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

            if (result.Succeeded)
            {
                return Ok(new
                {
                    token = TokenHelper.GenerateJwtToken(user, _userManager,_roleManager)
                });
            }

            return Unauthorized();
        }
    }
}
