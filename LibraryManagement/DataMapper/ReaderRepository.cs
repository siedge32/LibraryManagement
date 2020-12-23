// <copyright file="ReaderRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
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
    }
}
