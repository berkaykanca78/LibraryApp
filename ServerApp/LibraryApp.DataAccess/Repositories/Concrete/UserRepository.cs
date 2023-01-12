using AutoMapper;
using LibraryApp.Common.Extensions;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Specification.QuerySpecification;
using LibraryApp.WebToken.Jwt.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.DataAccess.Repositories.Concrete
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;
        private new readonly DbSet<User> _dbSet;
        private readonly UserManager<User> _userManager;
        private readonly IJwtHelper _jwtHelper;

        public UserRepository(LibraryDbContext context, IMapper mapper, UserManager<User> userManager, IJwtHelper jwtHelper) : base(context)
        {
            _context ??= context;
            _mapper = mapper;
            _dbSet = context.Set<User>();
            _userManager = userManager;
            _jwtHelper = jwtHelper;
        }

        public async Task<List<UserForListDto>> GetUsers(UserQueryParams userParams)
        {
            IQueryable<User> userContext = _dbSet.AsNoTracking();

            userContext = userContext.Specify(new UserQueryNameSpec(userParams.Name))
                                     .Specify(new UserQuerySurnameSpec(userParams.Surname))
                                     .Specify(new UserQueryUserNameSpec(userParams.UserName))
                                     .Specify(new UserQueryCitySpec(userParams.City))
                                     .Specify(new UserQueryMinMaxAgeSpec(userParams.MinAge, userParams.MaxAge))
                                     .Specify(new UserQueryGenderSpec(userParams.Gender))
                                     .Specify(new UserQueryOrderBySpec(userParams.OrderBy, userParams.Ordering))
                                     .Cast<User>();

            List<UserForListDto> users = await userContext.Include(i => i.Address)
                                                          .Select(x => _mapper.Map<UserForListDto>(x))
                                                          .AsNoTracking()
                                                          .ToListAsync()
                                                          .ConfigureAwait(false);

            foreach (var user in users.Select((value, index) => new { index, value }))
            {
                user.value.No = user.index + 1;
            }

            return users;
        }        
        
        public async Task<List<UserForDetailDto>> GetUsersForDashboard()
        {
            int currentUserId = _jwtHelper.GetJwtDto().Id;

            IQueryable<User> userContext = _dbSet.Include(i => i.Address).AsNoTracking();
            IList<User> usersWithoutAdmin = await _userManager.GetUsersInRoleAsync("User");
           
            IEnumerable<UserForDetailDto> usersWithoutAdminQueryable = usersWithoutAdmin.Where(x => x.Id != currentUserId && x.IsDeleted == false).Select(x => _mapper.Map<UserForDetailDto>(x));
            List<UserForDetailDto> users = usersWithoutAdminQueryable.ToList();

            foreach (var user in users.Select((value, index) => new { index, value }))
            {
                IdentityUserRole<int> role = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.value.Id);
                Role roleTable = await _context.Roles.FirstOrDefaultAsync(x => x.Id == role.RoleId);
                string roleName = roleTable.Name;
                user.value.RoleName = roleName;
                user.value.No = user.index + 1;
            }
            return users;
        }

        public async Task<List<DoughnutChartDto>> GetCitiesForUsersInfo()
        {
            List<string> hexColorList = new();
            List<DoughnutChartDto> doughnutChartDto = new();
            using (var labels = _dbSet.Select(x => new { CityName = x.Address.City }).AsNoTracking().ToListAsync())
            {
                for (int index = 0; index < labels.Result.Count; index++)
                {
                    string hexColor = String.Format("#{0:X6}", new Random().Next(0x1000000)); 
                    string hoverHexCode = String.Format("#{0:X6}", new Random().Next(0x1000000)); 
                    int count = await _context.Users.CountAsync(i => i.Address.City == labels.Result[index].CityName);
                    doughnutChartDto.Add(new DoughnutChartDto() { HexCode = hexColor, Label = labels.Result[index].CityName, Count = count, HoverHexCode = hoverHexCode  });
                }
            }
            return doughnutChartDto;
        }
    }

}
