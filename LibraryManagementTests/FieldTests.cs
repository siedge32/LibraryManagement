// <copyright file="FieldTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System.Data.Entity;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The field tests
    /// </summary>
    [TestFixture]
    public class FieldTests
    {
        /// <summary>
        /// Gets the library context mock.
        /// </summary>
        /// <value>
        /// The library context mock.
        /// </value>
        public Mock<LibraryDbContext> LibraryContextMock { get; private set; }

        /// <summary>
        /// Gets the field service.
        /// </summary>
        /// <value>
        /// The field service.
        /// </value>
        public FieldService FieldService { get; private set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var mockSet = new Mock<DbSet<Field>>();
            this.LibraryContextMock = new Mock<LibraryDbContext>();
            this.LibraryContextMock.Setup(m => m.Set<Field>()).Returns(mockSet.Object);
            this.FieldService = new FieldService(new FieldRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Adds the null field.
        /// </summary>
        [Test]
        public void AddNullField()
        {
            Field field = null;

            var wasCreated = this.FieldService.AddField(field);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Adds the name of the null.
        /// </summary>
        [Test]
        public void AddNullName()
        {
            var field = new Field
            {
                Name = null
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid length name0.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthName0()
        {
            var field = new Field
            {
                Name = string.Empty
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid length name1.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthName1()
        {
            var field = new Field
            {
                Name = "s"
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid length name2.
        /// </summary>
        [Test]
        public void TestAddInvalidLengthName2()
        {
            var field = new Field
            {
                Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero. "
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the check field inheritance null field.
        /// </summary>
        [Test]
        public void TestCheckFieldInheritanceNullField()
        {
            Field field1 = null;

            var field2 = new Field
            {
                Name = "Maths",
            };

            var areRelated = FieldService.CheckFieldInheritance(field1, field2);
            Assert.False(areRelated);
        }

        /// <summary>
        /// Tests the check field inheritance not related.
        /// </summary>
        [Test]
        public void TestCheckFieldInheritanceNotRelated()
        {
            var field1 = new Field
            {
                Name = "Biology",
            };

            var field2 = new Field
            {
                Name = "Maths",
            };

            var areRelated = FieldService.CheckFieldInheritance(field1, field2);
            Assert.False(areRelated);
        }

        /// <summary>
        /// Tests the check field inheritance first grade related.
        /// </summary>
        [Test]
        public void TestCheckFieldInheritanceFirstGradeRelated()
        {
            var field1 = new Field
            {
                Name = "Science",
            };

            var field2 = new Field
            {
                Name = "Computer Science",
                ParentField = field1
            };

            var areRelated = FieldService.CheckFieldInheritance(field2, field1);
            Assert.True(areRelated);
        }

        /// <summary>
        /// Tests the check field inheritance second grade related.
        /// </summary>
        [Test]
        public void TestCheckFieldInheritanceSecondGradeRelated()
        {
            var field1 = new Field
            {
                Name = "Science",
            };

            var field2 = new Field
            {
                Name = "Computer Science",
                ParentField = field1
            };

            var field3 = new Field
            {
                Name = "Data Science",
                ParentField = field2
            };

            var areRelated = FieldService.CheckFieldInheritance(field3, field1);
            Assert.True(areRelated);
        }

        /// <summary>
        /// Tests the add field valid.
        /// </summary>
        [Test]
        public void TestAddFieldValid()
        {
            var field = new Field
            {
                Name = "Science",
            };

            var wasCreated = FieldService.AddField(field);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(f => f.SaveChanges(), Times.Once());
        }
    }
}
