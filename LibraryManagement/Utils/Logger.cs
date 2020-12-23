// <copyright file="Logger.cs" company="Transilvania University of Brasov">
// Hanganu Bogdan
// </copyright>
namespace LibraryManagement.Utils
{
    using System.Reflection;

    /// <summary>
    /// The Logger class
    /// </summary>
    /// <seealso cref="LibraryManagement.Utils.ILogger" />
    public class Logger : ILogger
    {
        /// <summary>
        /// The log
        /// </summary>
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Logs the information.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        public void LogInfo(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Info("[" + methodName + "]:" + message);
            }
        }

        /// <summary>
        /// Logs the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        public void LogError(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Error("[" + methodName + "]:" + message);
            }
        }

        /// <summary>
        /// Logs the warning.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="method">The method.</param>
        public void LogWarning(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Warn("[" + methodName + "]:" + message);
            }
        }
    }
}
