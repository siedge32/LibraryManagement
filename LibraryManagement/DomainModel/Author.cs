// <copyright file="Author.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The Author class
    /// </summary>
    public class Author
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
        [NotNullValidator(MessageTemplate = "The FirstName cannot be null", Ruleset = "AuthorFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The FirstName should have between {3} and {5} letters", Ruleset = "AuthorNameStringLength")]
        [RegexValidator(@"^[a-zA-Z -]+$", MessageTemplate = "Only characters, empty space and -", Ruleset = "AuthorNameRegex")]
        [RegexValidator(@"^[A-Z]", MessageTemplate = "Start with capital letter", Ruleset = "AuthorNameRegex")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [NotNullValidator(MessageTemplate = "The SecondName cannot be null", Ruleset = "AuthorFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The LastName should have between {3} and {5} letters", Ruleset = "AuthorNameStringLength")]
        [RegexValidator(@"^[a-zA-Z -]+$", MessageTemplate = "Only characters", Ruleset = "AuthorNameRegex")]
        [RegexValidator(@"^[A-Z]", MessageTemplate = "Start with capital letter", Ruleset = "AuthorNameRegex")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        [NotNullValidator(MessageTemplate = "The Adress cannot be null", Ruleset = "AuthorFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 100, RangeBoundaryType.Inclusive, ErrorMessage = "The LastName should have between {3} and {5} letters", Ruleset = "AuthorNameStringLength")]
        [RegexValidator(@"^[a-zA-Z0-9 .-]+$", MessageTemplate = "Only letters, numbers and -", Ruleset = "AuthorNameRegex")]
        public string Adress { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        [NotNullValidator(MessageTemplate = "The Gender cannot be null", Ruleset = "AuthorFieldNotNull")]
        [DomainValidator("m", "f", "M", "F", MessageTemplate = "Unknown gender", Ruleset = "AuthorInvalidGender")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public ICollection<Book> Books { get; set; }
    }
}
