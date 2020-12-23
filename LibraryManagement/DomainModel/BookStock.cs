// <copyright file="BookStock.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    /// <summary>
    /// The BookStock class
    /// </summary>
    public class BookStock
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number of books.
        /// </summary>
        /// <value>
        /// The number of books.
        /// </value>
        public int NumberOfBooks { get; set; }

        /// <summary>
        /// Gets or sets the number of books for lecture.
        /// </summary>
        /// <value>
        /// The number of books for lecture.
        /// </value>
        public int NumberOfBooksForLecture { get; set; }

        /// <summary>
        /// Gets or sets the book publication.
        /// </summary>
        /// <value>
        /// The book publication.
        /// </value>
        public virtual BookPublication BookPublication { get; set; }
    }
}
