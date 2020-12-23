using LibraryManagement.BusinessLayer;
using LibraryManagement.DataMapper;
using LibraryManagement.DomainModel;
using LibraryManagement.Utils;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementTests
{
    [TestFixture]
    public class FieldTests
    {
        public Mock<LibraryDbContext> LibraryContextMock { get; private set; }
        public FieldService FieldService { get; private set; }

        [SetUp]
        public void SetUp()
        {
            var mockSet = new Mock<DbSet<Field>>();
            LibraryContextMock = new Mock<LibraryDbContext>();
            LibraryContextMock.Setup(m => m.Set<Field>()).Returns(mockSet.Object);
            FieldService = new FieldService(new FieldRepository(LibraryContextMock.Object));
        }

        [Test]
        public void AddNullField()
        {
            Field field = null;

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        [Test]
        public void AddNullName()
        {
            var field = new Field
            {
                Name = null
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLengthName0()
        {
            var field = new Field
            {
                Name = ""
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLengthName1()
        {
            var field = new Field
            {
                Name = "s"
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

        [Test]
        public void TestAddInvalidLengthName2()
        {
            var field = new Field
            {
                Name = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero. "
            };

            var wasCreated = FieldService.AddField(field);
            Assert.False(wasCreated);
            LibraryContextMock.Verify(f => f.SaveChanges(), Times.Never());
        }

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

        [Test]
        public void TestAddFieldValid()
        {
            var field = new Field
            {
                Name = "Science",
            };

            var wasCreated = FieldService.AddField(field);
            Assert.True(wasCreated);
            LibraryContextMock.Verify(f => f.SaveChanges(), Times.Once());
        }
    }
}
