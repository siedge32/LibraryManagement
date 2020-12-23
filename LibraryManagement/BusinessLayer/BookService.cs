// <copyright file="BookService.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.BusinessLayer
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;

    /// <summary>
    /// The BookService class
    /// </summary>
    public class BookService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookService"/> class.
        /// </summary>
        /// <param name="bookRepository">The book repository.</param>
        public BookService(BookRepository bookRepository)
        {
            BookRepository = bookRepository;
            Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the book repository.
        /// </summary>
        /// <value>
        /// The book repository.
        /// </value>
        private BookRepository BookRepository { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Creates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>True if the book is created, false else</returns>
        public bool CreateBook(Book book)
        {
            if (book == null)
            {
                Logger.LogError("Book is null", MethodBase.GetCurrentMethod());
                return false;
            }

            var ruleSets = new string[] { "BookFieldNotNull", "BookNameStringLength", string.Empty };

            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, book);
                if (!results.IsValid)
                {
                    foreach (var result in results)
                    {
                        Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                    }

                    return false;
                }
            }

            BookRepository.Create(book);
            return true;
        }

        /// <summary>
        /// Finds the name of the book by.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A book found by it's name</returns>
        public Book FindBookByName(string name)
        {
            return BookRepository.FindByCondition(book => book.Name == name).FirstOrDefault();
        }

        /// <summary>
        /// Finds all books.
        /// </summary>
        /// <returns>All the book</returns>
        public List<Book> FindAllBooks()
        {
            return BookRepository.FindAll().ToList();
        }
    }
}
