// <copyright file="BookPublication.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The BookPublication class
    /// </summary>
    [HasSelfValidation]
    public class BookPublication
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        /// <value>
        /// The number of pages.
        /// </value>
        [RangeValidator(1, RangeBoundaryType.Inclusive, 10000, RangeBoundaryType.Inclusive, MessageTemplate = "NumberOfPages must be greater than 0", Ruleset = "BookPublicationNumberOfPagesFieldRange")]
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the cover material.
        /// </summary>
        /// <value>
        /// The cover material.
        /// </value>
        [NotNullValidator(MessageTemplate = "The CoverMaterial of the book cannot be null", Ruleset = "BookPublicationFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The CoverMaterial of the book should have between {3} and {5} letters", Ruleset = "BookPublicationCoverStringLength")]
        [RegexValidator(@"^[a-zA-Z]+$", MessageTemplate = "Only characters", Ruleset = "BookPublicationNameRegex")]
        public string CoverMaterial { get; set; }

        /// <summary>
        /// Gets or sets the publishing house.
        /// </summary>
        /// <value>
        /// The publishing house.
        /// </value>
        public PublishingHouse PublishingHouse { get; set; }

        /// <summary>
        /// Gets or sets the book.
        /// </summary>
        /// <value>
        /// The book.
        /// </value>
        public Book Book { get; set; }

        /// <summary>
        /// Gets or sets the book stock.
        /// </summary>
        /// <value>
        /// The book stock.
        /// </value>
        [Required]
        public virtual BookStock BookStock { get; set; }

        /// <summary>
        /// Gets or sets the book withdrawals.
        /// </summary>
        /// <value>
        /// The book withdrawals.
        /// </value>
        public virtual ICollection<BookWithdrawal> BookWithdrawals { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (PublishingHouse == null)
            {
                validationResults.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("PublishingHouse is null", this, "ValidatePublishingHouse", "error", null));
            }

            if (Book == null)
            {
                validationResults.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("Book is null", this, "ValidateBook", "error", null));
            }

            if (BookStock == null)
            {
                validationResults.AddResult(new Microsoft.Practices.EnterpriseLibrary.Validation.ValidationResult("BookStock is null", this, "ValidateBookStock", "error", null));
            }
        }
    }
}
