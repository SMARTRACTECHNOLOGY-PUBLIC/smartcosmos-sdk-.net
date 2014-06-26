using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using NLog.Targets;

namespace Smartrac.Logging.File
{
    /// <summary>
    /// Log to file service
    /// </summary>
    public sealed class FileLoggerService : IMessageLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public void AddLog(string aMessage, LogType aLogType = LogType.Debug)
        {
            if (logger == null)
                return;

            switch (aLogType)
            {
                case LogType.Debug:
                    logger.Debug(aMessage);
                    break;
                case LogType.Error:
                    logger.Error(aMessage);
                    break;
                case LogType.Fatal:
                    logger.Fatal(aMessage);
                    break;
                case LogType.Info:
                    logger.Info(aMessage);
                    break;
                case LogType.Warning:
                    logger.Warn(aMessage);
                    break;
                default:
                    logger.Error(aMessage);
                    break;
            }            
        }

        /// <summary>
        /// Change output file for logging. Accepts NLog placeholders such as {basedir}.
        /// Use '/' for path element separators.
        /// </summary>
        /// <param name="sLogFilePath">log file path</param>
        public static void SetLogFilePath(string sLogFilePath)
        {
            // Name of target must match name property in <target xsi:type="File" name="f" ... /> of NLog.config
            var target = (FileTarget)LogManager.Configuration.FindTargetByName("f");
            if (target != null)
            {
                target.FileName = sLogFilePath;
                LogManager.ReconfigExistingLoggers();
            }
        }
    }   
}
