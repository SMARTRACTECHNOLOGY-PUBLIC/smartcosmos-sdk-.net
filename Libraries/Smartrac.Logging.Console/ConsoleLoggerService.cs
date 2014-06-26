using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartrac.Logging.Console
{
    public sealed class ConsoleLoggerService : IMessageLogger
    {
        public void AddLog(string aMessage, LogType aLogType = LogType.Debug)
        {
            switch (aLogType)
            {
                case LogType.Debug:
                    System.Console.WriteLine(aMessage);
                    break;
                case LogType.Error:
                    System.Console.WriteLine("ERR:" + aMessage);
                    break;
                case LogType.Fatal:
                    System.Console.WriteLine("FATAL:" + aMessage);
                    break;
                case LogType.Info:
                    System.Console.WriteLine(aMessage);
                    break;
                case LogType.Warning:
                    System.Console.WriteLine("WRN:" + aMessage);
                    break;
                default:
                    System.Console.WriteLine(aMessage);
                    break;
            }   
        }
    }
}
