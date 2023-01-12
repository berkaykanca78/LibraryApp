using LibraryApp.Data.Dtos.Publisher;
using LibraryApp.Entities.Models;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Abstract
{
    public interface IPublisherService
    {
        Task<ServiceResult> GetPublishersForFillDropdown();
        Task<ServiceResult> AddPublisher(PublisherForAddDto model);
    }
}
