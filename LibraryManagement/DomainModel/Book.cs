// <copyright file="Book.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using LibraryManagement.Utils;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The Book class
    /// </summary>
    [HasSelfValidation]
    public class Book
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
        [NotNullValidator(MessageTemplate = "The Name of the book cannot be null", Ruleset = "BookFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name of the book should have between {3} and {5} letters", Ruleset = "BookNameStringLength")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual ICollection<Field> Categories { get; set; }
        
        /// <summary>
        /// Gets or sets the authors.
        /// </summary>
        /// <value>
        /// The authors.
        /// </value>
        public virtual ICollection<Author> Authors { get; set; }
        
        /// <summary>
        /// Gets or sets the book publications.
        /// </summary>
        /// <value>
        /// The book publications.
        /// </value>
        public virtual ICollection<BookPublication> BookPublications { get; set; }

        /// <summary>
        /// Validates the specified validation results.
        /// </summary>
        /// <param name="validationResults">The validation results.</param>
        [SelfValidation]
        public void Validate(ValidationResults validationResults)
        {
            if (this.Categories == null || this.Categories.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Empty Categories or null", this, "ValidateCategories", "error", null));
            }

            var dOM = int.Parse(ConfigurationManager.AppSettings["DOM"]);
            if (this.Categories.Count > dOM)
            {
                validationResults.AddResult(new ValidationResult("Number of Categories higher than DOM", this, "ValidateCategoriesDOM", "error", null));
            }

            foreach (var field in this.Categories)
            {
                var restOfFields = this.Categories.ToList();
                restOfFields.Remove(field);

                if (ValidationUtil.CheckInheritanceFields(field, restOfFields))
                {
                    validationResults.AddResult(new ValidationResult("Invalid field", this, "ValidateField", "error", null));
                    break;
                }
            }

            if (this.Authors == null || this.Authors.Count == 0)
            {
                validationResults.AddResult(new ValidationResult("Empty Authors or null", this, "ValidateAuthors", "error", null));
            }
        }
    }
}
