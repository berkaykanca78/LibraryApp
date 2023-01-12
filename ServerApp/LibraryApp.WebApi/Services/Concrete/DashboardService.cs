using LibraryApp.Data.Dtos.Charts;
using LibraryApp.Data.Dtos.Dashboard;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Entities.Enums;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Concrete
{
    public class DashboardService: IDashboardService
    {
        private readonly IUserRepository userRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookRepository bookRepository;
        private readonly ICategoryRepository categoryRepository;

        public DashboardService(IUserRepository userRepository, IAuthorRepository authorRepository, IBookRepository bookRepository, ICategoryRepository categoryRepository)
        {
            this.userRepository = userRepository;
            this.authorRepository = authorRepository;
            this.bookRepository = bookRepository;
            this.categoryRepository = categoryRepository;
        }

        #region Queries
        public async Task<ServiceResult> GetCountsForDashboard()
        {
            int usersCount = await userRepository.CountAsync();
            int authorsCount = await authorRepository.CountAsync();
            int booksCount = await bookRepository.CountAsync();
            CountDto countDto = new()
            {
                UsersCount = usersCount,
                AuthorsCount = authorsCount,
                BooksCount = booksCount,
            };
            return new ServiceResult { ResultType = ResultType.Success, Data = countDto, Messages = null };
        }

        public async Task<ServiceResult> GetCategoriesForBooksInfo()
        {
            List<PieChartDto> infoList = await categoryRepository.GetCategoriesForBooksInfo();
            return new ServiceResult { ResultType = ResultType.Success, Data = infoList, Messages = null };
        }

        public async Task<ServiceResult> GetCitiesForUsersInfo()
        {
            List<DoughnutChartDto> infoList = await userRepository.GetCitiesForUsersInfo();
            return new ServiceResult { ResultType = ResultType.Success, Data = infoList, Messages = null };
        }

        public async Task<ServiceResult> GetBooksForAuthorsInfo()
        {
            List<BarChartDto> infoList = await bookRepository.GetBooksForAuthorsInfo();
            return new ServiceResult { ResultType = ResultType.Success, Data = infoList, Messages = null };
        }
        #endregion

    }
}
