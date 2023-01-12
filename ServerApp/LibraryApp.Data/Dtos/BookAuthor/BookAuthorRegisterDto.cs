using LibraryApp.Common.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Data.Dtos.BookAuthor
{
    public class BookAuthorRegisterDto
    {
        #region Author
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string Country => "Türkiye";
        public string City { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string AuthorImageUrl { get; set; }
        public GenderEnum Gender { get; set; }
        public string AuthorIntroduction { get; set; }
        public int Age { get; set; }
        #endregion

        #region Book
        public string Title { get; set; }
        public MediaTypesEnum MediaType { get; set; }
        public LanguagesEnum Language { get; set; }
        [Column(TypeName = "money")]
        public double Price { get; set; }
        public int TotalPages { get; set; }
        public DateTime PublishedDate { get; set; }
        public string BookImageUrl { get; set; }
        public string BookIntroduction { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        #endregion
    }
}
