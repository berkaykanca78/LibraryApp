using LibraryApp.Common.Enums;
using System;

namespace LibraryApp.Data.Dtos.User
{
    public class UserForDetailDto
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public GenderEnum Gender { get; set; }
        public string UserName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime LastActiveDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string StreetName { get; set; }
        public string ZipCode { get; set; }
        public string FullAddress { get; set; }
        public string Introduction { get; set; }
        public string Hobbies { get; set; }
        public string ImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string GenderName { get; set; }
        public int Age { get; set; }
        public string RoleName { get; set; }


    }
}
