// <copyright file="Extension.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The Extension class 
    /// </summary>
    [HasSelfValidation]
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
        /// Gets or sets the date extension was made.
        /// </summary>
        /// <value>
        /// The date to return.
        /// </value>
        public DateTime DateExtensionWasMade { get; set; }

        /// <summary>
        /// Gets or sets the book withdrawal.
        /// </summary>
        /// <value>
        /// The book withdrawal.
        /// </value>
        [NotNullValidator(MessageTemplate = "The DateToReturn cannot be null", Ruleset = "ExtensionFieldNotNull")]
        public BookWithdrawal BookWithdrawal { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.DateExtensionWasMade > this.DateToReturn)
            {
                validationResults.AddResult(new ValidationResult("DateExtensionWasMade is greater than DateToReturn", this, "ValidateCategories", "error", null));
            }
        }
    }
}
