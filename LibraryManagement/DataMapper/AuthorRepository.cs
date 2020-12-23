// <copyright file="AuthorRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The AuthorRepository class
    /// </summary>
    /// <seealso cref="LibraryManagement.DataMapper.RepositoryBase{LibraryManagement.DomainModel.Author}" />
    public class AuthorRepository : RepositoryBase<Author>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public AuthorRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
