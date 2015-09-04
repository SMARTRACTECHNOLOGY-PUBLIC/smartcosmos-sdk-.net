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
using System;

namespace Smartrac.SmartCosmos.Logging.File
{
    public class FileLoggerService : BaseMessageLogger, Smartrac.SmartCosmos.Logging.IMessageFileLogger
    {
        private Logger logger;
        public string loggerId = "f";

        public FileLoggerService(string LoggerId = "f")
            : base()
        {
            loggerId = LoggerId;
            logger = LogManager.GetLogger(loggerId);
            if (logger == null)
                throw new Exception("Logger not found in Nlog.config: " + loggerId);
        }

        protected override void DoAddLog(string aMessage, LogType aLogType = LogType.Debug)
        {
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

        public string LogFilePath
        {
            get
            {
                return SingleFileLoggerService.GetLogFilePath(loggerId);
            }
            set
            {
                SingleFileLoggerService.SetLogFilePath(value, loggerId);
            }
        }

        public void Flush()
        {
            SingleFileLoggerService.Flush();
        }

        public override string GetLoggerId()
        {
            return loggerId;
        }
    }
}