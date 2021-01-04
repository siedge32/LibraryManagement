// <copyright file="BookWithdrawalTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System;
    using System.Collections.Generic;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;
    using NUnit.Framework;

    /// <summary>
    /// The BookWithdrawalTests class
    /// </summary>
    [TestFixture]
    public class BookWithdrawalTests
    {
        /// <summary>
        /// The book withdrawal
        /// </summary>
        private BookWithdrawal bookWithdrawal;

        /// <summary>
        /// The reader
        /// </summary>
        private Reader reader;

        /// <summary>
        /// The librarian
        /// </summary>
        private Librarian librarian;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var book = new Book
            {
                Id = 1,
                Authors = new List<Author>(),
                Categories = new List<Field>
                {
                    new Field
                    {
                        Id = 1,
                        Name = "Romance"
                    }
                }
            };

            var bookStock = new BookStock
            {
                Id = 1,
                NumberOfBooks = 10,
                NumberOfBooksForLecture = 5
            };

            var bookPublication = new BookPublication
            {
                Id = 1,
                Book = book,
                BookStock = bookStock,
                BookWithdrawals = new List<BookWithdrawal> { this.bookWithdrawal },
                CoverMaterial = "Carton",
                NumberOfPages = 30
            };

            this.reader = new Reader
            {
                Id = 1,
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            this.librarian = new Librarian
            {
                Id = 1,
                LastName = "Matei",
                FirstName = "Popescu",
                Address = "Brasov",
                Phone = "12345678",
                Email = "smth@smth.com",
                Gender = "M"
            };

            this.bookWithdrawal = new BookWithdrawal
            {
                Id = 1,
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                BookPublications = new List<BookPublication> { bookPublication },
                Extensions = new List<Extension>(),
                Librarian = this.librarian,
                Reader = this.reader
            };
        }

        /// <summary>
        /// Tests the book withdrawal null reader.
        /// </summary>
        [Test]
        public void TestBookWithdrawalNullReader()
        {
            this.bookWithdrawal.Reader = null;
            var ruleSet = "BookWithdrawalFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book withdrawal null librarian.
        /// </summary>
        [Test]
        public void TestBookWithdrawalNullLibrarian()
        {
            this.bookWithdrawal.Librarian = null;
            var ruleSet = "BookWithdrawalFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book withdrawal null book publications.
        /// </summary>
        [Test]
        public void TestBookWithdrawalNullBookPublications()
        {
            this.bookWithdrawal.BookPublications = null;
            var ruleSet = string.Empty;
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book withdrawal empty book publications.
        /// </summary>
        [Test]
        public void TestBookWithdrawalEmptyBookPublications()
        {
            this.bookWithdrawal.BookPublications = new List<BookPublication>();
            var ruleSet = string.Empty;
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book withdrawal future date rented.
        /// </summary>
        [Test]
        public void TestBookWithdrawalFutureDateRented()
        {
            this.bookWithdrawal.DateRented = new DateTime(2020, 2, 1);
            this.bookWithdrawal.DateToReturn = new DateTime(2020, 1, 1);
            var ruleSet = string.Empty;
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book withdrawal valid.
        /// </summary>
        [Test]
        public void TestBookWithdrawalValid()
        {
            var ruleSets = new string[] { "BookWithdrawalFieldNotNull", string.Empty };
            var isValid = true;
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
                if (!results.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the book withdrawal null.
        /// </summary>
        [Test]
        public void TestBookWithdrawalNull()
        {
            this.bookWithdrawal = null;
            var ruleSets = new string[] { "BookWithdrawalFieldNotNull", string.Empty };
            var isValid = true;
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, this.bookWithdrawal);
                if (!results.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsFalse(isValid);
        }
    }
}
