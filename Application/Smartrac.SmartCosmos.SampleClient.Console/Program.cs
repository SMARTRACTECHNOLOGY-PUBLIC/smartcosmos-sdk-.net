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

using System.Configuration;
using Smartrac.Logging;
using Smartrac.Logging.Console;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.DataContextFactory.Sample;
using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.TestCase.Runner;

namespace Smartrac.SmartCosmos.SampleClient.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // define output for logging
            IMessageLogger logger = new ConsoleLoggerService();

            // factory for endpoints
            IEndpointFactory factory = new EndpointFactory(logger);
            // user settings
            // NOTE: please enter your SmartCosmos user name and password in the app.config
            factory.UserName = ConfigurationManager.AppSettings["UserName"];
            factory.UserPassword = ConfigurationManager.AppSettings["UserPassword"];

            // Configure server of SMART COSMOS Objects server
            factory.ObjectsServerURL = "";

            // initate tester case runner
            ITestCaseRunner testCaseRunner = new TestCaseRunnerBuilder()
                                            .setLogger(logger) // set logger
                                            .setDataContextFactory(new SampleDataContextFactory()) // data context factory for sample data
                                            .setEndpointFactory(factory) // set factory for endpoints
                                            .build();

            // START TESTING ----
            bool bTestResult = testCaseRunner.Run(TestCaseType.Functional);

            // output
            System.Console.WriteLine("");
            System.Console.WriteLine("Test result: " + bTestResult);
            System.Console.WriteLine("Press a key for exit...");
            System.Console.ReadLine();
        }
    }
}