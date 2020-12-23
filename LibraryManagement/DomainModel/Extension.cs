// <copyright file="Extension.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System;

    /// <summary>
    /// The Extension class 
    /// </summary>
    public class Extension
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
        /// Gets or sets the book withdrawal.
        /// </summary>
        /// <value>
        /// The book withdrawal.
        /// </value>
        public BookWithdrawal BookWithdrawal { get; set; }
    }
}
