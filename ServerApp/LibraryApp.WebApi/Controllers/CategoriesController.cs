using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using LibraryApp.WebApi.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet(Name = "GetCategoriesForFillDropdown")]
        public async Task<ServiceResult> GetCategoriesForFillDropdown()
        {
            return await _categoryService.GetCategoriesForFillDropdown().ConfigureAwait(false);
        }
    }
}
