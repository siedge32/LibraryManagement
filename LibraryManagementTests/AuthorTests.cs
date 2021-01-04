// <copyright file="AuthorTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;
    using NUnit.Framework;

    /// <summary>
    /// The AuthorTests class
    /// </summary>
    [TestFixture]
    public class AuthorTests
    {
        /// <summary>
        /// The author
        /// </summary>
        private Author author;

        /// <summary>
        /// The rule sets
        /// </summary>
        private string[] ruleSets = new string[] { "AuthorFieldNotNull", "AuthorNameStringLength", "AuthorNameRegex", "AuthorInvalidGender" };

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Adress = "Brasov",
                Gender = "M"
            };
        }

        /// <summary>
        /// Tests the first name of the invalid null.
        /// </summary>
        [Test]
        public void TestInvalidNullFirstName()
        {
            this.author.FirstName = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the name of the invalid null last.
        /// </summary>
        [Test]
        public void TestInvalidNullLastName()
        {
            this.author.LastName = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid null address.
        /// </summary>
        [Test]
        public void TestInvalidNullAddress()
        {
            this.author.Adress = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid gender.
        /// </summary>
        [Test]
        public void TestNullGender()
        {
            this.author.Gender = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid gender.
        /// </summary>
        [Test]
        public void TestInvalidGender()
        {
            this.author.Gender = "asdf";
            var ruleSet = "AuthorInvalidGender";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the valid gender upper f.
        /// </summary>
        [Test]
        public void TestValidGenderUpperF()
        {
            this.author.Gender = "F";
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the valid gender lower m.
        /// </summary>
        [Test]
        public void TestValidGenderLowerM()
        {
            this.author.Gender = "m";
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the valid gender lower f.
        /// </summary>
        [Test]
        public void TestValidGenderLowerF()
        {
            this.author.Gender = "f";
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the first name of the invalid string length.
        /// </summary>
        [Test]
        public void TestInvalidStringLengthFirstName()
        {
            this.author.FirstName = "A";
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the first name of the invalid string length empty.
        /// </summary>
        [Test]
        public void TestInvalidStringLengthEmptyFirstName()
        {
            this.author.FirstName = string.Empty;
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the last name of the invalid string length.
        /// </summary>
        [Test]
        public void TestInvalidStringLengthLastName()
        {
            this.author.LastName = "A";
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the last name of the invalid string length empty.
        /// </summary>
        [Test]
        public void TestInvalidStringLengthEmptyLastName()
        {
            this.author.LastName = string.Empty;
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid string length address.
        /// </summary>
        [Test]
        public void TestInvalidStringLengthAddress()
        {
            this.author.Adress = "A";
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid string length empty address.
        /// </summary>
        [Test]
        public void TestInvalidStringLengthEmptyAddress()
        {
            this.author.Adress = string.Empty;
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the first name of the invalid regex characters.
        /// </summary>
        [Test]
        public void TestInvalidRegexCharactersFirstNameDigits()
        {
            this.author.FirstName = "bogdan1234";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid regex characters first name symbols.
        /// </summary>
        [Test]
        public void TestInvalidRegexCharactersFirstNameSymbols()
        {
            this.author.FirstName = "bogdan~!@#`";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the last name of the invalid regex characters.
        /// </summary>
        [Test]
        public void TestInvalidRegexCharactersLastNameDigits()
        {
            this.author.LastName = "bogdan1234";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the invalid regex characters last name symbols.
        /// </summary>
        [Test]
        public void TestInvalidRegexCharactersLastNameSymbols()
        {
            this.author.LastName = "bogdan~!@#`";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the first name of the invalid regex upper case.
        /// </summary>
        [Test]
        public void TestInvalidRegexUpperCaseFirstName()
        {
            this.author.FirstName = "bogdan";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the last name of the invalid regex upper case.
        /// </summary>
        [Test]
        public void TestInvalidRegexUpperCaseLastName()
        {
            this.author.LastName = "hanganu";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, this.author);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the valid author.
        /// </summary>
        [Test]
        public void TestValidAuthor()
        {
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author compound first name space.
        /// </summary>
        [Test]
        public void TestValidAuthorCompoundFirstNameSpace()
        {
            this.author.FirstName = "Maria Ioana";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author compound first name dash.
        /// </summary>
        [Test]
        public void TestValidAuthorCompoundFirstNameDash()
        {
            this.author.FirstName = "Maria-Ioana";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author compound last name space.
        /// </summary>
        [Test]
        public void TestValidAuthorCompoundLastNameSpace()
        {
            this.author.LastName = "Popescu Ionescu";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author compound last name dash.
        /// </summary>
        [Test]
        public void TestValidAuthorCompoundLastNameDash()
        {
            this.author.FirstName = "Popescu-Ionescu";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author dash address.
        /// </summary>
        [Test]
        public void TestValidAuthorDashAddress()
        {
            this.author.Adress = "Prunului-Visinului";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author dash space address.
        /// </summary>
        [Test]
        public void TestValidAuthorDashSpaceAddress()
        {
            this.author.Adress = "Strada Prunului-Visinului";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author short dash space address.
        /// </summary>
        [Test]
        public void TestValidAuthorShortDashSpaceAddress()
        {
            this.author.Adress = "Str. Prunului-Visinului";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the valid author short dash space address number.
        /// </summary>
        [Test]
        public void TestValidAuthorShortDashSpaceAddressNumber()
        {
            this.author.Adress = "Str. Prunului-Visinului Nr. 14";
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the null author.
        /// </summary>
        [Test]
        public void TestNullAuthor()
        {
            this.author = null;
            var isValid = this.IsValidAuthor(this.ruleSets, this.author);

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Tests the not null author.
        /// </summary>
        [Test]
        public void TestNotNullAuthor()
        {
            Assert.IsNotNull(new Author { });
        }

        /// <summary>
        /// Determines whether [is valid author] [the specified rule sets].
        /// </summary>
        /// <param name="ruleSets">The rule sets.</param>
        /// <param name="author">The author.</param>
        /// <returns>
        ///   <c>true</c> if [is valid author] [the specified rule sets]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsValidAuthor(string[] ruleSets, Author author)
        {
            foreach (var ruleset in ruleSets)
            {
                var result = ValidationUtil.ValidateEntity(ruleset, author);
                if (!result.IsValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
