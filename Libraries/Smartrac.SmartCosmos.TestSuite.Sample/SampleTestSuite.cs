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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Smartrac.Logging;
using Smartrac.SmartCosmos.TestSuite;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.Profiles.PlatformAvailability;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.Profiles.TagVerification;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.Registration;
using Smartrac.SmartCosmos.Objects.AccountManagement;
using Smartrac.SmartCosmos.Objects.UserManagement;
using Smartrac.SmartCosmos.Objects.ObjectManagement;
using Smartrac.SmartCosmos.Objects.ObjectInteraction;
using Smartrac.SmartCosmos.Objects.RelationshipManagement;
using System.Reflection;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    /// <summary>
    /// Test suite for SmartCosmos
    /// </summary>
    public class SampleTestSuite : BaseTestSuite, ISampleTestSuite
    {
        public bool RunPerformanceTests { get; set; }

        public SampleTestSuite()
        {
            RunPerformanceTests = true;
        }

        /// <summary>
        /// Execute tests
        /// </summary>
        /// <param name="testCaseTypes">Or linked TestCaseTypes</param>
        /// <returns></returns>
        public override bool Run(TestCaseType testCaseTypes)
        {
            bool result = true;

            // Search all test cases
            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.TestSuite.*.dll"))
            {
                var typesWithMyAttribute =
                from t in Assembly.LoadFile(file).GetTypes()
                where t.Namespace.Contains("TestSuite")
                let attributes = t.GetCustomAttributes(typeof(TestSuiteAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<TestSuiteAttribute>() };

                foreach (var item in typesWithMyAttribute)
                {
                    foreach (var attr in item.Attributes)
                    {
                        TestSuiteAttribute testCase = (TestSuiteAttribute)attr;
                        if ((testCase != null) && (testCaseTypes.HasFlag(testCase.testCaseType)))
                        {
                            BaseTestCase MyTestCase = (BaseTestCase)Activator.CreateInstance(item.Type);
                            if (MyTestCase != null)
                            {
                                result = MyTestCase.Run(Logger, EndpointFactory, DataContextFactory) && result;
                            }
                        }
                    }
                }
            }
            
            Logger.AddLog("");
            Logger.AddLog("Total result: " + result);
            return result;
        }
   }
}
