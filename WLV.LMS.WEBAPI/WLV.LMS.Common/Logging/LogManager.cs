using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.Common.Logging
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class LogManager
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static Logger logger;

        /// <summary>
        /// Initializes the <see cref="LogManager"/> class.
        /// </summary>
        static LogManager()
        {
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Logs the specified log severity.
        /// </summary>
        /// <param name="logSeverity">The log severity.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        public static void Log(LogSeverity logSeverity, LogModule module, string message)
        {
            Log(logSeverity, module, message, null);
        }

        /// <summary>
        /// Logs the specified log severity.
        /// </summary>
        /// <param name="logSeverity">The log severity.</param>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        public static void Log(LogSeverity logSeverity, LogModule module, string message, Exception exception)
        {
            string formatedMessage = FormatLogMessage(module, message);

            if (exception != null)
            {
                logger.Log(LogLevelHelper.GetNLogLogLevel(logSeverity), formatedMessage, exception, new object[0]);
            }
            else
            {
                logger.Log(LogLevelHelper.GetNLogLogLevel(logSeverity), formatedMessage);
            }
        }

        /// <summary>
        /// Formats the log message.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        private static string FormatLogMessage(LogModule module, string message)
        {
            return string.Format("{0} : {1}", module.ToString(), message);
        }
    }
}
