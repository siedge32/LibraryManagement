// <copyright file="BookPublicationRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The BookPublicationRepository class
    /// </summary>
    /// <seealso cref="LibraryManagement.DataMapper.RepositoryBase{LibraryManagement.DomainModel.BookPublication}" />
    public class BookPublicationRepository : RepositoryBase<BookPublication>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookPublicationRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public BookPublicationRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
