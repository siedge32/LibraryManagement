// <copyright file="Field.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.DomainModel
{
    using System.Collections.Generic;
    using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

    /// <summary>
    /// The Field class
    /// </summary>
    public class Field
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
        [NotNullValidator(MessageTemplate = "The Name of the field cannot be null", Ruleset = "FieldNameFieldNotNull")]
        [StringLengthValidator(2, RangeBoundaryType.Inclusive, 40, RangeBoundaryType.Inclusive, ErrorMessage = "The name of the field should have between {3} and {5} letters", Ruleset = "FieldNameStringLength")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the books.
        /// </summary>
        /// <value>
        /// The books.
        /// </value>
        public virtual ICollection<Book> Books { get; set; }

        /// <summary>
        /// Gets or sets the parent field.
        /// </summary>
        /// <value>
        /// The parent field.
        /// </value>
        public Field ParentField { get; set; }
    }
}
