using NLog;
using NLog.Layouts;
using NLog.Targets;
using System;

namespace Smartrac.SmartCosmos.Logging.File
{
    /// <summary>
    /// simple single thread log to file service
    /// </summary>
    public sealed class SingleFileLoggerService
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public static void AddLog(string message, LogType logType = LogType.Debug)
        {
            if (logger == null)
                return;

            switch (logType)
            {
                case LogType.Debug:
                    logger.Debug(message);
                    break;

                case LogType.Error:
                    logger.Error(message);
                    break;

                case LogType.Fatal:
                    logger.Fatal(message);
                    break;

                case LogType.Info:
                    logger.Info(message);
                    break;

                case LogType.Warning:
                    logger.Warn(message);
                    break;

                default:
                    logger.Error(message);
                    break;
            }
        }

        /// <summary>
        /// Change output file for logging. Accepts NLog placeholders such as {basedir}.
        /// Use '/' for path element separators.
        /// </summary>
        /// <param name="sLogFilePath">log file path</param>
        public static void SetLogFilePath(string sLogFilePath, string loggerId = "f")
        {
            if (LogManager.Configuration == null)
                throw new Exception("NLog.config not found!");

            // Name of target must match name property in <target xsi:type="File" name="targetf" ... /> of NLog.config
            var target = (FileTarget)LogManager.Configuration.FindTargetByName("target" + loggerId);
            if (target != null)
            {
                target.FileName = sLogFilePath;
                LogManager.ReconfigExistingLoggers();
            }
            else
            {
                throw new Exception("target 'target" + loggerId + "' in NLog.config not found!");
            }
        }

        /// <summary>
        /// Get output file for logging.
        /// </summary>
        /// <param name="sLogFilePath">log file path</param>
        public static string GetLogFilePath(string loggerId = "f")
        {
            if (LogManager.Configuration == null)
                throw new Exception("NLog.config not found!");

            // Name of target must match name property in <target xsi:type="File" name="targetf" ... /> of NLog.config
            var target = (FileTarget)LogManager.Configuration.FindTargetByName("target" + loggerId);
            if (target != null)
            {
                return ((SimpleLayout)target.FileName).Text;
            }
            else
            {
                throw new Exception("target 'target" + loggerId + "' in NLog.config not found!");
            }
        }

        public static void Flush()
        {
            LogManager.Flush();
        }

        /// <summary>
        /// Check if logging allowed
        /// </summary>
        /// <param name="logType">logType</param>
        /// <returns>logging allowed</returns>
        public bool CanLog(LogType logType)
        {
            return true;
        }
    }
}