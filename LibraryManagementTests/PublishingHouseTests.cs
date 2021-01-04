// <copyright file="PublishingHouseTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System.Collections.Generic;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;
    using NUnit.Framework;

    /// <summary>
    /// The PublishingHouseTests class
    /// </summary>
    [TestFixture]
    public class PublishingHouseTests
    {
        /// <summary>
        /// The publishing house
        /// </summary>
        private PublishingHouse publishingHouse;

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            this.publishingHouse = new PublishingHouse
            {
                Id = 1,
                Adress = "Brasov",
                Name = "Corint",
                PublishedBooks = new List<BookPublication>()
            };
        }

        /// <summary>
        /// Tests the null publishing house.
        /// </summary>
        [Test]
        public void TestNullPublishingHouse()
        {
            this.publishingHouse = null;
            var result = ValidationUtil.ValidateEntity("PublishingHouseFieldNotNull", this.publishingHouse);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the null address publishing house.
        /// </summary>
        [Test]
        public void TestNullAdressPublishingHouse()
        {
            this.publishingHouse.Adress = null;
            var result = ValidationUtil.ValidateEntity("PublishingHouseFieldNotNull", this.publishingHouse);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the null name publishing house.
        /// </summary>
        [Test]
        public void TestNullNamePublishingHouse()
        {
            this.publishingHouse.Name = null;
            var result = ValidationUtil.ValidateEntity("PublishingHouseFieldNotNull", this.publishingHouse);
            Assert.IsFalse(result.IsValid);
        }

        /// <summary>
        /// Tests the valid publishing house.
        /// </summary>
        [Test]
        public void TestValidPublishingHouse()
        {
            var result = ValidationUtil.ValidateEntity("PublishingHouseFieldNotNull", this.publishingHouse);
            Assert.IsTrue(result.IsValid);
        }
    }
}
