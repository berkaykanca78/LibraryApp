using AutoMapper;
using LibraryApp.Data.Context;
using LibraryApp.DataAccess.UnitOfWork.Abstract;
using LibraryApp.WebApi.Services.Abstract;

namespace LibraryApp.WebApi.Services.Concrete
{
    public class AddressService : IAddressService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddressService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
    }
}
