// <copyright file="FieldService.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.BusinessLayer
{
    using System.Reflection;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;

    /// <summary>
    /// The FieldService class
    /// </summary>
    public class FieldService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FieldService"/> class.
        /// </summary>
        /// <param name="fieldRepository">The field repository.</param>
        public FieldService(FieldRepository fieldRepository)
        {
            FieldRepository = fieldRepository;
            Logger = new Logger();
        }

        /// <summary>
        /// Gets or sets the field repository.
        /// </summary>
        /// <value>
        /// The field repository.
        /// </value>
        private FieldRepository FieldRepository { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Adds the field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>True if the field is created, false else</returns>
        public bool AddField(Field field)
        {
            if (field == null)
            {
                Logger.LogError("Field is null", MethodBase.GetCurrentMethod());
                return false;
            }

            var ruleSets = new string[] { "FieldNameFieldNotNull", "FieldNameStringLength", string.Empty };

            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, field);
                if (!results.IsValid)
                {
                    foreach (var result in results)
                    {
                        Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                    }

                    return false;
                }
            }

            FieldRepository.Create(field);
            return true;
        }

        /// <summary>
        /// Checks the field inheritance.
        /// </summary>
        /// <param name="lhs">The LHS.</param>
        /// <param name="rhs">The RHS.</param>
        /// <returns>True if the field is inherited, false else</returns>
        public bool CheckFieldInheritance(Field lhs, Field rhs)
        {
            if (lhs == null || rhs == null)
            {
                Logger.LogError("One of the fields are null", MethodBase.GetCurrentMethod());
                return false;
            }

            return ValidationUtil.CheckInheritanceField(lhs, rhs);
        }
    }
}
