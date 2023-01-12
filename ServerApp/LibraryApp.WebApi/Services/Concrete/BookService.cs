using FluentValidation.Results;
using LibraryApp.Common.Extensions;
using LibraryApp.Common.Helpers;
using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.DataAccess.UnitOfWork.Abstract;
using LibraryApp.Entities.Enums;
using LibraryApp.Entities.Models;
using LibraryApp.Validation.FluentValidation;
using LibraryApp.WebApi.Services.Abstract;
using LibraryApp.WebToken.Jwt.Abstract;
using LibraryApp.WebToken.Jwt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Concrete
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IBookAuthorRepository bookAuthorRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtHelper jwtHelper;
        private readonly JwtDto currentUser;


        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork, IBookAuthorRepository bookAuthorRepository, IJwtHelper jwtHelper, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.unitOfWork = unitOfWork;
            this.bookAuthorRepository = bookAuthorRepository;
            this.jwtHelper = jwtHelper;
            this.currentUser = jwtHelper.GetJwtDto();
            this.authorRepository = authorRepository;
        }

        #region Queries
        public async Task<ServiceResult> GetBooks(BookQueryParams bookParams)
        {
            List<BookForListDto> books = await bookRepository.GetBooks(bookParams);
            return new ServiceResult { ResultType = ResultType.Success, Data = books.ToList(), Messages = null };
        }

        public async Task<ServiceResult> GetBookById(int id)
        {
            Book book = await bookRepository.GetByIdAsync(id);
            BookForDetailDto result = MapperHelper.MapFrom<BookForDetailDto>(book);
            BookAuthor bookAuthor = await bookAuthorRepository.GetBookAuthorIdByBookId(book.Id);
            Author author = await authorRepository.GetByIdAsync(bookAuthor.AuthorId.Value);
            result.AuthorName = author.FullName;
            result.PublisherName = book.Publisher.Name;
            result.CategoryName = book.Category.Name;
            result.MediaTypeName = result.MediaType.DisplayName();
            result.LanguageName = result.Language.DisplayName();
            return new ServiceResult { ResultType = ResultType.Success, Data = result, Messages = new List<string>() { } };
        }
        #endregion

        #region Commands
        public async Task<ServiceResult> AddBook(BookForDetailDto model)
        {
            ServiceResult serviceResult = new ServiceResult { ResultType = ResultType.Success, Data = null, Messages = null };

            Book book = MapperHelper.MapFrom<Book>(model);
            book.CreatedBy = currentUser.Id;
            book.CreatedDate = DateTime.Now;
            book.CreatedRoleId = currentUser.RoleId;
            book.BookAuthorId = null;

            (bool isValid, List<ValidationFailure> errors) = ValidateHelper.Validate(new BookValidator(), book);

            List<string> errorMessages = new List<string>();

            if (errors != null)
                errorMessages = errors.Select(x => x.ErrorMessage).ToList() ?? new List<string>() { };

            if (!isValid)
            {
                serviceResult.ResultType = ResultType.Error;
                foreach (var errorMessage in errorMessages)
                {
                    serviceResult.Messages.Add(errorMessage);
                }
                serviceResult.Data = null;
            }

            bookRepository.AddOrUpdate(book);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);

            BookAuthor bookAuthor = new()
            {
                BookId = book.Id,
                AuthorId = model.AuthorId,
                CreatedBy = currentUser.Id,
                CreatedDate = DateTime.Now,
                CreatedRoleId = currentUser.RoleId,
            };
            bookAuthorRepository.AddOrUpdate(bookAuthor);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = book };
        }
        public async Task<ServiceResult> UpdateBook(BookForDetailDto model)
        {
            ServiceResult serviceResult = new ServiceResult { ResultType = ResultType.Success, Data = null, Messages = null };

            Book book = MapperHelper.MapFrom<Book>(model);

            (bool isValid, List<ValidationFailure> errors) = ValidateHelper.Validate(new BookValidator(), book);

            List<string> errorMessages = new List<string>();

            if (errors != null)
                errorMessages = errors.Select(x => x.ErrorMessage).ToList() ?? new List<string>() { };

            if (!isValid)
            {
                serviceResult.ResultType = ResultType.Error;
                foreach (var errorMessage in errorMessages)
                {
                    serviceResult.Messages.Add(errorMessage);
                }
                serviceResult.Data = null;
            }
            book.BookAuthorId = null;
            book.UpdatedDate= DateTime.Now;
            book.UpdatedBy = currentUser.Id;
            book.UpdatedRoleId = currentUser.RoleId;
            await bookRepository.UpdateAsync(book, book.Id);
            try
            {
                await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            }
            catch (Exception ex)
            {

                throw;
            }
            return new ServiceResult { ResultType = ResultType.Success, Data = book };
        }

        public async Task<ServiceResult> UpdateBookRating(int id, BookForListDto model)
        {
            Book book = await bookRepository.GetByIdAsync(id);
            book.Rating = model.Rating;
            book.UpdatedDate = DateTime.Now;
            book.UpdatedBy = currentUser.Id;
            book.UpdatedRoleId = currentUser.RoleId;
            await unitOfWork.GetRepository<Book>().UpdateAsync(book, id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = book };
        }

        public async Task<ServiceResult> DeleteBook(int id)
        {
            BookAuthor booksAuthor = await bookAuthorRepository.GetBookAuthorIdByBookId(id);
            booksAuthor.UpdatedDate = DateTime.Now;
            booksAuthor.UpdatedBy = currentUser.Id;
            booksAuthor.UpdatedRoleId = currentUser.RoleId;
            unitOfWork.GetRepository<BookAuthor>().Delete(booksAuthor.Id);
            unitOfWork.GetRepository<Book>().Delete(id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success };
        }
        #endregion

    }
}
