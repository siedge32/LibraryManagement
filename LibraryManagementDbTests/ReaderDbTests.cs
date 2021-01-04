// <copyright file="ReaderDbTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementDbTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using NUnit.Framework;

    /// <summary>
    /// The ReaderDatabaseTests class
    /// </summary>
    [TestFixture]
    public class ReaderDbTests
    {
        /// <summary>
        /// The reader
        /// </summary>
        private Reader reader;

        /// <summary>
        /// The librarian
        /// </summary>
        private Librarian librarian;

        /// <summary>
        /// The field1
        /// </summary>
        private Field field1;

        /// <summary>
        /// The field2
        /// </summary>
        private Field field2;

        /// <summary>
        /// The author
        /// </summary>
        private Author author;

        /// <summary>
        /// The book
        /// </summary>
        private Book book;

        /// <summary>
        /// The p h
        /// </summary>
        private PublishingHouse pH;

        /// <summary>
        /// The book stock
        /// </summary>
        private BookStock bookStock;

        /// <summary>
        /// The book publication
        /// </summary>
        private BookPublication bookpH;

        /// <summary>
        /// Gets the Book publication 2.
        /// </summary>
        /// <value>
        /// The bookp h2.
        /// </value>
        private BookPublication bookpH2;

        /// <summary>Gets the library context.</summary>
        /// <value>The library context.</value>
        public LibraryDbContext LibraryContext { get; private set; }

        /// <summary>
        /// Gets the librarian service.
        /// </summary>
        /// <value>
        /// The librarian service.
        /// </value>
        public LibrarianService LibrarianService { get; private set; }

        /// <summary>
        /// Gets the reader service.
        /// </summary>
        /// <value>
        /// The reader service.
        /// </value>
        public ReaderService ReaderService { get; private set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.LibraryContext = new LibraryDbContext();
            this.LibrarianService = new LibrarianService(new LibrarianRepository(this.LibraryContext));
            this.ReaderService = new ReaderService(new ReaderRepository(this.LibraryContext), new BookPublicationRepository(this.LibraryContext));

            this.reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            this.librarian = new Librarian
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            this.field1 = new Field { Name = "Art" };
            this.field2 = new Field { Name = "Science" };

            this.author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            this.book = new Book
            {
                Name = "Art - Science",
                Categories = new List<Field>
                {
                   this.field1, this.field2
                },
                Authors = new List<Author> { this.author }
            };

            this.pH = new PublishingHouse
            {
                Name = "Corint"
            };

            this.bookStock = new BookStock
            {
                NumberOfBooks = 10,
                NumberOfBooksForLecture = 7
            };

            this.bookpH = new BookPublication
            {
                NumberOfPages = 36,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            this.bookpH2 = new BookPublication
            {
                NumberOfPages = 72,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            this.book.BookPublications = new List<BookPublication> { this.bookpH, this.bookpH2 };
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            this.LibraryContext.Database.Delete();
        }

        /// <summary>
        /// Tests the is librarian registered as reader invalid.
        /// </summary>
        [Test]
        public void TestIsLibrarianRegisteredAsReaderInvalid()
        {
            this.reader.Email = "smth@stmh.com";
            this.librarian.Email = "other@other.com";

            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);
            Assert.False(isLibrarian);
        }

        /// <summary>
        /// Tests the is librarian registered as reader valid.
        /// </summary>
        [Test]
        public void TestIsLibrarianRegisteredAsReaderValid()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);
            Assert.True(isLibrarian);
        }

        /// <summary>
        /// Tests the check number of books in period valid.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksInPeriodValid()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);
            var canRent = this.ReaderService.CheckNumberOfBooksInPeriod(this.book.BookPublications.ToList(), this.reader, isLibrarian);
            Assert.True(canRent);
        }

        /// <summary>
        /// Tests the check number of books valid.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksValid()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var listBookPublications = new List<BookPublication> { this.bookpH };
            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);
            var isValid = this.ReaderService.CheckNumberOfBooks(listBookPublications, isLibrarian);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the check number of fields in last given months valid.
        /// </summary>
        [Test]
        public void TestCheckNumberOfFieldsInLastGivenMonthsValid()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var parentField = new Field { Name = "Design" };
            this.field1.ParentField = parentField;
            var listBookPublications = new List<BookPublication> { this.bookpH };

            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);
            var isValid = this.ReaderService.CheckNumberOfFieldsInLastGivenMonths(listBookPublications, this.reader, isLibrarian);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the check same book rented delta valid.
        /// </summary>
        [Test]
        public void TestCheckSameBookRentedDeltaValid()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = this.reader,
                BookPublications = this.book.BookPublications,
            };

            this.reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);
            var canRent = this.ReaderService.CheckSameBookRentedDelta(this.book.BookPublications.ToList(), this.reader, isLibrarian);
            Assert.IsTrue(canRent);
        }

        /// <summary>
        /// Tests the check number of books in one day valid.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksInOneDayValid()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var isLibrarian = this.ReaderService.IsLibrarianRegisteredAsReader(this.reader);

            var canRent = this.ReaderService.CheckNumberOfBooksInOneDay(new List<BookPublication> { this.book.BookPublications.First() }, this.reader, isLibrarian);
            Assert.IsTrue(canRent);
        }

        /// <summary>
        /// Tests the can add extension.
        /// </summary>
        [Test]
        public void TestCanAddExtension()
        {
            var wasCreated = this.LibrarianService.AddLibrarian(this.librarian);
            Assert.True(wasCreated);

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = this.reader,
                BookPublications = this.book.BookPublications
            };

            var extension1 = new Extension
            {
                BookWithdrawal = bookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 2, 1),
                DateToReturn = new DateTime(2020, 3, 1)
            };

            bookWithdrawal.Extensions = new List<Extension> { extension1 };

            this.reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var currentBookWithdrawal = new BookWithdrawal() { Extensions = new List<Extension>() };

            var currentExtension = new Extension
            {
                Id = 1,
                BookWithdrawal = currentBookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 2, 1)
            };

            var canAddExtension = this.ReaderService.AddExtension(this.reader, currentExtension);
            Assert.IsTrue(canAddExtension);
        }
    }
}
