// <copyright file="ILogger.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.Utils
{
    using System.Reflection;

    /// <summary>
    /// The ILogger interface
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        void LogError(string message, MethodBase method);

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        void LogInfo(string message, MethodBase method);

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        void LogWarning(string message, MethodBase method);
    }
}