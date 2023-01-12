using AutoMapper;
using LibraryApp.Common.Extensions;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Author;
using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Specification.QuerySpecification;
using LibraryApp.WebToken.Jwt.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private new readonly DbSet<Author> _dbSet;
        private IJwtHelper _jwtHelper;

        public AuthorRepository(LibraryDbContext context, IMapper mapper, IJwtHelper jwtHelper) : base(context)
        {
            _context ??= context;
            _mapper = mapper;
            _dbSet = context.Set<Author>();
            _jwtHelper = jwtHelper;
        }

        public async Task<List<DropdownDto>> GetAuthorsForFillDropdown()
        {
            return await _dbSet.Select(x => new DropdownDto() { Value = x.Id, Label = x.FullName }).AsNoTracking().ToListAsync();
        }

        public async Task<List<AuthorForListDto>> GetAuthors(AuthorQueryParams authorParams)
        {
            IQueryable<Author> authorContext = _dbSet;

            authorContext = authorContext.Specify(new AuthorQueryNameSpec(authorParams.Name))
                                     .Specify(new AuthorQuerySurnameSpec(authorParams.Surname))
                                     .Specify(new AuthorQueryNationalitySpec(authorParams.Nationality))
                                     .Specify(new AuthorQueryGenderSpec(authorParams.Gender))
                                     .Specify(new AuthorQueryMinMaxAgeSpec(authorParams.MinAge, authorParams.MaxAge))
                                     .Specify(new AuthorQueryOrderBySpec(authorParams.OrderBy, authorParams.Ordering))
                                     .Cast<Author>();

            List<AuthorForListDto> authors = await authorContext.Select(x => _mapper.Map<AuthorForListDto>(x))
                                                                .ToListAsync()
                                                                .ConfigureAwait(false);

            foreach (var author in authors.Select((value, index) => new { index, value }))
            {
                author.value.No = author.index + 1;
                author.value.IsFavourite = _context.Set<FavouritesAuthor>().Any(i => i.UserId == _jwtHelper.GetJwtDto().Id && i.AuthorId == author.value.ID && i.IsDeleted == false);
            }

            return authors;
        }
    }
}
