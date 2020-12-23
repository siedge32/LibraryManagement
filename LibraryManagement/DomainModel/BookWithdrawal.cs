// <copyright file="BookWithdrawal.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The BookWithdrawal class
    /// </summary>
    public class BookWithdrawal
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the date to return.
        /// </summary>
        /// <value>
        /// The date to return.
        /// </value>
        public DateTime DateToReturn { get; set; }

        /// <summary>
        /// Gets or sets the date rented.
        /// </summary>
        /// <value>
        /// The date rented.
        /// </value>
        public DateTime DateRented { get; set; }

        /// <summary>
        /// Gets or sets the reader.
        /// </summary>
        /// <value>
        /// The reader.
        /// </value>
        public Reader Reader { get; set; }

        /// <summary>
        /// Gets or sets the librarian.
        /// </summary>
        /// <value>
        /// The librarian.
        /// </value>
        public Librarian Librarian { get; set; }

        /// <summary>
        /// Gets or sets the book publications.
        /// </summary>
        /// <value>
        /// The book publications.
        /// </value>
        public virtual ICollection<BookPublication> BookPublications { get; set; }

        /// <summary>
        /// Gets or sets the extensions.
        /// </summary>
        /// <value>
        /// The extensions.
        /// </value>
        public virtual ICollection<Extension> Extensions { get; set; }
    }
}
