// <copyright file="ExtensionTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;
    using NUnit.Framework;

    /// <summary>
    /// The ExtensionTests class
    /// </summary>
    [TestFixture]
    public class ExtensionTests
    {
        /// <summary>
        /// The extension
        /// </summary>
        private Extension extension;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.extension = new Extension
            {
                Id = 1,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 2),
                BookWithdrawal = new BookWithdrawal()
            };
        }

        /// <summary>
        /// Tests the null extension.
        /// </summary>
        [Test]
        public void TestNullExtension()
        {
            this.extension = null;

            var ruleSets = new string[] { "ExtensionFieldNotNull", string.Empty };
            var isValid = true;
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, this.extension);
                if (!results.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Tests the valid extension.
        /// </summary>
        [Test]
        public void TestValidExtension()
        {
            var ruleSets = new string[] { "ExtensionFieldNotNull", string.Empty };
            var isValid = true;
            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, this.extension);
                if (!results.IsValid)
                {
                    isValid = false;
                    break;
                }
            }

            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the invalid dates.
        /// </summary>
        [Test]
        public void TestInvalidDates()
        {
            this.extension.DateToReturn = new DateTime(2019, 1, 1);
            var results = ValidationUtil.ValidateEntity(string.Empty, this.extension);

            Assert.IsFalse(results.IsValid);
        }
    }
}
