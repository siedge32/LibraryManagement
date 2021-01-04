// <copyright file="LibrarianService.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.BusinessLayer
{
    using System.Reflection;
    using LibraryManagement.DataMapper;
    using LibraryManagement.DomainModel;
    using LibraryManagement.Utils;

    /// <summary>
    /// The LibrarianService class
    /// </summary>
    public class LibrarianService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LibrarianService"/> class.
        /// </summary>
        /// <param name="librarianRepository">The librarian repository.</param>
        public LibrarianService(LibrarianRepository librarianRepository)
        {
            LibrarianRepository = librarianRepository;
            Logger = new Logger();
        }

        /// <summary>
        /// Gets the librarian repository.
        /// </summary>
        /// <value>
        /// The librarian repository.
        /// </value>
        public LibrarianRepository LibrarianRepository { get; private set; }

        /// <summary>
        /// Gets the logger.
        /// </summary>
        /// <value>
        /// The logger.
        /// </value>
        public Logger Logger { get; private set; }

        /// <summary>
        /// Adds the librarian.
        /// </summary>
        /// <param name="librarian">The librarian.</param>
        /// <returns>true if the librarian has been added, false else</returns>
        public bool AddLibrarian(Librarian librarian)
        {
            if (librarian == null)
            {
                Logger.LogError("Librarian is null", MethodBase.GetCurrentMethod());
                return false;
            }

            var ruleSets = new string[] { "LibrarianFieldNotNull", "LibrarianNameStringLength", "LibrarianNameRegex", "LibrarianInvalidGender", string.Empty };

            foreach (var ruleSet in ruleSets)
            {
                var results = ValidationUtil.ValidateEntity(ruleSet, librarian);
                if (!results.IsValid)
                {
                    foreach (var result in results)
                    {
                        Logger.LogError(result.Message, MethodBase.GetCurrentMethod());
                    }

                    return false;
                }
            }

            LibrarianRepository.Create(librarian);

            return true;
        }
    }
}
