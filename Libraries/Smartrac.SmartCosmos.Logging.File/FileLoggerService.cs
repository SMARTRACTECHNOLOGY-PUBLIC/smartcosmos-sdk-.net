#region License

// SMART COSMOS .Net SDK
// (C) Copyright 2013 - 2015 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion License

using NLog;
using NLog.Targets;

namespace Smartrac.SmartCosmos.Logging.File
{
    /// <summary>
    /// Log to file service
    /// </summary>
    public class FileLoggerService : BaseMessageLogger
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private LogLevel MapLogLevel(LogType logType)
        {
            switch (logType)
            {
                case LogType.Debug: return NLog.LogLevel.Debug;
                case LogType.Error: return NLog.LogLevel.Error;
                case LogType.Fatal: return NLog.LogLevel.Fatal;
                case LogType.Info: return NLog.LogLevel.Info;
                case LogType.Warning: return NLog.LogLevel.Warn;
                default: return NLog.LogLevel.Error;
            }
        }

        public override bool CanLog(LogType logType)
        {
            return base.CanLog(logType) && (logger != null) && logger.IsEnabled(MapLogLevel(logType));
        }

        protected override void DoAddLog(string message, LogType logType = LogType.Debug)
        {
            if (logger == null)
                return;
            logger.Log(MapLogLevel(logType), message);
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