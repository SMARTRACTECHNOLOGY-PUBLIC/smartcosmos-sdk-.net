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
using System.Configuration;
using Smartrac.SmartCosmos.Profiles.DataContext;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext.Sample;
using Smartrac.SmartCosmos.Objects.DataContext.Sample;

namespace Smartrac.SmartCosmos.SampleClient.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // define output for logging
            IMessageLogger logger = new ConsoleLoggerService();
            
            IEndpointFactory factory = new EndpointFactory(logger);

            // user settings
            // NOTE: please enter your SmartCosmos user name and password in the app.config
            factory.UserName = ConfigurationManager.AppSettings["UserName"]; 
            factory.UserPassword = ConfigurationManager.AppSettings["UserPassword"];

            // initate tester suite
            ISampleTestSuite testSuite = new SampleTestSuiteBuilder()
                                            .setLogger(logger)
                                            .setTagDataContext(new SampleTagDataContext()) // create data context with sample data for tags (Profiles)
                                            .setFileDataContext(new SampleFileDataContext()) // create data context with sample data for files (Objects)
                                            .setRegistrationDataContext(new SampleRegistrationDataContext()) // create data context with sample data for registration (Objects)
                                            .setAccountManagementDataContext(new SampleAccountManagementDataContext()) // create data context with sample data for account management (Objects)
                                            .setUserManagementDataContext(new SampleUserManagementDataContext()) // create data context with sample data for user management (Objects)
                                            .setObjectManagementDataContext(new SampleObjectManagementDataContext()) // create data context with sample data for object management (Objects)
                                            .setObjectInteractionDataContext(new SampleObjectInteractionDataContext()) // create data context with sample data for object interaction (Objects)
                                            .setRelationshipManagementDataContext(new SampleRelationshipManagementDataContext()) // create data context with sample data for relationship  management (Objects)
                                            .setFactory(new EndpointFactory(logger)) // Create default factory for endpoints
                                            .setRunPerformanceTests(true) // define if performance test should be executed
                                            .build();

            // START TESTING ----
            testSuite.Run();

            System.Console.WriteLine("Press a key for exit...");
            System.Console.ReadLine();
        }

    }

}
