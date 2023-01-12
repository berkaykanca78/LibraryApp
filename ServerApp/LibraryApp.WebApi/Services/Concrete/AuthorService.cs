using AutoMapper;
using FluentValidation.Results;
using LibraryApp.Common.Helpers;
using LibraryApp.Data.Dtos.Author;
using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Dtos.BookAuthor;
using LibraryApp.Data.Dtos.Dropdown;
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
    public class AuthorService: IAuthorService
    {
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly IFavouriteAuthorRepository favouriteAuthorRepository;
        private readonly IBookAuthorRepository bookAuthorRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtHelper jwtHelper;
        private readonly IMapper mapper;
        private readonly JwtDto currentUser;

        public AuthorService(IAuthorRepository authorRepository, IUnitOfWork unitOfWork, IJwtHelper jwtHelper, IFavouriteAuthorRepository favouriteAuthorRepository, IBookRepository bookRepository, IMapper mapper, IBookAuthorRepository bookAuthorRepository)
        {
            this.authorRepository = authorRepository;
            this.unitOfWork = unitOfWork;
            this.jwtHelper = jwtHelper;
            this.favouriteAuthorRepository = favouriteAuthorRepository;
            this.bookRepository = bookRepository;
            this.mapper = mapper;
            this.bookAuthorRepository = bookAuthorRepository;
            this.currentUser = jwtHelper.GetJwtDto();
        }

        #region Queries
        public async Task<ServiceResult> GetAuthors(AuthorQueryParams authorParams)
        {
            List<AuthorForListDto> authors = await authorRepository.GetAuthors(authorParams).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = authors.ToList(), Messages = null };
        }

        public async Task<ServiceResult> GetAuthorsForFillDropdown()
        {
            List<DropdownDto> authors = await authorRepository.GetAuthorsForFillDropdown().ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = authors, Messages = null };
        }

        public async Task<ServiceResult> GetAuthorById(int id)
        {
            Author author = await authorRepository.GetByIdAsync(id).ConfigureAwait(false);
            AuthorForDetailDto result = mapper.Map<AuthorForDetailDto>(author);
            result.Books = await bookRepository.GetBooksByAuthorId(result.ID).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = result, Messages = new List<string>() { } };
        }
        #endregion

        #region Commands
        public async Task<ServiceResult> AddAuthor(BookAuthorRegisterDto model)
        {
            ServiceResult serviceResult = new ServiceResult { ResultType = ResultType.Success, Data = null, Messages = null };

            #region Book
            Book book = new()
            {
                CategoryId = model.CategoryId,
                CreatedBy = currentUser.Id,
                CreatedDate = DateTime.Now,
                CreatedRoleId = currentUser.RoleId,
                ImageUrl = model.BookImageUrl,
                Introduction = model.BookIntroduction,
                Language = model.Language,
                MediaType = model.MediaType,
                Price = model.Price,
                PublisherId = model.PublisherId,
                PublishedDate = model.PublishedDate,
                Rating = 5,
                Title = model.Title,
                TotalPages = model.TotalPages,
            };

            (bool isValidBook, List<ValidationFailure> errorsForBook) = ValidateHelper.Validate(new BookValidator(), book);

            List<string> errorMessagesForBook = new List<string>();

            if (errorsForBook != null)
                errorMessagesForBook = errorsForBook.Select(x => x.ErrorMessage).ToList() ?? new List<string>() { };

            if (!isValidBook)
            {
                serviceResult.ResultType = ResultType.Error;
                foreach (var errorMessageForBook in errorMessagesForBook)
                {
                    serviceResult.Messages.Add(errorMessageForBook);
                }
                serviceResult.Data = null;
            }
            unitOfWork.GetRepository<Book>().AddOrUpdate(book);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            #endregion

            #region Author
            Author author = new()
            {
                City = model.City,
                Country = model.Country,
                DateOfBirth = model.DateOfBirth,
                CreatedBy = currentUser.Id,
                CreatedRoleId = currentUser.RoleId,
                CreatedDate = DateTime.Now,
                FirstName = model.FirstName,
                Gender = model.Gender,
                ImageUrl = model.AuthorImageUrl,
                Introduction = model.AuthorIntroduction,
                LastName = model.LastName,
            };

            (bool isValidAuthor, List<ValidationFailure> errorsForAuthor) = ValidateHelper.Validate(new AuthorValidator(), author);

            List<string> errorMessagesForAuthor = new List<string>();

            if (errorsForAuthor != null)
                errorMessagesForAuthor = errorsForAuthor.Select(x => x.ErrorMessage).ToList() ?? new List<string>() { };

            if (!isValidAuthor)
            {
                serviceResult.ResultType = ResultType.Error;
                foreach (var errorMessageForAuthor in errorMessagesForAuthor)
                {
                    serviceResult.Messages.Add(errorMessageForAuthor);
                }
                serviceResult.Data = null;
            }
            unitOfWork.GetRepository<Author>().AddOrUpdate(author);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            #endregion

            #region Ara Tablo
            BookAuthor bookAuthor = new()
            {
                AuthorId = author.Id,
                BookId = book.Id,
                CreatedBy = currentUser.Id,
                CreatedDate = DateTime.Now,
                CreatedRoleId = currentUser.RoleId,
            };
            unitOfWork.GetRepository<BookAuthor>().AddOrUpdate(bookAuthor);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            #endregion
            return new ServiceResult { ResultType = ResultType.Success, Data = author };
        }
        public async Task<ServiceResult> UpdateAuthor(AuthorForDetailDto model)
        {
            ServiceResult serviceResult = new ServiceResult { ResultType = ResultType.Success, Data = null, Messages = null };

            Author author = MapperHelper.MapFrom<Author>(model);

            (bool isValid, List<ValidationFailure> errors) = ValidateHelper.Validate(new AuthorValidator(), author);

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
            author.UpdatedDate = DateTime.Now;
            author.UpdatedBy = currentUser.Id;
            author.UpdatedRoleId = currentUser.RoleId;
            await authorRepository.UpdateAsync(author, author.Id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = author };
        }

        public async Task<ServiceResult> AddOrUpdateAuthorToFavourite(AuthorForListDto model)
        {
            FavouritesAuthor favouritesAuthor;
            int authorId = model.ID;
            int userId = jwtHelper.GetJwtDto().Id;
            FavouritesAuthor checkExists = await favouriteAuthorRepository.GetByAuthorIdAndUserId(authorId, userId);
            if (checkExists != null)
            {
                checkExists.UpdatedDate = DateTime.Now;
                checkExists.UpdatedBy = currentUser.Id;
                checkExists.UpdatedRoleId = currentUser.RoleId;
                unitOfWork.GetRepository<FavouritesAuthor>().Delete(checkExists.Id);
                await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
                favouritesAuthor = null;
            }
            else
            {
                favouritesAuthor = new FavouritesAuthor()
                {
                    AuthorId = model.ID,
                    UserId = jwtHelper.GetJwtDto().Id,
                    CreatedBy= currentUser.Id,
                    CreatedDate = DateTime.Now,
                    CreatedRoleId= currentUser.RoleId,
                };
                await unitOfWork.GetRepository<FavouritesAuthor>().AddAsync(favouritesAuthor).ConfigureAwait(false);
                await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            }
            return new ServiceResult { ResultType = ResultType.Success, Messages = null, Data = favouritesAuthor };
        }

        public async Task<ServiceResult> DeleteAuthor(int id)
        {
            List<BookForDetailDto> booksFromAuthor = await bookRepository.GetBooksByAuthorId(id);
            foreach (BookForDetailDto book in booksFromAuthor)
            {
                BookAuthor bookAuthors = await bookAuthorRepository.GetByIdAsync(book.BookAuthorId);
                bookAuthors.UpdatedBy = currentUser.Id;
                bookAuthors.UpdatedDate = DateTime.Now;
                bookAuthors.UpdatedRoleId = currentUser.RoleId;
                unitOfWork.GetRepository<BookAuthor>().Delete(bookAuthors.Id);
                await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
                book.UpdatedBy = currentUser.Id;
                book.UpdatedDate = DateTime.Now;
                book.UpdatedRoleId = currentUser.RoleId;
                unitOfWork.GetRepository<Book>().Delete(book.ID);
                await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            }
            Author author = await authorRepository.GetByIdAsync(id);
            author.UpdatedBy = currentUser.Id;
            author.UpdatedDate = DateTime.Now;
            author.UpdatedRoleId = currentUser.RoleId;
            unitOfWork.GetRepository<Author>().Delete(id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success };
        }
        #endregion

    }
}
