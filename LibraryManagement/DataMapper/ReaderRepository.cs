﻿// <copyright file="ReaderRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using System.Linq;
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The ReaderRepository class
    /// </summary>
    /// <seealso cref="LibraryManagement.DataMapper.RepositoryBase{LibraryManagement.DomainModel.Reader}" />
    public class ReaderRepository : RepositoryBase<Reader>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReaderRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public ReaderRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }

        /// <summary>Determines whether the specified reader is librarian.</summary>
        /// <param name="reader">The reader.</param>
        /// <returns>
        /// <c>true</c> if the specified reader is librarian; otherwise, <c>false</c>.</returns>
        public bool IsLibrarian(Reader reader)
        {
            if (LibraryDbContext.Librarians == null)
            {
                return false;
            }

            return LibraryDbContext.Librarians.FirstOrDefault(l => l.Email.Equals(reader.Email)) != null;
        }
    }
}
