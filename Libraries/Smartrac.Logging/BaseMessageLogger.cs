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

using System;
namespace Smartrac.Logging
{
    /// <summary>
    /// Base class for MessageLogger
    /// </summary>
    public class BaseMessageLogger : IMessageLogger
    {
        public LogType LogLevel = LogType.Debug;

        public BaseMessageLogger(LogType logLevel)
            : base()
        {
            this.LogLevel = logLevel;
        }

        public BaseMessageLogger()
            : this(LogType.Debug)
        {
        }

        public virtual bool CanLog(LogType logType)
        {
            return (int)logType <= (int)LogLevel;
        }

        public void AddLog(string message, LogType logType = LogType.Debug)
        {
            if (!CanLog(logType))
                return;

            DoAddLog(message, logType);
        }

        protected virtual void DoAddLog(string message, LogType logType = LogType.Debug)
        {
            throw new NotImplementedException();
        }
    }
}