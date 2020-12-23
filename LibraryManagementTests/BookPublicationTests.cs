using LibraryManagement.BusinessLayer;
using LibraryManagement.DataMapper;
using LibraryManagement.DomainModel;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementTests
{
    [TestFixture]
    public class BookPublicationTests
    {
        private BookPublicationService BookPublicationService { get; set; }
        private Mock<LibraryDbContext> LibraryContextMock { get; set; }

        private Field field1;
        private Field field2;
        private Author author;
        private Book book;
        private PublishingHouse pH;
        private BookStock bookStock;
        private BookPublication bookpH;

        [SetUp]
        public void SetUp()
        {
            var mockSet = new Mock<DbSet<BookPublication>>();
            LibraryContextMock = new Mock<LibraryDbContext>();
            LibraryContextMock.Setup(m => m.Set<BookPublication>()).Returns(mockSet.Object);
            BookPublicationService = new BookPublicationService(new BookPublicationRepository(LibraryContextMock.Object));

            field1 = new Field { Name = "Art" };
            field2 = new Field { Name = "Science" };

            author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            book = new Book
            {
                Name = "Art - Science",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = new List<Author> { author }
            };

            pH = new PublishingHouse
            {
                Name = "Corint"
            };

            bookStock = new BookStock
            {
                NumberOfBooks = 10,
                NumberOfBooksForLecture = 7
            };

            bookpH = new BookPublication
            {
                NumberOfPages = 36,
                CoverMaterial = "Textil",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            book.BookPublications = new List<BookPublication> { bookpH };
        }

        [Test]
        public void TestAddBookPublicationNull()
        {
            BookPublication booPhInvalid = null;

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationInvalidNumberOfPages0()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = -1,
                CoverMaterial = "Textil",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationInvalidNumberOfPages1()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 100000000,
                CoverMaterial = "Textil",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationNullCoverMaterial()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = null,
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationStringLengthCoverMaterial0()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationStringLengthCoverMaterial1()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "a",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationStringLengthCoverMaterial2()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque ut dui nec risus facilisis semper nec ut sapien. Curabitur non tincidunt arcu. Etiam ac elementum magna. Vivamus semper turpis suscipit condimentum malesuada. Sed sed massa facilisis, commodo est in, euismod nunc. Nulla vitae eros scelerisque, finibus nisl quis, tincidunt ipsum. Maecenas sodales tristique augue, non varius mi maximus et. Curabitur tempus, mi vel scelerisque eleifend, augue ex pellentesque diam, ut pellentesque libero elit sit amet justo.",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationNullPublishingHouse()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = null,
                Book = book,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationNullBook()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = pH,
                Book = null,
                BookStock = bookStock
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationNullBookStock()
        {
            var booPhInvalid = new BookPublication
            {
                NumberOfPages = 673,
                CoverMaterial = "Sintetic",
                PublishingHouse = pH,
                Book = book,
                BookStock = null
            };

            var wasCreated = BookPublicationService.CreateBookPublication(booPhInvalid);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddBookPublicationValid()
        {
            var wasCreated = BookPublicationService.CreateBookPublication(bookpH);
            Assert.True(wasCreated);
            LibraryContextMock.Verify(bP => bP.SaveChanges(), Times.Once());
        }

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
                PublishingHouse = pH,
                Book = book,
                BookStock = invalidBookStock
            };

            var canRent = BookPublicationService.CanRentBookStockAmount(booPhInvalid);
            Assert.False(canRent);
        }

        [Test]
        public void TestCanRentBookValid()
        {
            var canRent = BookPublicationService.CanRentBookStockAmount(bookpH);
            Assert.True(canRent);
        }
    }
}
