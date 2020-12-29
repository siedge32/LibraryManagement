// <copyright file="BookWithdrawal.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The BookWithdrawal class
    /// </summary>
    [HasSelfValidation]
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
        [NotNullValidator(MessageTemplate = "The Reader cannot be null", Ruleset = "BookWithdrawalFieldNotNull")]
        public Reader Reader { get; set; }

        /// <summary>
        /// Gets or sets the librarian.
        /// </summary>
        /// <value>
        /// The librarian.
        /// </value>
        [NotNullValidator(MessageTemplate = "The Librarian cannot be null", Ruleset = "BookWithdrawalFieldNotNull")]
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

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.BookPublications == null || this.BookPublications.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Empty BookPublications or null", this, "ValidateBookPublications", "error", null));
            }

            if (this.DateRented > this.DateToReturn)
            {
                validationResults.AddResult(new ValidationResult("DateRented is future dated than DateToReturn", this, "ValidateBookPublications", "error", null));
            }
        }
    }
}
