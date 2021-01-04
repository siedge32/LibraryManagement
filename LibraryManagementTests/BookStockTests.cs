// <copyright file="BookStockTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;
    using NUnit.Framework;

    /// <summary>
    /// The BookStockTests class
    /// </summary>
    [TestFixture]
    public class BookStockTests
    {
        /// <summary>
        /// The book stock
        /// </summary>
        private BookStock bookStock;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.bookStock = new BookStock
            {
                Id = 1,
                NumberOfBooks = 20,
                NumberOfBooksForLecture = 15,
                BookPublication = new BookPublication()
            };
        }

        /// <summary>
        /// Tests the book stock invalid number of books.
        /// </summary>
        [Test]
        public void TestBookStockInvalidNumberOfBooks()
        {
            this.bookStock.NumberOfBooks = -1;
            var ruleSet = "BookStockRangeValidator";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookStock);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book stock invalid number of books for lecture.
        /// </summary>
        [Test]
        public void TestBookStockInvalidNumberOfBooksForLecture()
        {
            this.bookStock.NumberOfBooksForLecture = -1;
            var ruleSet = "BookStockRangeValidator";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookStock);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book stock null book publication.
        /// </summary>
        [Test]
        public void TestBookStockNullBookPublication()
        {
            this.bookStock.BookPublication = null;
            var ruleSet = "BookStockFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.bookStock);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book stock invalid self validation.
        /// </summary>
        [Test]
        public void TestBookStockInvalidSelfValidation()
        {
            this.bookStock.NumberOfBooks = 10;
            this.bookStock.NumberOfBooksForLecture = 20;
            var result = ValidationUtil.ValidateEntity(string.Empty, this.bookStock);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the book stock valid.
        /// </summary>
        [Test]
        public void TestBookStockValid()
        {
            var ruleSets = new string[] { "BookStockRangeValidator", string.Empty };
            var isValid = true;
            foreach (var ruleset in ruleSets)
            {
                var result = ValidationUtil.ValidateEntity(ruleset, this.bookStock);
                if (!result.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the get book publication.
        /// </summary>
        [Test]
        public void TestGetBookPublication()
        {
            var bP = this.bookStock.BookPublication;
            Assert.IsTrue(bP != null);
        }

        /// <summary>
        /// Tests the get identifier.
        /// </summary>
        [Test]
        public void TestGetId()
        {
            Assert.IsTrue(this.bookStock.Id == 1);
        }
    }
}
