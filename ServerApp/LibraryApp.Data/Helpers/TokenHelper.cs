using LibraryApp.Common.Extensions;
using LibraryApp.Common.Variables;
using LibraryApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LibraryApp.Common.Helpers
{
    public static class TokenHelper
    {
        public static string GenerateJwtToken(User user, UserManager<User> userManager, RoleManager<Role> roleManager) 
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            Task<IList<string>> roleNameList = userManager.GetRolesAsync(user);
            string roleName = roleNameList.Result.FirstOrDefault();
            int roleId = roleManager.Roles.FirstOrDefault(x => x.Name == roleName).Id;

            ClaimsIdentity claims = new ClaimsIdentity(new Claim[] {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("id", user.Id.ToString()),
                new Claim("name", user.Name),
                new Claim("surname", user.Surname),
                new Claim("gender", user.Gender.ToString()),
                new Claim("email", user.Email),
                new Claim("userName", user.UserName),
                new Claim("dateOfBirth", user.DateOfBirth.ToString()),
                new Claim("country", user.Address.Country),
                new Claim("city", user.Address.City),
                new Claim("streetName", user.Address.StreetName),
                new Claim("zipCode", user.Address.ZipCode),
                new Claim("fullAddress", user.Address.FullAddress),
                new Claim("introduction", user.Introduction),
                new Claim("hobbies", user.Hobbies),
                new Claim("imageUrl", user.ImageUrl),
                new Claim("phoneNumber", user.PhoneNumber),
                new Claim("age", user.DateOfBirth.CalculateAge().ToString()),
                new Claim("createdDate", user.CreatedDate.ToString()),
                new Claim("lastActiveDate", user.LastActiveDate.ToString()),
                new Claim("roleName", roleName),
                new Claim("roleId", roleId.ToString())
           });

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConnectionConstants.JwtKey));

            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
