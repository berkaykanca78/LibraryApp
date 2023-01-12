using LibraryApp.Common.Enums;
using System;

namespace LibraryApp.Data.Dtos.Author
{
    public class AuthorForListDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Country { get; set; }
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public string Introduction { get; set; }
        public GenderEnum Gender { get; set; }
        public bool IsFavourite { get; set; }
        public int Age { get; set; }
        public string GenderName { get; set; }
        public int No { get; set; }
    }
}
