using LibraryApp.Common.Enums;
using LibraryApp.Data.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Data.Entities
{
    public class Book : AuditableEntity
    {
        public string Title { get; set; }
        public MediaTypesEnum MediaType { get; set; }
        public LanguagesEnum Language { get; set; }
        public decimal Rating { get; set; }
        [Column(TypeName = "money")]
        public double Price { get; set; }
        public int TotalPages { get; set; }
        public DateTime PublishedDate { get; set; }
        public string ImageUrl { get; set; }
        public string Introduction { get; set; }

        #region 1-n Relations (Book-Publisher)
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        #endregion

        #region 1-n Relations (Book-Category)
        public int CategoryId { get; set; } 
        public virtual Category Category { get; set; }
        #endregion

        #region 1-n Relations (Book-BookAuthor)
        public int? BookAuthorId { get; set; }
        public virtual BookAuthor BookAuthor { get; set; }
        #endregion
    }
}
