// <copyright file="BookTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The BookTests class
    /// </summary>
    [TestFixture]
    public class BookTests
    {
        /// <summary>
        /// Gets or sets the book service.
        /// </summary>
        /// <value>
        /// The book service.
        /// </value>
        private BookService BookService { get; set; }

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
            var mockSet = new Mock<DbSet<Book>>();
            this.LibraryContextMock = new Mock<LibraryDbContext>();
            this.LibraryContextMock.Setup(m => m.Set<Book>()).Returns(mockSet.Object);
            BookService = new BookService(new BookRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Tests the add null book.
        /// </summary>
        [Test]
        public void TestAddNullBook()
        {
            Book nullBook = null;
            var wasCreated = BookService.CreateBook(nullBook);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the name of the add null.
        /// </summary>
        [Test]
        public void TestAddNullName()
        {
            var book = new Book
            {
                Name = null
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid length name0.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthName0()
        {
            var book = new Book
            {
                Name = string.Empty
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid length name1.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthName1()
        {
            var book = new Book
            {
                Name = "a"
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid length name2.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthName2()
        {
            var book = new Book
            {
                Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero. "
            };

            var wasCreated = BookService.CreateBook(book);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add null categories.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add empty categories.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add more categories than DOM.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add related one level inheritance.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add related two level inheritance.
        /// </summary>
        [Test]
        public void TestAddRelatedTwoLevelInheritance()
        {
            var field1 = new Field { Name = "Art" };
            var field2 = new Field { Name = "Painting", ParentField = field1 };
            var field3 = new Field { Name = "Colours", ParentField = field2 };

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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add null authors.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add empty authors.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add multiple authors.
        /// </summary>
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
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid book.
        /// </summary>
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

            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
