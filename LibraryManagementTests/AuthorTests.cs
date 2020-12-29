using LibraryManagement.DomainModel;
using LibraryManagement.Utils;
using NUnit.Framework;

namespace LibraryManagementTests
{
    [TestFixture]
    public class AuthorTests
    {

        private Author author;

        [SetUp]
        public void SetUp()
        {
            author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Adress = "Brasov",
                Gender = "M"
            };
        }

        [Test]
        public void TestInvalidNullFirstName()
        {
            author.FirstName = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidNullLasttName()
        {
            author.LastName = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidNullAdress()
        {
            author.Adress = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidGender()
        {
            author.Gender = null;
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidStringLengthFirstName()
        {
            author.FirstName = "A";
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidStringLengthLastName()
        {
            author.LastName = "A";
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidStringLengthAdress()
        {
            author.Adress = "A";
            var ruleSet = "AuthorNameStringLength";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidRegexCharactersFirstName()
        {
            author.FirstName = "1234";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidRegexCharactersLastName()
        {
            author.LastName = "1234";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidRegexUpperCaseFirstName()
        {
            author.FirstName = "bogdan";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidRegexUpperCaseLastName()
        {
            author.LastName = "hanganu";
            var ruleSet = "AuthorNameRegex";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestInvalidGenderUnknown()
        {
            author.Gender = "Helicopter";
            var ruleSet = "AuthorInvalidGender";
            var result = ValidationUtil.ValidateEntity(ruleSet, author);
            Assert.IsFalse(result.IsValid);
        }

        [Test]
        public void TestValidAuthor()
        {
            var ruleSets = new string[] { "AuthorFieldNotNull", "AuthorNameStringLength", "AuthorNameRegex", "AuthorInvalidGender" };
            var isValid = true;
            foreach (var ruleset in ruleSets)
            {
                var result = ValidationUtil.ValidateEntity(ruleset, author);
                if (!result.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsTrue(isValid);
        }

        [Test]
        public void TestNullAuthor()
        {
            author = null;
            var ruleSets = new string[] { "AuthorFieldNotNull", "AuthorNameStringLength", "AuthorNameRegex", "AuthorInvalidGender" };
            var isValid = true;
            foreach (var ruleset in ruleSets)
            {
                var result = ValidationUtil.ValidateEntity(ruleset, author);
                if (!result.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsFalse(isValid);
        }

        [Test]
        public void TestNotNullAuthor()
        {
            Assert.IsNotNull(new Author { });
        }
    }
}
