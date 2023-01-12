using LibraryApp.Common.Enums;
using System;

namespace LibraryApp.Data.Dtos.Book
{
    public class BookForListDto
    {
        public int? Id { get; set; }
        public int No { get; set; }
        public string Title { get; set; }
        public string MediaTypeName { get; set; }
        public string LanguageName { get; set; }
        public decimal Rating { get; set; }
        public double Price { get; set; }
        public int TotalPages { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public MediaTypesEnum MediaType { get; set; }
        public LanguagesEnum Language { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int AuthorId { get; set; }
        public int BookAuthorId { get; set; }
        public string Introduction { get; set; }
    }
}
