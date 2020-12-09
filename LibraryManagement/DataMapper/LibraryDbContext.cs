using LibraryManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.DataMapper
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext() :
            base("LibraryConnectionString")
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookPublication> BookPublications { get; set; }
        public DbSet<BookStock> BookStocks { get; set; }
        public DbSet<BookWithdrawal> BookWithdrawals { get; set; }
        public DbSet<Extension> Extensions { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<PublishingHouse> PublishingHouses { get; set; }
        public DbSet<Reader> Readers { get; set; }
    }
}
