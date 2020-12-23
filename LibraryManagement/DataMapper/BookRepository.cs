// <copyright file="BookRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The BookRepository class
    /// </summary>
    /// <seealso cref="LibraryManagement.DataMapper.RepositoryBase{LibraryManagement.DomainModel.Book}" />
    public class BookRepository : RepositoryBase<Book>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public BookRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
