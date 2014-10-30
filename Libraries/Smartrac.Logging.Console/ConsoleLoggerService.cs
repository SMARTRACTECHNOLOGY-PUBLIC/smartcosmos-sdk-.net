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

namespace Smartrac.Logging.Console
{
    public class ConsoleLoggerService : BaseMessageLogger
    {
        public ConsoleLoggerService(LogType logLevel) 
            : base(logLevel)
        {
            System.Console.BufferHeight = 5000;
        }

        public ConsoleLoggerService()
            : this(LogType.Debug)
        {
        }

        protected override void DoAddLog(string message, LogType logType = LogType.Debug)
        {
            switch (logType)
            {
                case LogType.Debug:
                    System.Console.WriteLine(message);
                    break;

                case LogType.Error:
                    System.Console.WriteLine("ERR:" + message);
                    break;

                case LogType.Fatal:
                    System.Console.WriteLine("FATAL:" + message);
                    break;

                case LogType.Info:
                    System.Console.WriteLine(message);
                    break;

                case LogType.Warning:
                    System.Console.WriteLine("WRN:" + message);
                    break;

                default:
                    System.Console.WriteLine("???:" + message);
                    break;
            }
        }
    }
}