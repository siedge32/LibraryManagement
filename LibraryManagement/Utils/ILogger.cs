using System.Reflection;

namespace LibraryManagement.Utils
{
    public interface ILogger
    {
        void LogError(string message, MethodBase method);
        void LogInfo(string message, MethodBase method);
        void LogWarning(string message, MethodBase method);
    }
}