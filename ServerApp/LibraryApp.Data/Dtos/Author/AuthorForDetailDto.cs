using LibraryApp.Common.Enums;
using LibraryApp.Data.Dtos.Book;
using System;
using System.Collections.Generic;

namespace LibraryApp.Data.Dtos.Author
{
    public class AuthorForDetailDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Country => "Türkiye";
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ImageUrl { get; set; }
        public GenderEnum Gender { get; set; }
        public string Introduction { get; set; }
        public int Age { get; set; }
        public List<BookForDetailDto> Books { get; set; }
    }
}
