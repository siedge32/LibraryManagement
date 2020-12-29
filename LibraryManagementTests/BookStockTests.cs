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
    public class BookStockTests
    {
        private BookStock bookStock;

        [SetUp]
        public void SetUp()
        {
            bookStock = new BookStock
            {
                Id = 1,
                NumberOfBooks = 20,
                NumberOfBooksForLecture = 15,
                BookPublication = new BookPublication()
            };
        }

        [Test]
        public void TestBookStockInvalidNumberOfBooks()
        {
            bookStock.NumberOfBooks = -1;
            var ruleSet = "BookStockRangeValidator";
            var result = ValidationUtil.ValidateEntity(ruleSet, bookStock);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookStockInvalidNumberOfBooksForLecture()
        {
            bookStock.NumberOfBooksForLecture = -1;
            var ruleSet = "BookStockRangeValidator";
            var result = ValidationUtil.ValidateEntity(ruleSet, bookStock);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookStockInvalidSelfValidation()
        {
            bookStock.NumberOfBooks = 10;
            bookStock.NumberOfBooksForLecture = 20;
            var result = ValidationUtil.ValidateEntity(string.Empty, bookStock);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestBookStockValid()
        {
            var ruleSets = new string[] { "BookStockRangeValidator", string.Empty };
            var isValid = true;
            foreach (var ruleset in ruleSets)
            {
                var result = ValidationUtil.ValidateEntity(ruleset, bookStock);
                if (!result.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsTrue(isValid);
        }

        [Test]
        public void TestGetBookPublication()
        {
            var bP = bookStock.BookPublication;
            Assert.IsTrue(bP != null);
        }

        [Test]
        public void TestGetId()
        {
            Assert.IsTrue(bookStock.Id == 1);
        }
    }
}
