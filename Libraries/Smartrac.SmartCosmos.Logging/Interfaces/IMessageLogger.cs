using System;

namespace Smartrac.SmartCosmos.Logging
{
    [Flags]
    public enum LogType
    {
        Info = 1,  //     Info log level.
        Debug = 2, //     Debug log level.
        Warning = 4, //    Warning log level.
        Error = 8, //     Error log level.
        Fatal = 16 //     Fatal log level.
    }

    public interface IMessageLogger
    {
        /// <summary>
        /// Add log message
        /// </summary>
        /// <param name="message">message</param>
        /// <param name="logType">logType</param>
        void AddLog(string message, LogType logType = LogType.Debug);

        /// <summary>
        /// Check if logging allowed
        /// </summary>
        /// <param name="logType">logType</param>
        /// <returns>logging allowed</returns>
        bool CanLog(LogType logType);
    }
}