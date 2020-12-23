// <copyright file="PublishingHouse.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System.Collections.Generic;

    /// <summary>
    /// The PublishingHouse class
    /// </summary>
    public class PublishingHouse
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Adress { get; set; }

        /// <summary>
        /// Gets or sets the published books.
        /// </summary>
        /// <value>
        /// The published books.
        /// </value>
        public virtual ICollection<BookPublication> PublishedBooks { get; set; }
    }
}
