using Bogus;
using LibraryApp.Common.Enums;
using LibraryApp.Common.Helpers;
using LibraryApp.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApp.WebApi.Data
{
    public static class SeedDatabase
    {
        public static async Task Seed(UserManager<User> userManager, RoleManager<Role> roleManager)
        {

            if (!roleManager.Roles.Any())
            {
                Role roleAdmin = new Role() { Name = "Admin" };
                Role roleUser = new Role() { Name = "User" };
                await roleManager.CreateAsync(roleAdmin);
                await roleManager.CreateAsync(roleUser);
            }

            if (!userManager.Users.Any())
            {
                List<User> fakeUsers = new Faker<User>("tr")
                    .RuleFor(i => i.Name, i => i.Person.FirstName)
                    .RuleFor(i => i.Surname, i => i.Person.LastName)
                    .RuleFor(i => i.DateOfBirth, i => i.Person.DateOfBirth)
                    .RuleFor(i => i.Email, i => i.Person.Email.ToLowerInvariant())
                    .RuleFor(i => i.UserName, (i, j) => i.Internet.UserName(j.Name, j.Surname))
                    .RuleFor(i => i.Gender, i => (GenderEnum)i.Person.Gender)
                    .RuleFor(i => i.Hobbies, i => i.Lorem.Word())
                    .RuleFor(i => i.Introduction, i => i.Lorem.Word())
                    .RuleFor(i => i.PhoneNumber, i => i.Person.Phone)
                    .RuleFor(i => i.ImageUrl, i => i.Image.PicsumUrl())
                    .Generate(100);

                foreach (User fakeUser in fakeUsers)
                {
                    User user = MapperHelper.MapFrom<User>(fakeUser);
                    user.CreatedDate = DateTime.Now;
                    user.LastActiveDate = DateTime.Now;
                    Address fakeAddress = new Faker<Address>("tr")
                    .RuleFor(i => i.City, i => i.Address.City())
                    .RuleFor(i => i.Country, "Türkiye")
                    .RuleFor(i => i.StreetName, i => i.Address.StreetAddress())
                    .RuleFor(i => i.ZipCode, i => i.Address.ZipCode())
                    .Generate();
                    fakeAddress.FullAddress = fakeAddress.StreetName + " " + fakeAddress.City + " / Türkiye";
                    Address address = MapperHelper.MapFrom<Address>(fakeAddress);
                    user.Address = address;
                    await userManager.CreateAsync(user, "LibraryApp_2023");
                    await userManager.AddToRoleAsync(user, "User");
                }

                User fakeUserForAdmin = new Faker<User>("tr")
                   .RuleFor(i => i.Name, i => i.Person.FirstName)
                   .RuleFor(i => i.Surname, i => i.Person.LastName)
                   .RuleFor(i => i.DateOfBirth, i => i.Person.DateOfBirth)
                   .RuleFor(i => i.Email, i => i.Person.Email.ToLowerInvariant())
                   .RuleFor(i => i.UserName, (i, j) => i.Internet.UserName(j.Name, j.Surname))
                   .RuleFor(i => i.Gender, i => (GenderEnum)i.Person.Gender)
                   .RuleFor(i => i.Hobbies, i => i.Lorem.Word())
                   .RuleFor(i => i.Introduction, i => i.Lorem.Word())
                   .RuleFor(i => i.PhoneNumber, i => i.Person.Phone)
                   .RuleFor(i => i.ImageUrl, i => i.Image.PicsumUrl())
                   .Generate();

                Address fakeAddressForAdmin = new Faker<Address>("tr")
                    .RuleFor(i => i.City, i => i.Address.City())
                    .RuleFor(i => i.Country, "Türkiye")
                    .RuleFor(i => i.StreetName, i => i.Address.StreetAddress())
                    .RuleFor(i => i.ZipCode, i => i.Address.ZipCode())
                    .Generate();
                fakeAddressForAdmin.FullAddress = fakeAddressForAdmin.StreetName + " " + fakeAddressForAdmin.City + " / Türkiye";

                User adminUser = new User()
                {
                    UserName = "berkaykanca",
                    Name = "Berkay",
                    Surname = "Kanca",
                    Address= fakeAddressForAdmin,
                    CreatedDate= DateTime.Now,
                    DateOfBirth = new DateTime(1994, 05, 29, 7, 0, 0),
                    Email = "berkaykanca@hotmail.com",
                    Gender = GenderEnum.Erkek,
                    Hobbies = fakeUserForAdmin.Hobbies,
                    Introduction = fakeUserForAdmin.Introduction,
                    ImageUrl = fakeUserForAdmin.ImageUrl,
                    LastActiveDate= DateTime.Now,
                    PhoneNumber = "+90-505-618-1-906",
                };

                await userManager.CreateAsync(adminUser, "LibraryApp_2023");
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
