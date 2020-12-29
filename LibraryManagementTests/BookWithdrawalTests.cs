using LibraryManagement.DomainModel;
using LibraryManagement.Utils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementTests
{
    [TestFixture]
    public class BookWithdrawalTests
    {
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
                BookWithdrawals = new List<BookWithdrawal> { bookWithdrawal },
                CoverMaterial = "Carton",
                NumberOfPages = 30
            };

            reader = new Reader
            {
                Id = 1,
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            librarian = new Librarian
            {
                Id = 1,
                LastName = "Matei",
                FirstName = "Popescu",
                Address = "Brasov",
                Phone = "12345678",
                Email = "smth@smth.com",
                Gender = "M"
            };

            bookWithdrawal = new BookWithdrawal
            {
                Id = 1,
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                BookPublications = new List<BookPublication> { bookPublication },
                Extensions = new List<Extension>(),
                Librarian = librarian,
                Reader = reader
            };

        }

        private BookWithdrawal bookWithdrawal;

        private Reader reader;

        private Librarian librarian;


        [Test]
        public void TestBookWithdrawalNullReader()
        {
            bookWithdrawal.Reader = null;
            var ruleSet = "BookWithdrawalFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookWithdrawalNullLibrarian()
        {
            bookWithdrawal.Librarian = null;
            var ruleSet = "BookWithdrawalFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookWithdrawalNullBookPublications()
        {
            bookWithdrawal.BookPublications = null;
            var ruleSet = string.Empty;
            var result = ValidationUtil.ValidateEntity(ruleSet, bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookWithdrawalEmptyBookPublications()
        {
            bookWithdrawal.BookPublications = new List<BookPublication>();
            var ruleSet = string.Empty;
            var result = ValidationUtil.ValidateEntity(ruleSet, bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookWithdrawalFutureDateRented()
        {
            bookWithdrawal.DateRented = new DateTime(2020, 2, 1);
            bookWithdrawal.DateToReturn = new DateTime(2020, 1, 1);
            var ruleSet = string.Empty;
            var result = ValidationUtil.ValidateEntity(ruleSet, bookWithdrawal);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookWithdrawalValid()
        {
            var ruleSets = new string[] { "BookWithdrawalFieldNotNull", string.Empty };
            var isValid = true;
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, bookWithdrawal);
                if (!results.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsTrue(isValid);
        }
    }
}
