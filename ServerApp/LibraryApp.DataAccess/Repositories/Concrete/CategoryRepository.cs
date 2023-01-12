using AutoMapper;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private new readonly DbSet<Category> _dbSet;

        public CategoryRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _context ??= context;
            _mapper = mapper;
            _dbSet = context.Set<Category>();
        }

        public async Task<List<DropdownDto>> GetCategoriesForFillDropdown()
        {
            return await _dbSet.Select(x => new DropdownDto() { Value = x.Id, Label = x.Name }).AsNoTracking().ToListAsync();
        }

        public async Task<List<PieChartDto>> GetCategoriesForBooksInfo()
        {
            List<string> hexColorList = new();
            List<PieChartDto> pieChartList = new();
            using (var labels = _dbSet.Select(x => new { x.Id, x.Name }).AsNoTracking().ToListAsync())
            {
                for (int index = 0; index < labels.Result.Count; index++)
                {
                    string hexColor = String.Format("#{0:X6}", new Random().Next(0x1000000)); 
                    int count = await _context.Books.CountAsync(i => i.CategoryId == labels.Result[index].Id);
                    pieChartList.Add(new PieChartDto() { HexCode = hexColor, Label = labels.Result[index].Name, Count = count });
                }
            }
            return pieChartList;
        }
    }
}
