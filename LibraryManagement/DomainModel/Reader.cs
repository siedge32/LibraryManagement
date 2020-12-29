// <copyright file="Reader.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The Reader class
    /// </summary>
    [HasSelfValidation]
    public class Reader
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The FirstName cannot be null", Ruleset = "ReaderFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The FirstName should have between {3} and {5} letters", Ruleset = "ReaderNameStringLength")]
        [RegexValidator(@"^[a-zA-Z]+$", MessageTemplate = "Only characters", Ruleset = "ReaderNameRegex")]
        [RegexValidator(@"^[A-Z]", MessageTemplate = "Start with capital letter", Ruleset = "ReaderNameRegex")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The LastName cannot be null", Ruleset = "ReaderFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The LastName should have between {3} and {5} letters", Ruleset = "ReaderNameStringLength")]
        [RegexValidator(@"^[a-zA-Z]+$", MessageTemplate = "Only characters", Ruleset = "ReaderNameRegex")]
        [RegexValidator(@"^[A-Z]", MessageTemplate = "Start with capital letter", Ruleset = "ReaderNameRegex")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [NotNullValidator(MessageTemplate = "The Address cannot be null", Ruleset = "ReaderFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, ErrorMessage = "The Adress should have between {3} and {5} letters", Ruleset = "ReaderNameStringLength")]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [NotNullValidator(MessageTemplate = "The PhoneNumber cannot be null", Ruleset = "ReaderFieldNotNull")]
        [RegexValidator(@"^[0-9]+$", MessageTemplate = "Invalid Romanian PhoneNumber", Ruleset = "ReaderNameRegex")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [NotNullValidator(MessageTemplate = "The Email cannot be null", Ruleset = "ReaderFieldNotNull")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [NotNullValidator(MessageTemplate = "The Gender cannot be null", Ruleset = "ReaderFieldNotNull")]
        [DomainValidator("m", "f", "M", "F", MessageTemplate = "Unknown gender", Ruleset = "ReaderInvalidGender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the book withdrawals.
        /// </summary>
        /// <value>
        /// The book withdrawals.
        /// </value>
        public virtual ICollection<BookWithdrawal> BookWithdrawals { get; set; }

        /// <summary>
        /// Returns true if ... is valid.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void IsValid(ValidationResults validationResults)
        {
            try
            {
                var mail = new MailAddress(this.Email);
            }
            catch (FormatException)
            {
                validationResults.AddResult(new ValidationResult("Invalid Email", this, "InvalidEmail", "error", null));
            }
        }
    }
}
