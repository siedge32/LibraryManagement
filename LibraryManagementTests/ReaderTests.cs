// <copyright file="ReaderTests.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagementTests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using LibraryManagement.BusinessLayer;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using Moq;
    using NUnit.Framework;

    /// <summary>
    /// The ReaderTests class
    /// </summary>
    [TestFixture]
    public class ReaderTests
    {
        /// <summary>
        /// The field1
        /// </summary>
        private Field field1;

        /// <summary>
        /// The field2
        /// </summary>
        private Field field2;

        /// <summary>
        /// The author
        /// </summary>
        private Author author;

        /// <summary>
        /// The book
        /// </summary>
        private Book book;

        /// <summary>
        /// The p h
        /// </summary>
        private PublishingHouse pH;

        /// <summary>
        /// The book stock
        /// </summary>
        private BookStock bookStock;

        /// <summary>
        /// The book publication 1
        /// </summary>
        private BookPublication bookpH;

        /// <summary>
        /// The book publication 2
        /// </summary>
        private BookPublication bookpH2;

        /// <summary>
        /// Gets the library context mock.
        /// </summary>
        /// <value>
        /// The library context mock.
        /// </value>
        public Mock<LibraryDbContext> LibraryContextMock { get; private set; }

        /// <summary>
        /// Gets the reader service.
        /// </summary>
        /// <value>
        /// The reader service.
        /// </value>
        public ReaderService ReaderService { get; private set; }

        /// <summary>
        /// Gets the librarian service.
        /// </summary>
        /// <value>
        /// The librarian service.
        /// </value>
        public LibrarianService LibrarianService { get; private set; }

        /// <summary>
        /// Sets up.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            var mockSetReader = new Mock<DbSet<Reader>>();
            var mockSetLibrarian = new Mock<DbSet<Librarian>>();

            this.LibraryContextMock = new Mock<LibraryDbContext>();
            this.LibraryContextMock.Setup(m => m.Set<Reader>()).Returns(mockSetReader.Object);
            this.LibraryContextMock.Setup(m => m.Set<Librarian>()).Returns(mockSetLibrarian.Object);

            this.ReaderService = new ReaderService(new ReaderRepository(this.LibraryContextMock.Object), new BookPublicationRepository(this.LibraryContextMock.Object));
            this.LibrarianService = new LibrarianService(new LibrarianRepository(this.LibraryContextMock.Object));

            this.field1 = new Field { Name = "Art" };
            this.field2 = new Field { Name = "Science" };

            this.author = new Author
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Gender = "M"
            };

            this.book = new Book
            {
                Name = "Art - Science",
                Categories = new List<Field>
                {
                   this.field1, this.field2
                },
                Authors = new List<Author> { this.author }
            };

            this.pH = new PublishingHouse
            {
                Name = "Corint"
            };

            this.bookStock = new BookStock
            {
                NumberOfBooks = 10,
                NumberOfBooksForLecture = 7,
                BookPublication = this.bookpH
            };

            var bookStock2 = new BookStock
            {
                NumberOfBooks = 10,
                NumberOfBooksForLecture = 7,
                BookPublication = this.bookpH2
            };

            this.bookpH = new BookPublication
            {
                NumberOfPages = 36,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = this.bookStock
            };

            this.bookpH2 = new BookPublication
            {
                NumberOfPages = 72,
                CoverMaterial = "Textil",
                PublishingHouse = this.pH,
                Book = this.book,
                BookStock = bookStock2
            };

            this.book.BookPublications = new List<BookPublication> { this.bookpH, this.bookpH2 };
        }

        /// <summary>
        /// Tests the add null reader.
        /// </summary>
        [Test]
        public void TestAddNullReader()
        {
            Reader reader = null;
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the first name of the add null.
        /// </summary>
        [Test]
        public void TestAddNullFirstName()
        {
            var reader = new Reader
            {
                FirstName = null,
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length first name empty.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthFirstNameEmpty()
        {
            var reader = new Reader
            {
                FirstName = string.Empty,
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length first name to short.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthFirstNameToShort()
        {
            var reader = new Reader
            {
                FirstName = "A",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length first name to long.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthFirstNameToLong()
        {
            var reader = new Reader
            {
                FirstName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero.",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex first name upper case.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexFirstNameUpperCase()
        {
            // Starts with lower case letter
            var reader = new Reader
            {
                FirstName = "bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                FirstName = "Bogdan123",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                FirstName = "Bogdan!@#",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the last name of the add null.
        /// </summary>
        [Test]
        public void TestAddNullLastName()
        {
            var reader = new Reader
            {
                LastName = null,
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length last name empty.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthLastNameEmpty()
        {
            var reader = new Reader
            {
                LastName = string.Empty,
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length last name to short.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthLastNameToShort()
        {
            var reader = new Reader
            {
                LastName = "A",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid string length last name to long.
        /// </summary>
        [Test]
        public void TestAddInvalidStringLengthLastNameToLong()
        {
            var reader = new Reader
            {
                LastName = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse at ultrices sapien, sit amet congue felis. Duis sollicitudin commodo mauris vitae lacinia. Curabitur neque arcu, pretium eu laoreet eget, fringilla a libero.",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid regex second name upper digit.
        /// </summary>
        [Test]
        public void TestAddInvalidRegexSecondNameUpperDigit()
        {
            // Starts with lower case letter
            var reader = new Reader
            {
                LastName = "hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                LastName = "Hanganu123",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                LastName = "Hanganu!@##$",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the get identifier.
        /// </summary>
        [Test]
        public void TestGetId()
        {
            var reader = new Reader
            {
                Id = 1
            };

            Assert.True(reader.Id == 1);
        }

        /// <summary>
        /// Tests the set identifier.
        /// </summary>
        [Test]
        public void TestSetId()
        {
            var reader = new Reader
            {
                Id = 1
            };

            reader.Id = 2;

            Assert.True(reader.Id == 2);
        }

        /// <summary>
        /// Tests the get address.
        /// </summary>
        [Test]
        public void TestGetAdress()
        {
            var reader = new Reader
            {
                Address = "Brasov"
            };

            Assert.True(reader.Address.Equals("Brasov"));
        }

        /// <summary>
        /// Tests the add invalid null address.
        /// </summary>
        [Test]
        public void TestAddInvalidNullAddress()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = null,
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid empty address.
        /// </summary>
        [Test]
        public void TestAddInvalidEmptyAddress()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = string.Empty,
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid address to short.
        /// </summary>
        [Test]
        public void TestAddInvalidAddressToShort()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "a",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid address to long.
        /// </summary>
        [Test]
        public void TestAddInvalidAddressToLong()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid address regex.
        /// </summary>
        [Test]
        public void TestAddInvalidAddressRegex()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "#$Brov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add null phone number.
        /// </summary>
        [Test]
        public void TestAddNullPhoneNumber()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = null,
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "clearly not a phone number",
                Email = "smth@stmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = null,
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0743702901",
                Email = "smthstmh.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
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
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = null
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add invalid gender.
        /// </summary>
        [Test]
        public void TestAddInvalidGender()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "Helicopter"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.False(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Never());
        }

        /// <summary>
        /// Tests the add valid gender upper f.
        /// </summary>
        [Test]
        public void TestAddValidGenderUpperF()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "F"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid gender lower f.
        /// </summary>
        [Test]
        public void TestAddValidGenderLowerF()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "f"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid gender lower m.
        /// </summary>
        [Test]
        public void TestAddValidGenderLowerM()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "m"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid reader.
        /// </summary>
        [Test]
        public void TestAddValidReader()
        {
            // Contains numbers
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound first name space.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundFirstNameSpace()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan Dan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound first name dash.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundFirstNameDash()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound last name space.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundLastNameSpace()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu Popescu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound last name dash.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundLastNameDash()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address space.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressSpace()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address dash.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressDash()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov-Noua",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address dash short.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressDashShort()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov-Noua Str. Rozmarinului",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the add valid librarian compound address dash short number.
        /// </summary>
        [Test]
        public void TestAddValidLibrarianCompoundAddressDashShortNumber()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan-Dan",
                LastName = "Hanganu-Popescu",
                Address = "Judetul Brasov-Noua Str. Rozmarinului nr. 14",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };
            var wasCreated = ReaderService.AddReader(reader);
            Assert.True(wasCreated);
            this.LibraryContextMock.Verify(b => b.SaveChanges(), Times.Once());
        }

        /// <summary>
        /// Tests the check number of books in period true.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksInPeriodTrue()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var canRent = ReaderService.CheckNumberOfBooksInPeriod(this.book.BookPublications.ToList(), reader);
            Assert.True(canRent);
        }

        /// <summary>
        /// Tests the check number of books in period false.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksInPeriodFalse()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                new BookWithdrawal
                {
                   DateRented = new DateTime(2020, 1, 1),
                   DateToReturn = new DateTime(2020, 1, 8),
                   Reader = reader,
                   BookPublications = this.book.BookPublications
                }
            };

            var canRent = ReaderService.CheckNumberOfBooksInPeriod(this.book.BookPublications.ToList(), reader);
            Assert.False(canRent);
        }

        /// <summary>
        /// Tests the number of distinctive categories for books0.
        /// </summary>
        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks0()
        {
            var books = new List<Book> { };
            var numberOfDistinctiveFields = ReaderService.GetDistinctiveCategoriesForBooks(books).Count;
            Assert.IsTrue(numberOfDistinctiveFields == 0);
        }

        /// <summary>
        /// Tests the number of distinctive categories for books1.
        /// </summary>
        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks1()
        {
            var books = new List<Book> { this.book };
            var numberOfDistinctiveFields = ReaderService.GetDistinctiveCategoriesForBooks(books).Count;
            Assert.IsTrue(numberOfDistinctiveFields == 2);
        }

        /// <summary>
        /// Tests the number of distinctive categories for books2.
        /// </summary>
        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks2()
        {
            var field3 = new Field { Name = "Math", ParentField = this.field2 };
            this.book.Categories.Add(field3);
            var books = new List<Book> { this.book };
            var numberOfDistinctiveFields = ReaderService.GetDistinctiveCategoriesForBooks(books).Count;
            Assert.IsTrue(numberOfDistinctiveFields == 2);
        }

        /// <summary>
        /// Tests the number of distinctive categories for books3.
        /// </summary>
        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks3()
        {
            var field3 = new Field { Name = "Math", ParentField = this.field2 };
            var field4 = new Field { Name = "Painting", ParentField = this.field1 };
            this.book.Categories.Add(field3);
            this.book.Categories.Add(field4);
            var books = new List<Book> { this.book };
            var numberOfDistinctiveFields = this.ReaderService.GetDistinctiveCategoriesForBooks(books).Count;
            Assert.IsTrue(numberOfDistinctiveFields == 2);
        }

        /// <summary>
        /// Tests the number of distinctive categories for books4.
        /// </summary>
        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks4()
        {
            var field3 = new Field { Name = "Math", ParentField = this.field2 };
            var field4 = new Field { Name = "Painting", ParentField = this.field1 };
            var field5 = new Field { Name = "Gastronomy" };
            this.book.Categories.Add(field3);
            this.book.Categories.Add(field4);
            this.book.Categories.Add(field5);
            var books = new List<Book> { this.book };
            var numberOfDistinctiveFields = this.ReaderService.GetDistinctiveCategoriesForBooks(books).Count;
            Assert.IsTrue(numberOfDistinctiveFields == 3);
        }

        /// <summary>
        /// Tests the number of distinctive categories for books5.
        /// </summary>
        [Test]
        public void TestNumberOfDistinctiveCategoriesForBooks5()
        {
            var field3 = new Field { Name = "Math", ParentField = this.field2 };
            var field4 = new Field { Name = "Painting", ParentField = this.field1 };
            var field5 = new Field { Name = "Gastronomy" };

            this.book = new Book
            {
                Name = "Mancaruri Traditionale",
                Categories = new List<Field>
                {
                   field3, field4, field5
                }
            };

            var books = new List<Book> { this.book };
            var numberOfDistinctiveFields = this.ReaderService.GetDistinctiveCategoriesForBooks(books).Count;
            Assert.IsTrue(numberOfDistinctiveFields == 3);
        }

        /// <summary>
        /// Tests the check number of books greater than c.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksGreaterThanC()
        {
            var listBookPublications = new List<BookPublication> { this.bookpH, this.bookpH, this.bookpH, this.bookpH, this.bookpH };
            var isValid = ReaderService.CheckNumberOfBooks(listBookPublications);
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Tests the check number of books.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooks()
        {
            var listBookPublications = new List<BookPublication> { this.bookpH };
            var isValid = ReaderService.CheckNumberOfBooks(listBookPublications);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the check number of books greater then three with less than two distinctive fields.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksGreaterThenThreeWithLessThanTwoDinstinctiveFields()
        {
            var listBookPublications = new List<BookPublication> { this.bookpH, this.bookpH, this.bookpH };
            var isValid = ReaderService.CheckNumberOfBooks(listBookPublications);
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Tests the check number of books greater then three with more than two distinctive fields.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksGreaterThenThreeWithMoreThanTwoDinsticntiveFields()
        {
            var horrorField = new Field { Name = "Horror" };

            var book2 = new Book
            {
                Authors = new List<Author> { this.author },
                Name = "It",
                Categories = new List<Field> { horrorField }
            };

            var bookPublication2 = new BookPublication { Book = book2 };

            var listBookPublications = new List<BookPublication> { this.bookpH, bookPublication2 };
            var isValid = ReaderService.CheckNumberOfBooks(listBookPublications);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the check number of fields in last given months0.
        /// </summary>
        [Test]
        public void TestCheckNumberOfFieldsInLastGivenMonths0()
        {
            var listBookPublications = new List<BookPublication> { this.bookpH };
            var reader = new Reader { };

            var isValid = ReaderService.CheckNumberOfFieldsInLastGivenMonths(listBookPublications, reader);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the check number of fields in last given month1.
        /// </summary>
        [Test]
        public void TestCheckNumberOfFieldsInLastGivenMonth1()
        {
            var parentField = new Field { Name = "Design" };
            this.field1.ParentField = parentField;
            var listBookPublications = new List<BookPublication> { this.bookpH };
            var reader = new Reader { };

            var isValid = ReaderService.CheckNumberOfFieldsInLastGivenMonths(listBookPublications, reader);
            Assert.IsTrue(isValid);
        }

        /// <summary>
        /// Tests the check number of fields in last given month2.
        /// </summary>
        [Test]
        public void TestCheckNumberOfFieldsInLastGivenMonth2()
        {
            var parentField = new Field { Name = "Design" };
            this.field1.ParentField = parentField;

            var book2 = new Book { Categories = new List<Field> { new Field { Name = "Romance" }, new Field { Name = "Comedy" } } };
            var bookpH2 = new BookPublication { Book = book2 };
            var listBookPublications = new List<BookPublication> { this.bookpH, bookpH2 };
            var reader = new Reader { };

            var isValid = ReaderService.CheckNumberOfFieldsInLastGivenMonths(listBookPublications, reader);
            Assert.IsFalse(isValid);
        }

        /// <summary>
        /// Tests the add extension null.
        /// </summary>
        [Test]
        public void TestAddExtensionNull()
        {
            var canAdd = this.ReaderService.AddExtension(null, null);
            Assert.IsFalse(canAdd);
        }

        /// <summary>
        /// Tests the add extension small date.
        /// </summary>
        [Test]
        public void TestAddExtensionSmallDate()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var extension = new Extension
            {
                Id = 1,
                BookWithdrawal = new BookWithdrawal(),
                DateExtensionWasMade = new DateTime(2020, 12, 1),
                DateToReturn = new DateTime(2020, 10, 1)
            };

            var canAdd = this.ReaderService.AddExtension(reader, extension);
            Assert.IsFalse(canAdd);
        }

        /// <summary>
        /// Tests the add extension valid.
        /// </summary>
        [Test]
        public void TestAddExtensionValid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            var extension1 = new Extension
            {
                BookWithdrawal = bookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 2, 1),
                DateToReturn = new DateTime(2020, 3, 1)
            };

            bookWithdrawal.Extensions = new List<Extension> { extension1 };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var currentBookWithdrawal = new BookWithdrawal() { Extensions = new List<Extension>() };

            var currentExtension = new Extension
            {
                Id = 1,
                BookWithdrawal = currentBookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 2, 1)
            };

            var canAdd = this.ReaderService.AddExtension(reader, currentExtension);
            Assert.IsTrue(canAdd);
        }

        /// <summary>
        /// Tests the add extension valid2.
        /// </summary>
        [Test]
        public void TestAddExtensionValid2()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            var extension1 = new Extension
            {
                BookWithdrawal = bookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 2, 1),
                DateToReturn = new DateTime(2020, 3, 1)
            };

            var extension2 = new Extension
            {
                BookWithdrawal = bookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 2, 1)
            };

            bookWithdrawal.Extensions = new List<Extension> { extension1, extension2 };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var currentBookWithdrawal = new BookWithdrawal() { Extensions = new List<Extension>() };

            var currentExtension = new Extension
            {
                Id = 1,
                BookWithdrawal = currentBookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 2, 1)
            };

            var canAdd = this.ReaderService.AddExtension(reader, currentExtension);
            Assert.IsTrue(canAdd);
        }

        /// <summary>
        /// Tests the add extension invalid.
        /// </summary>
        [Test]
        public void TestAddExtensionInvalid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            var extension1 = new Extension
            {
                BookWithdrawal = bookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 2, 1),
                DateToReturn = new DateTime(2020, 3, 1)
            };

            var extension2 = new Extension
            {
                BookWithdrawal = bookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 2, 1)
            };

            bookWithdrawal.Extensions = new List<Extension> { extension1, extension1, extension2, extension2 };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var currentBookWithdrawal = new BookWithdrawal() { Extensions = new List<Extension>() };

            var currentExtension = new Extension
            {
                Id = 1,
                BookWithdrawal = currentBookWithdrawal,
                DateExtensionWasMade = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 2, 1)
            };

            var canAdd = this.ReaderService.AddExtension(reader, currentExtension);
            Assert.IsFalse(canAdd);
        }

        /// <summary>
        /// Tests the check same book rented delta valid.
        /// </summary>
        [Test]
        public void TestCheckSameBookRentedDeltaValid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var canRent = this.ReaderService.CheckSameBookRentedDelta(this.book.BookPublications.ToList(), reader);
            Assert.IsTrue(canRent);
        }

        /// <summary>
        /// Tests the check same book rented delta invalid.
        /// </summary>
        [Test]
        public void TestCheckSameBookRentedDeltaInvalid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 3),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var canRent = this.ReaderService.CheckSameBookRentedDelta(this.book.BookPublications.ToList(), reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the check number of books in one day invalid.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksInOneDayInvalid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 3),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var bookPublicationToRent = new List<BookPublication> { this.book.BookPublications.First(), this.book.BookPublications.First(), this.book.BookPublications.First(), this.book.BookPublications.First() };

            var canRent = this.ReaderService.CheckNumberOfBooksInOneDay(bookPublicationToRent, reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the check number of books in one day valid.
        /// </summary>
        [Test]
        public void TestCheckNumberOfBooksInOneDayValid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var canRent = this.ReaderService.CheckNumberOfBooksInOneDay(new List<BookPublication> { this.book.BookPublications.First() }, reader);
            Assert.IsTrue(canRent);
        }

        /// <summary>
        /// Tests the can rent books invalid null book publications.
        /// </summary>
        [Test]
        public void TestCanRentBooksInvalidNullBookPublications()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var canRent = this.ReaderService.CanRentBooks(null, reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books invalid null reader.
        /// </summary>
        [Test]
        public void TestCanRentBooksInvalidNullReader()
        {
            var canRent = this.ReaderService.CanRentBooks(this.book.BookPublications.ToList(), null);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books invalid empty book publications.
        /// </summary>
        [Test]
        public void TestCanRentBooksInvalidEmptyBookPublications()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var canRent = this.ReaderService.CanRentBooks(new List<BookPublication>(), reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books false check number of books in period.
        /// </summary>
        [Test]
        public void TestCanRentBooksFalseCheckNumberOfBooksInPeriod()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                new BookWithdrawal
                {
                   DateRented = new DateTime(2020, 1, 1),
                   DateToReturn = new DateTime(2020, 1, 8),
                   Reader = reader,
                   BookPublications = this.book.BookPublications
                }
            };

            var canRent = this.ReaderService.CanRentBooks(this.book.BookPublications.ToList(), reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books false check number of books.
        /// </summary>
        [Test]
        public void TestCanRentBooksFalseCheckNumberOfBooks()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                new BookWithdrawal
                {
                   DateRented = new DateTime(2020, 1, 1),
                   DateToReturn = new DateTime(2020, 1, 8),
                   Reader = reader,
                   BookPublications = this.book.BookPublications
                }
            };

            var bookPublicationsToRent = new List<BookPublication>
            {
                this.book.BookPublications.First(),
                this.book.BookPublications.First(),
                this.book.BookPublications.First(),
                this.book.BookPublications.First(),
                this.book.BookPublications.First()
            };

            var canRent = this.ReaderService.CanRentBooks(bookPublicationsToRent, reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books invalid book publication.
        /// </summary>
        [Test]
        public void TestCanRentBooksInvalidBookPublication()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            var bookPublicationsToRent = new List<BookPublication>
            {
                this.book.BookPublications.First()
            };

            bookPublicationsToRent.First().NumberOfPages = -1;

            var canRent = this.ReaderService.CanRentBooks(bookPublicationsToRent, reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books invalid book publication book stock.
        /// </summary>
        [Test]
        public void TestCanRentBooksInvalidBookPublicationBookStock()
        {
            var reader = new Reader
            {
                LastName = "Hanganu",
                FirstName = "Bogdan",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@smth.com",
                Gender = "M"
            };

            var bookPublicationsToRent = new List<BookPublication>
            {
                this.book.BookPublications.First()
            };

            bookPublicationsToRent.First().BookStock.NumberOfBooks = 6;
            bookPublicationsToRent.First().BookStock.NumberOfBooksForLecture = 6;

            var canRent = this.ReaderService.CanRentBooks(bookPublicationsToRent, reader);
            Assert.IsFalse(canRent);
        }

        /// <summary>
        /// Tests the can rent books valid.
        /// </summary>
        [Test]
        public void TestCanRentBooksValid()
        {
            var reader = new Reader
            {
                FirstName = "Bogdan",
                LastName = "Hanganu",
                Address = "Brasov",
                Phone = "0746700901",
                Email = "smth@stmh.com",
                Gender = "M",
                BookWithdrawals = new List<BookWithdrawal>()
            };

            var bookWithdrawal = new BookWithdrawal
            {
                DateRented = new DateTime(2020, 1, 1),
                DateToReturn = new DateTime(2020, 1, 15),
                Reader = reader,
                BookPublications = this.book.BookPublications,
            };

            reader.BookWithdrawals = new List<BookWithdrawal>
            {
                bookWithdrawal
            };

            var canRent = this.ReaderService.CanRentBooks(this.book.BookPublications.ToList(), reader);
            Assert.IsTrue(canRent);
        }
    }
}