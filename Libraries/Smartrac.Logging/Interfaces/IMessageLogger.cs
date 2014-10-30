using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.Logging
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
        void AddLog(string aMessage, LogType aLogType = LogType.Debug);
        bool CanLog(LogType aLogType);
    }
}