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
    public class ReaderTests
    {
        public Mock<LibraryDbContext> LibraryContextMock { get; private set; }

        public ReaderService ReaderService { get; private set; }

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
            var mockSet = new Mock<DbSet<Reader>>();
            LibraryContextMock = new Mock<LibraryDbContext>();
            LibraryContextMock.Setup(m => m.Set<Reader>()).Returns(mockSet.Object);
            ReaderService = new ReaderService(new ReaderRepository(LibraryContextMock.Object));

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

            var bookpH2 = new BookPublication
            {
                NumberOfPages = 72,
                CoverMaterial = "Textil",
                PublishingHouse = pH,
                Book = book,
                BookStock = bookStock
            };

            book.BookPublications = new List<BookPublication> { bookpH, bookpH2 };
        }

        [Test]
        public void TestAddNullReader()
        {
            Reader reader = null;
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullFirstName()
        {
            var reader = new Reader
            {
                FirstName = null,
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidStringLengthFirstName0()
        {
            var reader = new Reader
            {
                FirstName = "",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidStringLengthFirstName1()
        {
            var reader = new Reader
            {
                FirstName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero.",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidRegexFirstName0()
        {
            // Starts with lower case letter
            var reader = new Reader
            {
                FirstName = "bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidRegexFirstName1()
        {
            // Contains numbers
            var reader = new Reader
            {
                FirstName = "Bogdan123",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullLastName()
        {
            var reader = new Reader
            {
                LastName = null,
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidStringLengthLastName0()
        {
            var reader = new Reader
            {
                LastName = "",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidStringLengthLastName1()
        {
            var reader = new Reader
            {
                LastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero.",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidRegexSecondName0()
        {
            // Starts with lower case letter
            var reader = new Reader
            {
                LastName = "hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidRegexSecondName1()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu123",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullPhoneNumber()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = null,
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidRegexPhoneNumber()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "clearly not a phone number",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullEmail()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = null,
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidRegexEmail()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0743702901",
                Email = "smthstmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullGender()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = null
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidGender()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "Helicopter"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddValidReader()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        [Test]
        public void TestCheckNumberOfBooksInPeriodTrue()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var canRent = ReaderService.CheckNumberOfBooksInPeriod(book.BookPublications.ToList(), reader);
            Assert.True(canRent);
        }

        [Test]
        public void TestCheckNumberOfBooksInPeriodFalse()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                new BookWithdrawal
                {
                   DateRented = new DateTime(2020, 1, 1),
                   DateToReturn = new DateTime(2020, 1, 15),
                   Reader = reader,
                   Librarian = librarian,
                   BookPublications = book.BookPublications
                }
            };

            var canRent = ReaderService.CheckNumberOfBooksInPeriod(book.BookPublications.ToList(), reader);
            Assert.False(canRent);
        }

        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks0()
        {
            var books = new List<Book> { };
            var numberOfDistinctiveFields = ReaderService.NumberOfDistinctiveCategoriesForBooks(books);
            Assert.IsTrue(numberOfDistinctiveFields == 0);
        }

        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks1()
        {
            var books = new List<Book> { book };
            var numberOfDistinctiveFields = ReaderService.NumberOfDistinctiveCategoriesForBooks(books);
            Assert.IsTrue(numberOfDistinctiveFields == 2);
        }

        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks2()
        {
            var field3 = new Field { Name = "Math", ParentField = field2};
            book.Categories.Add(field3);
            var books = new List<Book> { book };
            var numberOfDistinctiveFields = ReaderService.NumberOfDistinctiveCategoriesForBooks(books);
            Assert.IsTrue(numberOfDistinctiveFields == 2);
        }

        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks3()
        {
            var field3 = new Field { Name = "Math", ParentField = field2 };
            var field4 = new Field { Name = "Painting", ParentField = field1 };
            book.Categories.Add(field3);
            book.Categories.Add(field4);
            var books = new List<Book> { book };
            var numberOfDistinctiveFields = ReaderService.NumberOfDistinctiveCategoriesForBooks(books);
            Assert.IsTrue(numberOfDistinctiveFields == 2);
        }

        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks4()
        {
            var field3 = new Field { Name = "Math", ParentField = field2 };
            var field4 = new Field { Name = "Painting", ParentField = field1 };
            var field5 = new Field { Name = "Gastronomy" };
            book.Categories.Add(field3);
            book.Categories.Add(field4);
            book.Categories.Add(field5);
            var books = new List<Book> { book };
            var numberOfDistinctiveFields = ReaderService.NumberOfDistinctiveCategoriesForBooks(books);
            Assert.IsTrue(numberOfDistinctiveFields == 3);
        }

        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks5()
        {
            var field3 = new Field { Name = "Math", ParentField = field2 };
            var field4 = new Field { Name = "Painting", ParentField = field1 };
            var field5 = new Field { Name = "Gastronomy" };

            book = new Book
            {
                Name = "Mancaruri Traditionale",
                Categories = new List<Field>
                {
                   field3, field4, field5
                }
            };

            var books = new List<Book> { book };
            var numberOfDistinctiveFields = ReaderService.NumberOfDistinctiveCategoriesForBooks(books);
            Assert.IsTrue(numberOfDistinctiveFields == 3);
        }
    }
}