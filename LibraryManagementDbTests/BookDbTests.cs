// <copyright file="BookDbTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementDbTests
{
    using System.Collections.Generic;
    using System.Linq;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using NUnit.Framework;

    /// <summary>
    /// The Book Database Tests class
    /// </summary>
    [TestFixture]
    public class BookDbTests
    {
        /// <summary>
        /// The library context
        /// </summary>
        private LibraryDbContext libraryContext;

        /// <summary>
        /// Gets or sets the book service.
        /// </summary>
        /// <value>
        /// The book service.
        /// </value>
        private BookService BookService { get; set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.libraryContext = new LibraryDbContext();
            this.BookService = new BookService(new BookRepository(this.libraryContext));
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        [TearDown]
        public void Cleanup()
        {
            this.libraryContext.Database.Delete();
        }

        /// <summary>
        /// Tests the book database add book.
        /// </summary>
        [Test]
        public void TestBookDbAddBook()
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
            Assert.True(this.libraryContext.Books.Count() == 1);
        }

        /// <summary>
        /// Tests the book database find all.
        /// </summary>
        [Test]
        public void TestBookDbFindAll()
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

            BookService.CreateBook(book);
            BookService.CreateBook(book);
            var allBooks = BookService.FindAllBooks().ToList();
            Assert.True(allBooks.Count == 2);
        }

        /// <summary>
        /// Tests the book database find by condition.
        /// </summary>
        [Test]
        public void TestBookDbFindByCondition()
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

            BookService.CreateBook(book);
            var bookFound = BookService.FindBookByName("Something");
            Assert.AreEqual(bookFound.Name, book.Name);
        }
    }
}
