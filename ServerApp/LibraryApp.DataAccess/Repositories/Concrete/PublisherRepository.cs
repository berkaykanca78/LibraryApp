using AutoMapper;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private new readonly DbSet<Publisher> _dbSet;

        public PublisherRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _context ??= context;
            _mapper = mapper;
            _dbSet = context.Set<Publisher>();
        }

        public async Task<List<DropdownDto>> GetPublishersForFillDropdown()
        {
            return await _dbSet.Select(x => new DropdownDto() { Value = x.Id, Label = x.Name }).AsNoTracking().ToListAsync();
        }
    }
}
