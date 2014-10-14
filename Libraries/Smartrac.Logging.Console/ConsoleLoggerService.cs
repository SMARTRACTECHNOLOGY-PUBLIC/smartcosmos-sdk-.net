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
    public sealed class ConsoleLoggerService : IMessageLogger
    {
        public void AddLog(string aMessage, LogType aLogType = LogType.Debug)
        {
            switch (aLogType)
            {
                case LogType.Debug:
                    System.Console.WriteLine("DBG:" + aMessage);
                    break;

                case LogType.Error:
                    System.Console.WriteLine("ERR:" + aMessage);
                    break;

                case LogType.Fatal:
                    System.Console.WriteLine("FATAL:" + aMessage);
                    break;

                case LogType.Info:
                    System.Console.WriteLine("INF:" + aMessage);
                    break;

                case LogType.Warning:
                    System.Console.WriteLine("WRN:" + aMessage);
                    break;

                default:
                    System.Console.WriteLine("???:" + aMessage);
                    break;
            }
        }
    }
}