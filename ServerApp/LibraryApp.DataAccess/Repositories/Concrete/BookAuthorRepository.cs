using AutoMapper;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.WebToken.Jwt.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class BookAuthorRepository : Repository<BookAuthor>, IBookAuthorRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private new readonly DbSet<BookAuthor> _dbSet;
        private IJwtHelper _jwtHelper;

        public BookAuthorRepository(LibraryDbContext context, IMapper mapper, IJwtHelper jwtHelper) : base(context)
        {
            _context ??= context;
            _mapper = mapper;
            _dbSet = context.Set<BookAuthor>();
            _jwtHelper = jwtHelper;
        }

        

        public async Task<BookAuthor> GetBookAuthorIdByBookId(int bookId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.BookId == bookId);
        }
    }
}
