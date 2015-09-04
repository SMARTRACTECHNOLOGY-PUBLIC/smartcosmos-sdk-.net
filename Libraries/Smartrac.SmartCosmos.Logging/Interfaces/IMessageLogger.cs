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

using System;

namespace Smartrac.SmartCosmos.Logging
{
    [Flags]
    public enum LogType
    {
        Debug = 1, //     Debug log level.
        Info = 2,  //     Info log level.        
        Warning = 4, //    Warning log level.
        Error = 8, //     Error log level.
        Fatal = 16 //     Fatal log level
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

        /// <summary>
        /// "Unique" logger id
        /// </summary>
        /// <returns>logger id</returns>
        string GetLoggerId();
    }

    public interface IMessageFileLogger : IMessageLogger
    {
        string LogFilePath { get; set; }

        void Flush();
    }
}