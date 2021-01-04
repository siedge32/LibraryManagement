// <copyright file="BookPublicationService.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.BusinessLayer
{
    using System.Reflection;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;

    /// <summary>
    /// The BookPublicationService class.
    /// </summary>
    public class BookPublicationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookPublicationService"/> class.
        /// </summary>
        /// <param name="bookPublicationRepository">The book publication repository.</param>
        public BookPublicationService(BookPublicationRepository bookPublicationRepository)
        {
            BookPublicationRepository = bookPublicationRepository;
            Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the book publication repository.
        /// </summary>
        /// <value>
        /// The book publication repository.
        /// </value>
        private BookPublicationRepository BookPublicationRepository { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Creates the book publication.
        /// </summary>
        /// <param name="bookPublication">The book publication.</param>
        /// <returns>Returns true if create, false else</returns>
        public bool CreateBookPublication(BookPublication bookPublication)
        {
            var isBookValid = this.ValidateBookPublication(bookPublication);
            if (isBookValid)
            {
                BookPublicationRepository.Create(bookPublication);
            }

            return isBookValid;
        }

        /// <summary>Validates the book publication.</summary>
        /// <param name="bookPublication">The book publication.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool ValidateBookPublication(BookPublication bookPublication)
        {
            if (bookPublication == null)
            {
                Logger.LogError("BookPublication is null", MethodBase.GetCurrentMethod());
                return false;
            }

            var ruleSets = new string[] { "BookPublicationNumberOfPagesFieldRange", "BookPublicationFieldNotNull", "BookPublicationCoverStringLength", "BookPublicationNameRegex", string.Empty };
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, bookPublication);
                if (!results.IsValid)
                {
                    foreach (var result in results)
                    {
                        Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                    }

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether this instance [can rent book stock amount] the specified book publication.
        /// </summary>
        /// <param name="bookPublication">The book publication.</param>
        /// <returns>
        ///   <c>true</c> if this instance [can rent book stock amount] the specified book publication; otherwise, <c>false</c>.
        /// </returns>
        public bool CanRentBookStockAmount(BookPublication bookPublication)
        {
            var bookStock = bookPublication.BookStock;
            var ruleSets = new string[] { "BookStockRangeValidator", "BookStockFieldNotNull", string.Empty };
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, bookStock);
                if (!results.IsValid)
                {
                    foreach (var result in results)
                    {
                        Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                    }

                    return false;
                }
            }

            if (bookStock.NumberOfBooks == bookStock.NumberOfBooksForLecture)
            {
                return false;
            }

            double leftovers = bookStock.NumberOfBooks - bookStock.NumberOfBooksForLecture - 1;
            double percent = leftovers / bookStock.NumberOfBooks;
            return percent >= 0.1;
        }
    }
}
