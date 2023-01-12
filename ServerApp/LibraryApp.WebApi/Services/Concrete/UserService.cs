using AutoMapper;
using FluentValidation.Results;
using LibraryApp.Common.Helpers;
using LibraryApp.Data.Dtos.User;
using LibraryApp.Data.Entities;
using LibraryApp.DataAccess.Repositories.Abstract;
using LibraryApp.DataAccess.UnitOfWork.Abstract;
using LibraryApp.Entities.Enums;
using LibraryApp.Entities.Models;
using LibraryApp.Validation.FluentValidation;
using LibraryApp.WebApi.Services.Abstract;
using LibraryApp.WebToken.Jwt.Abstract;
using LibraryApp.WebToken.Jwt.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IAddressRepository addresRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IJwtHelper jwtHelper;
        private readonly JwtDto currentUser;
        private readonly UserManager<User> userManager;


        public UserService(IUserRepository userRepository, IAddressRepository addresRepository, IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, IJwtHelper jwtHelper)
        {
            this.userRepository = userRepository;
            this.addresRepository = addresRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
            this.jwtHelper = jwtHelper;
            this.currentUser = jwtHelper.GetJwtDto();
        }

        #region Queries
        public async Task<ServiceResult> GetUsers(UserQueryParams userParams)
        {
            List<UserForListDto> users = await userRepository.GetUsers(userParams);
            return new ServiceResult { ResultType = ResultType.Success, Data = users, Messages = new List<string>() { } };
        }

        public async Task<ServiceResult> GetUsersForDashboard()
        {
            List<UserForDetailDto> users = await userRepository.GetUsersForDashboard();
            return new ServiceResult { ResultType = ResultType.Success, Data = users, Messages = new List<string>() { } };
        }

        public async Task<ServiceResult> GetUserById(int id)
        {
            User user = await userRepository.GetByIdAsync(id);
            Address adress = await addresRepository.GetByIdAsync(user.AddressId);
            user.Address = adress;
            UserForDetailDto result = mapper.Map<UserForDetailDto>(user);
            return new ServiceResult { ResultType = ResultType.Success, Data = result, Messages = new List<string>() { } };
        }

        public async Task<ServiceResult> GetUserForRegister(UserForRegisterDto model)
        {
            User user = mapper.Map<User>(model);
            return await Task.FromResult(new ServiceResult { Data = user }).ConfigureAwait(false);
        }
        #endregion

        #region Commands
        public async Task<ServiceResult> AddUser(UserForRegisterDto model)
        {
            ServiceResult serviceResult = new ServiceResult { ResultType = ResultType.Success, Data = null, Messages = null };

            User user = mapper.Map<User>(model);

            (bool isValid, List<ValidationFailure> errors) = ValidateHelper.Validate(new UserValidator(), user);

            List<string> errorMessages = new List<string>();

            if (errors != null)
                errorMessages = errors.Select(x => x.ErrorMessage).ToList() ?? new List<string>() { };

            if (!isValid)
            {
                serviceResult.ResultType = ResultType.Error;
                foreach (var errorMessage in errorMessages)
                {
                    serviceResult.Messages.Add(errorMessage);
                }
                serviceResult.Data = null;
            }
            user.CreatedBy = currentUser.Id;
            user.CreatedDate = DateTime.Now;
            user.CreatedRoleId = currentUser.RoleId;
            await userManager.CreateAsync(user, model.Password);
            await userManager.AddToRoleAsync(user, "User");

            return await Task.FromResult(serviceResult);
        }

        public async Task<ServiceResult> UpdateUser(UserForDetailDto model)
        {
            ServiceResult serviceResult = new ServiceResult { ResultType = ResultType.Success, Data = model, Messages = null };

            User userBase = await userRepository.GetByIdAsync(model.Id);

            Address address = await addresRepository.GetByIdAsync(userBase.AddressId);
            address.City = model.City;
            address.Country = "Türkiye";
            address.StreetName = model.StreetName;
            address.ZipCode = model.ZipCode;
            address.FullAddress = model.FullAddress;
            address.UpdatedBy = currentUser.Id;
            address.UpdatedDate = DateTime.Now;
            address.UpdatedRoleId = currentUser.RoleId;

            await unitOfWork.GetRepository<Address>().UpdateAsync(address, address.Id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);

            User user = mapper.Map<User>(model);
            userBase.Name = user.Name;
            userBase.Surname = user.Surname;
            userBase.Email = user.Email;
            userBase.Gender = user.Gender;
            userBase.UserName = user.UserName;
            userBase.Introduction = user.Introduction;
            userBase.Hobbies = user.Hobbies;
            userBase.ImageUrl = user.ImageUrl;
            userBase.PhoneNumber = user.PhoneNumber;
            userBase.DateOfBirth = user.DateOfBirth;
            userBase.UpdatedBy = currentUser.Id;
            userBase.UpdatedDate = DateTime.Now;
            userBase.UpdatedRoleId = currentUser.RoleId;

            (bool isValid, List<ValidationFailure> errors) = ValidateHelper.Validate(new UserValidator(), user);

            List<string> errorMessages = new List<string>();

            if (errors != null)
                errorMessages = errors.Select(x => x.ErrorMessage).ToList() ?? new List<string>() { };

            if (!isValid)
            {
                serviceResult.ResultType = ResultType.Error;
                foreach (var errorMessage in errorMessages)
                {
                    serviceResult.Messages.Add(errorMessage);
                }
                serviceResult.Data = null;
            }

            serviceResult.Data = userBase;
            await userManager.UpdateAsync(userBase);

            return await Task.FromResult(serviceResult);
        }

        public async Task<ServiceResult> DeleteUser(int id)
        {
            User user = await userRepository.GetByIdAsync(id);
            user.UpdatedBy = currentUser.Id;
            user.UpdatedDate = DateTime.Now;
            user.UpdatedRoleId = currentUser.RoleId;
            unitOfWork.GetRepository<User>().Delete(id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);

            Address address = await addresRepository.GetByIdAsync(user.AddressId);
            address.UpdatedBy = currentUser.Id;
            address.UpdatedDate = DateTime.Now;
            address.UpdatedRoleId = currentUser.RoleId;
            unitOfWork.GetRepository<User>().Delete(id);
            await unitOfWork.CompleteAsync(false).ConfigureAwait(false);

            return new ServiceResult { ResultType = ResultType.Success };
        }
        #endregion

    }
}
