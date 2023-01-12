using LibraryApp.Common.Enums;
using LibraryApp.Data.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryApp.Data.Dtos.Dashboard
{
    public class CountDto
    {
        public int UsersCount { get; set; }
        public int BooksCount { get; set; }
        public int AuthorsCount { get; set; }
    }
}
