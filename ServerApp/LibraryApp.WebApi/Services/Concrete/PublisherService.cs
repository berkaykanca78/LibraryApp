using LibraryApp.Common.Helpers;
using LibraryApp.Data.Context;
using LibraryApp.Data.Dtos.Dropdown;
using LibraryApp.Data.Dtos.Publisher;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.DataAccess.UnitOfWork.Abstract;
using LibraryApp.Entities.Enums;
using LibraryApp.Entities.Models;
using LibraryApp.WebApi.Services.Abstract;
using LibraryApp.WebToken.Jwt.Abstract;
using LibraryApp.WebToken.Jwt.Concrete;
using LibraryApp.WebToken.Jwt.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Concrete
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository publisherRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IJwtHelper jwtHelper;
        private readonly JwtDto currentUser;

        public PublisherService(IPublisherRepository publisherRepository, IUnitOfWork unitOfWork, IJwtHelper jwtHelper)
        {
            this.publisherRepository = publisherRepository;
            this.unitOfWork = unitOfWork;
            this.currentUser = jwtHelper.GetJwtDto();
            this.jwtHelper = jwtHelper;
        }

        #region Queries
        public async Task<ServiceResult> GetPublishersForFillDropdown()
        {
            List<DropdownDto> publishers = await publisherRepository.GetPublishersForFillDropdown();
            return new ServiceResult { ResultType = ResultType.Success, Data = publishers, Messages = null };
        }
        #endregion

        #region Commands
        public async Task<ServiceResult> AddPublisher(PublisherForAddDto model)
        {
            Publisher publisher = MapperHelper.MapFrom<Publisher>(model);
            publisher.CreatedDate = DateTime.Now;
            publisher.CreatedBy = currentUser.Id;
            publisher.CreatedRoleId = currentUser.RoleId;
            await publisherRepository.AddAsync(publisher);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);
            return new ServiceResult { ResultType = ResultType.Success, Data = publisher };
        }
        #endregion

    }
}
