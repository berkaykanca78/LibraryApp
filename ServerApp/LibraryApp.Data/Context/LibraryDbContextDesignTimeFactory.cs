using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryApp.Data.Context
{
    public class LibraryDbContextDesignTimeFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var context = new LibraryDbContext();
            return context;
        }
    }
}
