// <copyright file="BookPublicationTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The BookPublicationTests class
    /// </summary>
    [TestFixture]
    public class BookPublicationTests
    {
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
        /// The Publishing House
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
        /// Gets or sets the book publication service.
        /// </summary>
        /// <value>
        /// The book publication service.
        /// </value>
        private BookPublicationService BookPublicationService { get; set; }

        /// <summary>
        /// Gets or sets the library context mock.
        /// </summary>
        /// <value>
        /// The library context mock.
        /// </value>
        private Mock<LibraryDbContext> LibraryContextMock { get; set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var mockSet = new Mock<DbSet<BookPublication>>();
            this.LibraryContextMock = new Mock<LibraryDbContext>();
            this.LibraryContextMock.Setup(m => m.Set<BookPublication>()).Returns(mockSet.Object);
            this.BookPublicationService = new BookPublicationService(new BookPublicationRepository(this.LibraryContextMock.Object));

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
                NumberOfBooksForLecture = 7,
                BookPublication = this.bookpH
            };

            this.bookpH = new BookPublication
            {
                Id = 1,
                NumberOfPages = 36,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock,
                BookWithdrawals = new List<BookWithdrawal>()
            };

            this.book.BookPublications = new List<BookPublication> { this.bookpH };
        }

        /// <summary>
        /// Tests the add book publication null.
        /// </summary>
        [Test]
        public void TestAddBookPublicationNull()
        {
            BookPublication booPhInvalid = null;

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication invalid number of pages0.
        /// </summary>
        [Test]
        public void TestAddBookPublicationInvalidNumberOfPages0()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = -1,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication invalid number of pages1.
        /// </summary>
        [Test]
        public void TestAddBookPublicationInvalidNumberOfPages1()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 100000000,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication invalid number of pages2.
        /// </summary>
        [Test]
        public void TestAddBookPublicationInvalidNumberOfPages2()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 0,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication null cover material.
        /// </summary>
        [Test]
        public void TestAddBookPublicationNullCoverMaterial()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = null,
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication string length cover material0.
        /// </summary>
        [Test]
        public void TestAddBookPublicationStringLengthCoverMaterial0()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = string.Empty,
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication string length cover material1.
        /// </summary>
        [Test]
        public void TestAddBookPublicationStringLengthCoverMaterial1()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "a",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication string length cover material2.
        /// </summary>
        [Test]
        public void TestAddBookPublicationStringLengthCoverMaterial2()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ut dui nec risus facilisis semper nec ut sapien. Curabitur non tincidunt arcu. Etiam ac elementum magna. Vivamus semper turpis suscipit condimentum malesuada. Sed sed massa facilisis, commodo est in, euismod nunc. Nulla vitae eros scelerisque, finibus nisl quis, tincidunt ipsum. Maecenas sodales tristique augue, non varius mi maximus et. Curabitur tempus, mi vel scelerisque eleifend, augue ex pellentesque diam, ut pellentesque libero elit sit amet justo.",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication regex cover material digits.
        /// </summary>
        [Test]
        public void TestAddBookPublicationRegexCoverMaterialDigits()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "sintetic123",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication regex cover material symbols.
        /// </summary>
        [Test]
        public void TestAddBookPublicationRegexCoverMaterialSymbols()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "sintetic123!@#",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication null publishing house.
        /// </summary>
        [Test]
        public void TestAddBookPublicationNullPublishingHouse()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = null,
                Book = this.book,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication null book.
        /// </summary>
        [Test]
        public void TestAddBookPublicationNullBook()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = this.pH,
                Book = null,
                BookStock = this.bookStock
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication null book stock.
        /// </summary>
        [Test]
        public void TestAddBookPublicationNullBookStock()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = null
            };

            var wasCreated = this.BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add book publication valid.
        /// </summary>
        [Test]
        public void TestAddBookPublicationValid()
        {
            var wasCreated = this.BookPublicationService.CreateBookPublication(this.bookpH);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the can rent book invalid.
        /// </summary>
        [Test]
        public void TestCanRentBookInvalid()
        {
            var invalidBookStock = new BookStock
            {
                NumberOfBooks = 3,
                NumberOfBooksForLecture = 2
            };

            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = invalidBookStock
            };

            var canRent = this.BookPublicationService.CanRentBookStockAmount(booPhInvalid);
            Assert.False(canRent);
        }

        /// <summary>
        /// Tests the can rent book valid.
        /// </summary>
        [Test]
        public void TestCanRentBookValid()
        {
            var canRent = this.BookPublicationService.CanRentBookStockAmount(this.bookpH);
            Assert.True(canRent);
        }

        /// <summary>
        /// Tests the can rent book stock amount.
        /// </summary>
        [Test]
        public void TestCanRentBookStockAmount()
        {
            this.bookStock.NumberOfBooks = 7;
            var canRent = this.BookPublicationService.CanRentBookStockAmount(this.bookpH);
            Assert.False(canRent);
        }

        /// <summary>
        /// Tests the get book withdrawals.
        /// </summary>
        [Test]
        public void TestGetBookWithdrawals()
        {
            Assert.IsTrue(this.bookpH.BookWithdrawals.Count == 0);
        }

        /// <summary>
        /// Tests the set identifier.
        /// </summary>
        [Test]
        public void TestSetId()
        {
            Assert.IsTrue(this.bookpH.Id == 1);
        }
    }
}
