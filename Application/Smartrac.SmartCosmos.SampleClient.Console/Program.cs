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

using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.DataContextFactory;
using Smartrac.SmartCosmos.DataContextFactory.Sample;
using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Logging.Console;
using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.TestCase.Runner;
using System.Configuration;
using System.IO;

namespace Smartrac.SmartCosmos.SampleClient.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // define output for logging
            IMessageLogger logger = new ConsoleLoggerService();

            // factory for endpoints
            IEndpointFactory factory = new EndpointFactory(logger, XMLCredentialStore.ReadFromFile(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), ConfigurationManager.AppSettings["XMLCredentialStore"])));

            // data context
            IDataContextFactory dataContext = new SampleDataContextFactory(); // data context factory for sample data
            //IDataContextFactory dataContext = new XMLDataContextFactory(ConfigurationManager.AppSettings["XMLDataContextFactory"]); // data context factory for sample data

            // initate tester case runner
            ITestCaseRunner testCaseRunner = new TestCaseRunnerBuilder()
                                            .setLogger(logger) // set logger
                                            .setDataContextFactory(dataContext)
                                            .setEndpointFactory(factory) // set factory for endpoints
                                            .build();

            // START TESTING ----
            bool bTestResult = testCaseRunner.Run(TestCaseType.Functional, SmartCosmosService.Objects);

            // output
            System.Console.WriteLine("");
            System.Console.WriteLine("Press a key for exit...");
            System.Console.ReadLine();
        }
    }
}