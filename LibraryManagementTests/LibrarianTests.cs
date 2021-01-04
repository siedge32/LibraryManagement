// <copyright file="LibrarianTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System.Data.Entity;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The Librarian tests class
    /// </summary>
    [TestFixture]
    public class LibrarianTests
    {
        /// <summary>
        /// Gets the librarian service.
        /// </summary>
        /// <value>
        /// The librarian service.
        /// </value>
        public LibrarianService LibrarianService { get; private set; }

        /// <summary>
        /// Gets the library context mock.
        /// </summary>
        /// <value>
        /// The library context mock.
        /// </value>
        public Mock<LibraryDbContext> LibraryContextMock { get; private set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var mockSet = new Mock<DbSet<Librarian>>();
            this.LibraryContextMock = new Mock<LibraryDbContext>();
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(mockSet.Object);
            this.LibrarianService = new LibrarianService(new LibrarianRepository(this.LibraryContextMock.Object));
        }

        /// <summary>
        /// Tests the add null librarian.
        /// </summary>
        [Test]
        public void TestAddNullLibrarian()
        {
            Librarian librarian = null;
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the first name of the add null.
        /// </summary>
        [Test]
        public void TestAddNullFirstName()
        {
            var librarian = new Librarian
            {
                FirstName = null,
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length first name empty.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthFirstNameEmpty()
        {
            var librarian = new Librarian
            {
                FirstName = string.Empty,
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length first name short.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthFirstNameShort()
        {
            var librarian = new Librarian
            {
                FirstName = "A",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length first name to long.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthFirstNameToLong()
        {
            var librarian = new Librarian
            {
                FirstName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero.",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex first name lower case.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexFirstNameLowerCase()
        {
            // Starts with lower case letter
            var librarian = new Librarian
            {
                FirstName = "bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex first name digits.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexFirstNameDigits()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan123",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex first name symbols.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexFirstNameSymbols()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan@#$",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the last name of the add null.
        /// </summary>
        [Test]
        public void TestAddNullLastName()
        {
            var librarian = new Librarian
            {
                LastName = null,
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length last name empty.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthLastNameEmpty()
        {
            var librarian = new Librarian
            {
                LastName = string.Empty,
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length last name to short.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthLastNameToShort()
        {
            var librarian = new Librarian
            {
                LastName = "A",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length last name to long.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthLastNameToLong()
        {
            var librarian = new Librarian
            {
                LastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero.",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex second name lower case.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexSecondNameLowerCase()
        {
            // Starts with lower case letter
            var librarian = new Librarian
            {
                LastName = "hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex second name digits.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexSecondNameDigits()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu123",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex second name symbols.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexSecondNameSymbols()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu@##$",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid null address.
        /// </summary>
        [Test]
        public void TestAddInvalidNullAdress()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = null,
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid empty address.
        /// </summary>
        [Test]
        public void TestAddInvalidEmptyAdress()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = string.Empty,
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid short address.
        /// </summary>
        [Test]
        public void TestAddInvalidShortAdress()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "A",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid long address.
        /// </summary>
        [Test]
        public void TestAddInvalidLongAdress()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };

            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the get address.
        /// </summary>
        [Test]
        public void TestGetAdress()
        {
            var librarian = new Librarian
            {
                Address = "Brasov"
            };

            Assert.True(librarian.Address.Equals("Brasov"));
        }

        /// <summary>
        /// Tests the add null phone number.
        /// </summary>
        [Test]
        public void TestAddNullPhoneNumber()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = null,
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex phone number.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexPhoneNumber()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "clearly not a phone number",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add null email.
        /// </summary>
        [Test]
        public void TestAddNullEmail()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = null,
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex email.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexEmail()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0743702901",
                Email = "smthstmh.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add null gender.
        /// </summary>
        [Test]
        public void TestAddNullGender()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = null
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the valid gender upper f.
        /// </summary>
        [Test]
        public void TestValidGenderUpperF()
        {
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "F"
            };
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, librarian);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the valid gender lower m.
        /// </summary>
        [Test]
        public void TestValidGenderLowerM()
        {
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "m"
            };
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, librarian);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the valid gender lower f.
        /// </summary>
        [Test]
        public void TestValidGenderLowerF()
        {
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "f"
            };
            var ruleSet = "AuthorFieldNotNull";
            var result = ValidationUtil.ValidateEntity(ruleSet, librarian);
            Assert.IsTrue(result.IsValid);
        }

        /// <summary>
        /// Tests the add invalid gender.
        /// </summary>
        [Test]
        public void TestAddInvalidGender()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "Helicopter"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add valid librarian.
        /// </summary>
        [Test]
        public void TestAddValidLibrarian()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound first name space.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundFirstNameSpace()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan Dan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound first name dash.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundFirstNameDash()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound last name space.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundLastNameSpace()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu Popescu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound last name dash.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundLastNameDash()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address space.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressSpace()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address dash.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressDash()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov-Noua",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address dash short.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressDashShort()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov-Noua Str. Rozmarinului",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address dash short number.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressDashShortNumber()
        {
            // Contains numbers
            var librarian = new Librarian
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov-Noua Str. Rozmarinului nr. 14",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = LibrarianService.AddLibrarian(librarian);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }
    }
}
