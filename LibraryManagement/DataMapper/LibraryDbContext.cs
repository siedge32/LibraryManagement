// <copyright file="LibraryDbContext.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using System.Data.Entity;
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The database context class
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class LibraryDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibraryDbContext"/> class.
        /// </summary>
        public LibraryDbContext() :
            base("LibraryConnectionString")
        {
        }

        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public DbSet<Author> Authors { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public virtual DbSet<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the book publications.
        /// </summary>
        /// <value>
        /// The book publications.
        /// </value>
        public virtual DbSet<BookPublication> BookPublications { get; set; }

        /// <summary>
        /// Gets or sets the book stocks.
        /// </summary>
        /// <value>
        /// The book stocks.
        /// </value>
        public virtual DbSet<BookStock> BookStocks { get; set; }

        /// <summary>
        /// Gets or sets the book withdrawals.
        /// </summary>
        /// <value>
        /// The book withdrawals.
        /// </value>
        public virtual DbSet<BookWithdrawal> BookWithdrawals { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        public virtual DbSet<Extension> Extensions { get; set; }

        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public virtual DbSet<Field> Fields { get; set; }

        /// <summary>
        /// Gets or sets the librarians.
        /// </summary>
        /// <value>
        /// The librarians.
        /// </value>
        public virtual DbSet<Librarian> Librarians { get; set; }

        /// <summary>
        /// Gets or sets the publishing houses.
        /// </summary>
        /// <value>
        /// The publishing houses.
        /// </value>
        public virtual DbSet<PublishingHouse> PublishingHouses { get; set; }

        /// <summary>
        /// Gets or sets the readers.
        /// </summary>
        /// <value>
        /// The readers.
        /// </value>
        public virtual DbSet<Reader> Readers { get; set; }
    }
}
