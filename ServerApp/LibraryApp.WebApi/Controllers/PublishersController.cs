using LibraryApp.Data.Dtos.Publisher;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using LibraryApp.WebApi.Services.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class PublishersController : ControllerBase
    {
        private readonly IPublisherService _publisherService;
        public PublishersController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet(Name = "GetPublishersForFillDropdown")]
        public async Task<ServiceResult> GetPublishersForFillDropdown()
        {
            return await _publisherService.GetPublishersForFillDropdown().ConfigureAwait(false);
        }

        [HttpPost(Name = "AddPublisher")]
        public async Task<ServiceResult> AddPublisher(PublisherForAddDto model)
        {
            return await _publisherService.AddPublisher(model).ConfigureAwait(false);
        }
    }
}
