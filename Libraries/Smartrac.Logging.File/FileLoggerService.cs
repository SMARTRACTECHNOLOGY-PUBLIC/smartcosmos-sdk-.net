#region License

// SMART COSMOS .Net SDK
// (C) Copyright 2014 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
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