using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLV.LMS.Common.Logging
{

    public enum LogModule
    {
        Web,
        BLL,
        DAL,
        Common
    }

    /// <summary>
    /// Log Severity
    /// </summary>
    public enum LogSeverity
    {
        Debug = 1,
        Error = 4,
        Fatal = 5,
        Info = 2,
        Off = 6,
        Trace = 0,
        Warning = 3
    }

    /// <summary>
    /// Log Level Helper
    /// </summary>
    internal class LogLevelHelper
    {
        /// <summary>
        /// Gets the n log log level.
        /// </summary>
        /// <param name="logSeverity">The log severity.</param>
        /// <returns></returns>
        public static LogLevel GetNLogLogLevel(LogSeverity logSeverity)
        {
            switch (logSeverity)
            {
                case LogSeverity.Debug:
                    return LogLevel.Debug;
                case LogSeverity.Error:
                    return LogLevel.Error;
                case LogSeverity.Fatal:
                    return LogLevel.Fatal;
                case LogSeverity.Info:
                    return LogLevel.Info;
                case LogSeverity.Off:
                    return LogLevel.Off;
                case LogSeverity.Trace:
                    return LogLevel.Trace;
                case LogSeverity.Warning:
                    return LogLevel.Warn;
                default:
                    return LogLevel.Off;
            }
        }
    }
}
