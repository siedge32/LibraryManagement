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

            if (!this.CheckNumberOfFieldsInLastGivenMonths(bookPublications, reader))
            {
                Logger.LogError("The number of categories of the rented books is invalid", MethodBase.GetCurrentMethod());
                return false;
            }

            if (!this.CheckSameBookRentedDelta(bookPublications, reader))
            {
                Logger.LogError("One of the books has been rented more", MethodBase.GetCurrentMethod());
                return false;
            }

            if (!this.CheckNumberOfBooksInOneDay(bookPublications, reader))
            {
                Logger.LogError("Can't rent so much books in one day", MethodBase.GetCurrentMethod());
                return false;
            }

            return true;
        }

        public bool CheckNumberOfBooksInOneDay(List<BookPublication> bookPublications, Reader reader)
        {
            var ncz = int.Parse(ConfigurationManager.AppSettings["NCZ"]);

            var bookPublicationsInPeriodRented = this.GetBookPublicationsInPeriod(reader, 1).Select(b => b.Book).ToList();
            if (bookPublicationsInPeriodRented.Count + bookPublications.Count > ncz)
            {
                return false;
            }

            return true;
        }

        public bool CheckSameBookRentedDelta(List<BookPublication> bookPublications, Reader reader)
        {
            var delta = int.Parse(ConfigurationManager.AppSettings["DELTA"]);

            var bookPublicationsInPeriodRented = this.GetBookPublicationsInPeriod(reader, delta).Select(b => b.Book).Distinct().ToList();

            foreach (var bookPublication in bookPublications)
            {
                if (bookPublicationsInPeriodRented.FirstOrDefault(bpr => bpr.Name.Equals(bookPublication.Book.Name)) != null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Checks the number of fields in last given months.</summary>
        /// <param name="bookPublications">The book publications.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool CheckNumberOfFieldsInLastGivenMonths(List<BookPublication> bookPublications, Reader reader)
        {
            var d = int.Parse(ConfigurationManager.AppSettings["D"]);
            var l = int.Parse(ConfigurationManager.AppSettings["L"]);

            var books = bookPublications.Select(bookPublication => bookPublication.Book).ToList();
            var bookPublicationsInPeriod = this.GetBookPublicationsInPeriod(reader, l * 30).Select(b => b.Book).ToList();
            var allBooks = books.Union(bookPublicationsInPeriod).ToList();

            var distinctFields = allBooks.SelectMany(b => b.Categories).Select(f => this.GetParentField(f)).ToList();
            var groupedFields = distinctFields.GroupBy(dc => dc.Id).ToList();

            foreach (var groupedField in groupedFields)
            {
                var numberOfBooks = groupedField.Count();
                if (numberOfBooks > d)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>Gets the parent field.</summary>
        /// <param name="field">The field.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public Field GetParentField(Field field)
        {
            while (field.ParentField != null)
            {
                field = field.ParentField;
            }

            return field;
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
                if (this.GetDistinctiveCategoriesForBooks(books).Count < 2)
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
        public List<Field> GetDistinctiveCategoriesForBooks(List<Book> books)
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
                return new List<Field>();
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

            return distinctiveFields;
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

            var bookPublicationsRentedInPeriod = this.GetBookPublicationsInPeriod(reader, per);
            var numberOfBooksRentedInPeriod = bookPublicationsRentedInPeriod.Count;

            if (numberOfBooksRentedInPeriod + bookPublications.Count > nmc)
            {
                return false;
            }

            return true;
        }

        /// <summary>Gets the book publications in period.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="period">The period.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public List<BookPublication> GetBookPublicationsInPeriod(Reader reader, int period)
        {
            var bookPublicationsRentedInPeriod = new List<BookPublication>();

            if (reader.BookWithdrawals == null)
            {
                return bookPublicationsRentedInPeriod;
            }

            foreach (var bookWithdrawal in reader.BookWithdrawals)
            {
                if ((bookWithdrawal.DateToReturn - bookWithdrawal.DateRented).TotalDays <= period)
                {
                    foreach (var bookPublication in bookWithdrawal.BookPublications)
                    {
                        bookPublicationsRentedInPeriod.Add(bookPublication);
                    }
                }
            }

            return bookPublicationsRentedInPeriod;
        }

        /// <summary>Adds the extension.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="bookWithdrawal">The book withdrawal.</param>
        /// <param name="dateWasMade">The date was made.</param>
        /// <param name="dateToReturn">The date to return.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public bool AddExtension(Reader reader, BookWithdrawal bookWithdrawal, DateTime dateWasMade, DateTime dateToReturn)
        {
            if (reader == null || bookWithdrawal == null)
            {
                Logger.LogError("BookWithdrawal or Reader is null", MethodBase.GetCurrentMethod());
                return false;
            }

            if (dateWasMade > dateToReturn)
            {
                Logger.LogError("DateWasMade bigger than dateToReturn", MethodBase.GetCurrentMethod());
                return false;
            }

            if (!this.CanAddExtension(reader, dateWasMade))
            {
                Logger.LogError("Can't add extension", MethodBase.GetCurrentMethod());
                return false;
            }

            var extension = new Extension { BookWithdrawal = bookWithdrawal, DateExtensionWasMade = dateWasMade, DateToReturn = dateToReturn };
            bookWithdrawal.Extensions.Add(extension);

            return true;
        }

        /// <summary>Determines whether this instance [can add extension] the specified reader.</summary>
        /// <param name="reader">The reader.</param>
        /// <param name="date">The date.</param>
        /// <returns>
        /// <c>true</c> if this instance [can add extension] the specified reader; otherwise, <c>false</c>.</returns>
        private bool CanAddExtension(Reader reader, DateTime date)
        {
            var lim = int.Parse(ConfigurationManager.AppSettings["LIM"]);

            var allExtension = reader.BookWithdrawals.SelectMany(b => b.Extensions).ToList();
            var extensionWithin90Days = allExtension.Count(ext => (date - ext.DateExtensionWasMade).TotalDays < 90);

            if (extensionWithin90Days > lim)
            {
                return false;
            }

            return true;
        }
    }
}
