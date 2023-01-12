using LibraryApp.Common.Variables;
using LibraryApp.Data.Common;
using LibraryApp.Data.Entities;
using LibraryApp.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LibraryApp.Data.Context
{
    public class LibraryDbContext:IdentityDbContext<User, Role, int>
    {

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) 
        {
        }

        public LibraryDbContext()
        {
        }

        protected async override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(CoreEventId.DetachedLazyLoadingWarning)).UseSqlServer(ConnectionConstants.DbConnectionString);
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var type in builder.Model.GetEntityTypes())
            {
                if (typeof(IBaseEntity).IsAssignableFrom(type.ClrType))
                    builder.SetSoftDeleteFilter(type.ClrType);
            }

            builder.HasDefaultSchema("dbo");

            builder.Entity<Category>()
                .HasMany(i => i.Books)
                .WithOne(i => i.Category)
                .HasForeignKey(i => i.CategoryId);

            builder.Entity<Publisher>()
                .HasMany(i => i.Books)
                .WithOne(i => i.Publisher)
                .HasForeignKey(i => i.PublisherId);

            builder.Entity<Address>()
                .HasOne(i => i.User)
                .WithOne(i => i.Address)
                .HasForeignKey<User>(i => i.AddressId);

            builder.Entity<BookAuthor>()
                .HasMany(i => i.Books)
                .WithOne(i => i.BookAuthor)
                .HasForeignKey(i => i.BookAuthorId);

            builder.Entity<BookAuthor>()
                .HasMany(i => i.Authors)
                .WithOne(i => i.BookAuthor)
                .HasForeignKey(i => i.BookAuthorId);

            builder.Entity<FavouritesAuthor>()
                .HasMany(i => i.Users)
                .WithOne(i => i.FavouritesAuthor);

            builder.Entity<FavouritesAuthor>()
                .HasMany(i => i.Authors)
                .WithOne(i => i.FavouritesAuthor);

            base.OnModelCreating(builder);

            
        }  
    }
}
