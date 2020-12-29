// <copyright file="BookStock.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The BookStock class
    /// </summary>
    [HasSelfValidation]
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
        [RangeValidator(1, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, MessageTemplate = "NumberOfBooks must be between {3} and {5}", Ruleset = "BookStockRangeValidator")]
        public int NumberOfBooks { get; set; }

        /// <summary>
        /// Gets or sets the number of books for lecture.
        /// </summary>
        /// <value>
        /// The number of books for lecture.
        /// </value>
        [RangeValidator(0, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, MessageTemplate = "NumberOfBooksForLecture must be between {3} and {5}", Ruleset = "BookStockRangeValidator")]
        public int NumberOfBooksForLecture { get; set; }

        /// <summary>
        /// Gets or sets the book publication.
        /// </summary>
        /// <value>
        /// The book publication.
        /// </value>
        public virtual BookPublication BookPublication { get; set; }

        /// <summary>Validates the specified validation results.</summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.NumberOfBooksForLecture > this.NumberOfBooks)
            {
                validationResults.AddResult(new ValidationResult("NumberOfBooksForLecture greater than NumberOfBooks", this, "ValidateBookStock", "error", null));
            }
        }
    }
}
