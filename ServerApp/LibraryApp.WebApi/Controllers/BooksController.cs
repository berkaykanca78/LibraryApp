using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LibraryApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet(Name = "GetBooks")]
        public async Task<ServiceResult> GetBooks([FromQuery] BookQueryParams bookParams)
        {
            return await _bookService.GetBooks(bookParams).ConfigureAwait(false);
        }

        [HttpGet("{id}", Name = "GetBook")]
        public async Task<ServiceResult> GetBook(int id)
        {
            return await _bookService.GetBookById(id).ConfigureAwait(false);
        }

        [HttpPost(Name = "AddBook")]
        public async Task<ServiceResult> AddBook(BookForDetailDto model)
        {
            return await _bookService.AddBook(model).ConfigureAwait(false);
        }

        [HttpPut(Name = "UpdateBook")]
        public async Task<ServiceResult> UpdateBook(BookForDetailDto model)
        {
            return await _bookService.UpdateBook(model).ConfigureAwait(false);
        }

        [HttpPut("{id}", Name = "UpdateBookRating")]
        public async Task<ServiceResult> UpdateBookRating(int id, BookForListDto model)
        {
            return await _bookService.UpdateBookRating(id, model).ConfigureAwait(false);
        }

        [HttpDelete("{id}", Name = "DeleteBook")]
        public async Task<ServiceResult> DeleteBook(int id)
        {
            return await _bookService.DeleteBook(id).ConfigureAwait(false);
        }
    }
}
