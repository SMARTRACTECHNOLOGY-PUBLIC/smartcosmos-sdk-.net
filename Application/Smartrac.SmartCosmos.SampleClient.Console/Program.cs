#region License
// SMART COSMOS Profiles SDK
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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Smartrac.Logging;
using Smartrac.Logging.Console;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.TestSuite;
using Smartrac.SmartCosmos.TestSuite.Sample;
using Smartrac.SmartCosmos.DataContext;
using Smartrac.SmartCosmos.DataContext.Sample;
using System.Configuration;

namespace Smartrac.SmartCosmos.SampleClient.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // define output for logging
            IMessageLogger logger = new ConsoleLoggerService();

            // Create default factory for endpoints
            IEndpointFactory factory = new EndpointFactory(logger);

            // user settings
            // NOTE: please enter your SmartCosmos user name and password in the app.config
            factory.UserName = ConfigurationManager.AppSettings["UserName"]; 
            factory.UserPassword = ConfigurationManager.AppSettings["UserPassword"];      

            // create data context with sample data
            IDataContext dataContext = new SampleDataContext();

            // initate tester suite
            ISampleTestSuite testSuite = new SampleTestSuiteBuilder()
                                            .setLogger(logger)
                                            .setDataContext(dataContext)
                                            .setFactory(factory)
                                            .setRunPerformanceTests(true) // define if performance test should be executed
                                            .build();

            // START TESTING ----
            testSuite.Run();

            System.Console.WriteLine("Press a key for exit...");
            System.Console.ReadLine();
        }

    }

}
