using AutoMapper;
using LibraryApp.Common.Extensions;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Book;
using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Specification.QuerySpecification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private new readonly DbSet<Book> _dbSet;

        public BookRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _context ??= context;
            _mapper = mapper;
            _dbSet = context.Set<Book>();
        }

        public async Task<List<BookForListDto>> GetBooks(BookQueryParams bookParams)
        {
            IQueryable<Book> bookContext = _dbSet;
            
            bookContext = bookContext.Specify(new BookQueryTitleSpec(bookParams.Title))
                                     .Specify(new BookQueryCategorySpec(bookParams.Category))
                                     .Specify(new BookQueryPublisherSpec(bookParams.Publisher))
                                     .Specify(new BookQueryAuthorSpec(bookParams.Author))
                                     .Specify(new BookQueryMediaTypeSpec(bookParams.MediaType))
                                     .Specify(new BookQueryLanguageSpec(bookParams.Language))
                                     .Specify(new BookQueryMinRatingSpec(bookParams.MinRating))
                                     .Specify(new BookQueryMaxRatingSpec(bookParams.MaxRating))
                                     .Specify(new BookQueryMinPriceSpec(bookParams.MinPrice))
                                     .Specify(new BookQueryMaxPriceSpec(bookParams.MaxPrice))
                                     .Specify(new BookQueryOrderBySpec(bookParams.OrderBy, bookParams.Ordering))
                                     .Cast<Book>();

            List<BookForListDto> books = await bookContext.Select(x => _mapper.Map<BookForListDto>(x))
                                          .ToListAsync()
                                          .ConfigureAwait(false);
            
            foreach (var book in books.Select((value, index) => new { index, value }))
            {
                BookAuthor? bookAuthor = _context.BookAuthors.FirstOrDefault(x => x.BookId == book.value.Id);
                book.value.No = book.index + 1;
                book.value.AuthorId = bookAuthor.AuthorId.GetValueOrDefault();
                book.value.AuthorName = _context.Authors.FirstOrDefault(x => x.Id == bookAuthor.AuthorId).FullName;
                book.value.PublisherId = _context.Publishers.FirstOrDefault(x => x.Id == book.value.PublisherId).Id;
                book.value.CategoryId = _context.Categories.FirstOrDefault(x => x.Id == book.value.CategoryId).Id;
            }
            
            return books;
        }

        public async Task<List<BookForDetailDto>> GetBooksByAuthorId(int authorId)
        {
            return await _dbSet.Where(x => x.BookAuthor.AuthorId == authorId)
                               .Select(x => _mapper.Map<BookForDetailDto>(x))
                               .ToListAsync()
                               .ConfigureAwait(false);
        }

        public async Task<List<BarChartDto>> GetBooksForAuthorsInfo()
        {
            List<string> hexColorList = new();
            List<BarChartDto> barChartList = new();
            using (var labels = _context.BookAuthors.Select(x => new { AuthorUniqueId = x.AuthorId }).Distinct().AsNoTracking().ToListAsync())
            {
                for (int index = 0; index < labels.Result.Count; index++)
                {
                    var author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == labels.Result[index].AuthorUniqueId);
                    string hexColor = String.Format("#{0:X6}", new Random().Next(0x1000000));
                    string borderHexCode = String.Format("#{0:X6}", new Random().Next(0x1000000));
                    int count = await _context.BookAuthors.CountAsync(i => i.AuthorId == labels.Result[index].AuthorUniqueId);
                    barChartList.Add(new BarChartDto() { HexCode = hexColor, Label = author.FullName, Count = count, BorderHexCode = borderHexCode });
                }
            }
            return barChartList;
        }
    }
}
