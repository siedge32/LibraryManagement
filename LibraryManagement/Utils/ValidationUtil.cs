// <copyright file="ValidationUtil.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.Utils
{
    using System.Collections.Generic;
    using LibraryManagement.DomainModel;
    using Microsoft.Practices.EnterpriseLibrary.Validation;

    /// <summary>
    /// The Validation Utility class
    /// </summary>
    public class ValidationUtil
    {
        /// <summary>
        /// Validates the entity.
        /// </summary>
        /// <typeparam name="T">Data model type.</typeparam>
        /// <param name="ruleSet">The rule set.</param>
        /// <param name="entity">The entity.</param>
        /// <returns>The ValidationResults</returns>
        public static ValidationResults ValidateEntity<T>(string ruleSet, T entity)
        {
            var validator = ValidationFactory.CreateValidator<T>(ruleSet);
            var results = new ValidationResults();
            validator.Validate(entity, results);

            return results;
        }

        /// <summary>
        /// Checks the inheritance fields.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="restOfFields">The rest of fields.</param>
        /// <returns>True if field is inherited from fields, false else</returns>
        public static bool CheckInheritanceFields(Field field, List<Field> restOfFields)
        {
            while (field != null)
            {
                foreach (var nextField in restOfFields)
                {
                    if (CheckInheritanceField(field, nextField))
                    {
                        return true;
                    }
                }

                field = field.ParentField;
            }

            return false;
        }

        /// <summary>
        /// Checks the inheritance field.
        /// </summary>
        /// <param name="currentField">The current field.</param>
        /// <param name="fieldToBeCheckedAgainst">The field to be checked against.</param>
        /// <returns>True if field is inherited from field, false else</returns>
        public static bool CheckInheritanceField(Field currentField, Field fieldToBeCheckedAgainst)
        {
            while (currentField != null)
            {
                if (currentField.Name.Equals(fieldToBeCheckedAgainst.Name))
                {
                    return true;
                }

                currentField = currentField.ParentField;
            }

            return false;
        }
    }
}
