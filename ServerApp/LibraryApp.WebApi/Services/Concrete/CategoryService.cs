using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.Entities.Enums;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        #region Queries
        public async Task<ServiceResult> GetCategoriesForFillDropdown()
        {
            List<DropdownDto> categories = await categoryRepository.GetCategoriesForFillDropdown();
            return new ServiceResult { ResultType = ResultType.Success, Data = categories, Messages = null };
        }
        #endregion
    }
}
