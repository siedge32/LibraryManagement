using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Utils
{
    public class Logger : ILogger
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void LogInfo(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Info("[" + methodName + "]:" + message);
            }
        }

        public void LogError(string message, MethodBase method)
        {
            string methodName = method.DeclaringType.Name + "." + method.Name;
            if (Log.IsInfoEnabled)
            {
                Log.Error("[" + methodName + "]:" + message);
            }
        }

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
