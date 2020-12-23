// <copyright file="FieldRepository.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DataMapper
{
    using LibraryManagement.DomainModel;

    /// <summary>
    /// The FieldRepository class
    /// </summary>
    /// <seealso cref="LibraryManagement.DataMapper.RepositoryBase{LibraryManagement.DomainModel.Field}" />
    public class FieldRepository : RepositoryBase<Field>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldRepository"/> class.
        /// </summary>
        /// <param name="libraryDbContext">The library database context.</param>
        public FieldRepository(LibraryDbContext libraryDbContext) : base(libraryDbContext)
        {
        }
    }
}
