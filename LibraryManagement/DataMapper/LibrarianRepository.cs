// <copyright file="LibrarianRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The LibrarianRepository class
    /// </summary>
    /// <seealso cref="LibraryManagement.DataMapper.RepositoryBase{LibraryManagement.DomainModel.Librarian}" />
    public class LibrarianRepository : RepositoryBase<Librarian>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarianRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public LibrarianRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
