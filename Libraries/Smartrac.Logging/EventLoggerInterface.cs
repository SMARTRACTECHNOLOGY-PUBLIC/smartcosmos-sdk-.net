using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.Logging
{
    public enum LogType
    {

        Debug, //     Debug log level.        
        Error, //     Error log level.        
        Fatal, //     Fatal log level.
        Info,  //     Info log level.
        Warning //    Warning log level.
    }

    public interface IMessageLogger
    {
        void AddLog(string aMessage, LogType aLogType = LogType.Debug);
    }
}
