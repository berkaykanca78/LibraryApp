using LibraryApp.Common.Enums;
using LibraryApp.WebToken.Jwt.Abstract;
using LibraryApp.WebToken.Jwt.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace LibraryApp.WebToken.Jwt.Concrete
{
    public class JwtHelper : IJwtHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public JwtHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public JwtDto GetJwtDto()
        {
            JwtDto jwtDto = new JwtDto();
            try
            {
                jwtDto.Id = int.Parse(GetValueFromToken("id"));
                jwtDto.Age = int.Parse(GetValueFromToken("age"));
                jwtDto.StreetName = GetValueFromToken("streetName");
                jwtDto.UserName = GetValueFromToken("userName");
                jwtDto.PhoneNumber = GetValueFromToken("phoneNumber");
                jwtDto.CreatedDate = DateTime.Parse(GetValueFromToken("createdDate"));
                jwtDto.LastActiveDate = DateTime.Parse(GetValueFromToken("lastActiveDate"));
                jwtDto.Email = GetValueFromToken("email");
                jwtDto.City = GetValueFromToken("city");
                jwtDto.Country = GetValueFromToken("country");
                jwtDto.DateOfBirth = DateTime.Parse(GetValueFromToken("dateOfBirth"));
                jwtDto.FullAddress = GetValueFromToken("fullAddress");
                jwtDto.Gender = Enum.Parse<GenderEnum>(GetValueFromToken("gender"));
                jwtDto.Hobbies = GetValueFromToken("hobbies");
                jwtDto.ImageUrl = GetValueFromToken("imageUrl");
                jwtDto.Introduction = GetValueFromToken("introduction");
                jwtDto.Name = GetValueFromToken("name");
                jwtDto.Surname = GetValueFromToken("surname");
                jwtDto.ZipCode = GetValueFromToken("zipCode");
                jwtDto.RoleName = GetValueFromToken("roleName");
                jwtDto.RoleId = int.Parse(GetValueFromToken("roleId"));
                return jwtDto;
            }
            catch (Exception)
            {
                return jwtDto;
            }
        }

        public string GetValueFromToken(string propertyName)
        {
            try
            {
                var jwt = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                var handler = new JwtSecurityTokenHandler();
                var tokens = handler.ReadToken(jwt[0].Replace("Bearer ", "")) as JwtSecurityToken;
                return tokens.Claims.FirstOrDefault(claim => claim.Type == propertyName).Value;
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
