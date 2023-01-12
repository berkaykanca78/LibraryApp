using LibraryApp.Common.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Data.Dtos.Book
{
    public class BookForDetailDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public MediaTypesEnum MediaType { get; set; }
        public LanguagesEnum Language { get; set; }
        public decimal Rating => 5;
        [Column(TypeName = "money")]
        public double Price { get; set; }
        public int TotalPages { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public string AuthorName { get; set; }
        public string MediaTypeName { get; set; }
        public string LanguageName { get; set; }
        public string PublisherName { get; set; }
        public string CategoryName { get; set; }
        public string Introduction { get; set; }
        public int BookAuthorId { get; set; }
        public int AuthorId { get; set; }
        public int CategoryId { get; set; }
        public int PublisherId { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedRoleId { get; set; }
    }
}
