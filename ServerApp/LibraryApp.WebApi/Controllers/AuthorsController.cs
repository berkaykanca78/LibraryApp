using LibraryApp.Data.Dtos.Author;
using LibraryApp.Data.Dtos.BookAuthor;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class AuthorsController: ControllerBase
    {
        private readonly IAuthorService _authorService;
        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet(Name = "GetAuthors")]
        public async Task<ServiceResult> GetAuthors([FromQuery] AuthorQueryParams authorParams)
        {
            return await _authorService.GetAuthors(authorParams).ConfigureAwait(false);
        }

        [HttpGet(Name = "GetAuthorsForFillDropdown")]
        public async Task<ServiceResult> GetAuthorsForFillDropdown()
        {
            return await _authorService.GetAuthorsForFillDropdown().ConfigureAwait(false);
        }

        [HttpGet("{id}", Name = "GetAuthor")]
        public async Task<ServiceResult> GetAuthor(int id)
        {
            return await _authorService.GetAuthorById(id).ConfigureAwait(false);
        }

        [HttpPost(Name = "AddAuthor")]
        public async Task<ServiceResult> AddAuthor(BookAuthorRegisterDto model)
        {
            return await _authorService.AddAuthor(model).ConfigureAwait(false);
        }

        [HttpPut(Name = "UpdateAuthor")]
        public async Task<ServiceResult> UpdateAuthor(AuthorForDetailDto model)
        {
            return await _authorService.UpdateAuthor(model).ConfigureAwait(false);
        }

        [HttpDelete("{id}", Name = "DeleteAuthor")]
        public async Task<ServiceResult> DeleteAuthor(int id)
        {
            return await _authorService.DeleteAuthor(id).ConfigureAwait(false);
        }

        [HttpPut(Name = "AddOrUpdateAuthorToFavourite")]
        public async Task<ServiceResult> AddOrUpdateAuthorToFavourite(AuthorForListDto model)
        {
            return await _authorService.AddOrUpdateAuthorToFavourite(model).ConfigureAwait(false);
        }
    }
}
