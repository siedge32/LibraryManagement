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
    public class BookTests
    {
        private BookService BookService { get; set; }
        private Mock<LibraryDbContext> LibraryContextMock { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mockSet = new Mock<DbSet<Book>>();
            LibraryContextMock = new Mock<LibraryDbContext>();
            LibraryContextMock.Setup(m => m.Set<Book>()).Returns(mockSet.Object);
            BookService = new BookService(new BookRepository(LibraryContextMock.Object));
        }

        [Test]
        public void TestAddNullBook()
        {
            Book nullBook = null;
            var wasCreated = BookService.CreateBook(nullBook);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullName()
        {
            var book = new Book
            {
                Name = null
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLengthName0()
        {
            var book = new Book
            {
                Name = ""
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLengthName1()
        {
            var book = new Book
            {
                Name = "a"
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLengthName2()
        {
            var book = new Book
            {
                Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero. "
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullCategories()
        {
            var book = new Book
            {
                Name = "Something",
                Categories = null
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddEmptyCategories()
        {
            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>()
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddMoreCategoriesThanDOM()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Science" };
            var field3 = new Field { Name = "Philosophy" };

            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field2, field3
                },
                Authors = new List<Author> { author }
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddRelatedOneLevelInheritance()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Painting", ParentField = field1 };

            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = new List<Author> { author }
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddRelatedTwoLevelInheritance()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Painting", ParentField = field1 };
            var field3= new Field { Name = "Colours", ParentField = field2 };

            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field3
                },
                Authors = new List<Author> { author }
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddNullAuthors()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Science" };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = null
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddEmptyAuthors()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Science" };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = new List<Author>()
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddMultipleAuthors()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Science" };

            var author1 = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var author2 = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = new List<Author> { author1, author2 }
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.True(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        [Test]
        public void TestAddValidBook()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Science" };

            var author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            var book = new Book
            {
                Name = "Something",
                Categories = new List<Field>
                {
                   field1, field2
                },
                Authors = new List<Author> { author }
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.True(wasCreated);
            LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
