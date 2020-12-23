// <copyright file="ReaderService.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.BusinessLayer
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Reflection;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;

    /// <summary>
    /// The ReaderService class
    /// </summary>
    public class ReaderService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderService"/> class.
        /// </summary>
        /// <param name="readerRepository">The reader repository.</param>
        public ReaderService(ReaderRepository readerRepository)
        {
            ReaderRepository = readerRepository;
            Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the reader repository.
        /// </summary>
        /// <value>
        /// The reader repository.
        /// </value>
        private ReaderRepository ReaderRepository { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Adds the reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>True if the reader is created, false else</returns>
        public bool AddReader(Reader reader)
        {
            if (reader == null)
            {
                Logger.LogError("Reader is null", MethodBase.GetCurrentMethod());
                return false;
            }

            var ruleSets = new string[] { "ReaderFieldNotNull", "ReaderNameStringLength", "ReaderNameRegex", "ReaderInvalidGender", string.Empty };

            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, reader);
                if (!results.IsValid)
                {
                    foreach (var result in results)
                    {
                        Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                    }

                    return false;
                }
            }

            ReaderRepository.Create(reader);
            return true;
        }

        /// <summary>Determines whether this instance [can rent books] the specified book publications.</summary>
        /// <param name="bookPublications">The book publications.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>
        /// <c>true</c> if this instance [can rent books] the specified book publications; otherwise, <c>false</c>.</returns>
        public bool CanRentBooks(List<BookPublication> bookPublications, Reader reader)
        {
            if (bookPublications == null || reader == null || bookPublications.Count == 0)
            {
                Logger.LogError("BookPublications or Reader is null", MethodBase.GetCurrentMethod());
                return false;
            }

            if (!this.CheckNumberOfBooksInPeriod(bookPublications, reader))
            {
                Logger.LogError("The number of books on the given period is too great", MethodBase.GetCurrentMethod());
                return false;
            }

            if (!this.CheckNumberOfBooks(bookPublications))
            {
                Logger.LogError("The number of distinctive categories is invalid", MethodBase.GetCurrentMethod());
                return false;
            }

            return true;
        }

        /// <summary>Checks the number of books.</summary>
        /// <param name="bookPublications">The book publications.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool CheckNumberOfBooks(List<BookPublication> bookPublications)
        {
            var c = int.Parse(ConfigurationManager.AppSettings["C"]);
            if (bookPublications.Count > c)
            {
                return false;
            }

            if (bookPublications.Count >= 3)
            {
                var books = new List<Book>();
                bookPublications.ForEach(bookPublication => books.Add(bookPublication.Book));
                if (this.NumberOfDistinctiveCategoriesForBooks(books) < 2)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Numbers the of distinctive categories.</summary>
        /// <param name="books">The books.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public int NumberOfDistinctiveCategoriesForBooks(List<Book> books)
        {
            var allCategoriesFromBooks = new List<Field>();
            foreach (var book in books)
            {
                foreach (var field in book.Categories)
                {
                    allCategoriesFromBooks.Add(field);
                }
            }

            var distinctiveFields = new List<Field>();
            if (allCategoriesFromBooks.Count == 0)
            {
                return 0;
            }

            foreach (var field in allCategoriesFromBooks)
            {
                if (distinctiveFields.Count == 0)
                {
                    distinctiveFields.Add(field);
                    continue;
                }

                var restOfFields = allCategoriesFromBooks.Select(f => f).ToList();
                restOfFields.Remove(field);

                if (!ValidationUtil.CheckInheritanceFields(field, restOfFields))
                {
                    distinctiveFields.Add(field);
                }
            }

            return distinctiveFields.Count;
        }

        /// <summary>Checks the number of books in period.</summary>
        /// <param name="bookPublications">The book publications.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool CheckNumberOfBooksInPeriod(List<BookPublication> bookPublications, Reader reader)
        {
            var nmc = int.Parse(ConfigurationManager.AppSettings["NMC"]);
            var per = int.Parse(ConfigurationManager.AppSettings["PER"]);

            var numberOfBooksRentedInPeriod = 0;
            foreach (var bookWithdrawal in reader.BookWithdrawals)
            {
                if ((bookWithdrawal.DateToReturn - bookWithdrawal.DateRented).TotalDays > per)
                {
                    numberOfBooksRentedInPeriod += bookWithdrawal.BookPublications.Count;
                }
            }

            if (numberOfBooksRentedInPeriod + bookPublications.Count > nmc)
            {
                return false;
            }

            return true;
        }
    }
}
