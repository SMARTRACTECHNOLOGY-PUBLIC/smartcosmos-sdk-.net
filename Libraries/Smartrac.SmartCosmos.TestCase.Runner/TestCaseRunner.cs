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

using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.DataContextFactory;
using Smartrac.SmartCosmos.TestCase.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Smartrac.SmartCosmos.TestCase.Runner
{
    /// <summary>
    /// Test Case for SmartCosmos
    /// </summary>
    internal class TestCaseRunner : ITestCaseRunner
    {
        public IMessageLogger Logger { get; set; }

        public IEndpointFactory EndpointFactory { get; set; }

        public IDataContextFactory DataContextFactory { get; set; }

        public TestCaseRunner()
            : base()
        {
        }

        /// <summary>
        /// Execute tests
        /// </summary>
        /// <param name="testCaseTypes">Or linked TestCaseTypes</param>
        /// <returns></returns>
        public bool Run(TestCaseType testCaseTypes, string assemblySearchPattern = "*.dll")
        {
            Logger.AddLog("Start test runner...");

            Dictionary<int, ITestCase> testList = new Dictionary<int, ITestCase>();
            bool result = true;
            bool bTestSuiteFound = false;

            // Search all test cases
            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, assemblySearchPattern))
            {
                var typesWithMyAttribute =
                from t in Assembly.LoadFile(file).GetTypes()
                //where (t.Namespace != null) && t.Namespace.Contains("TestCase")
                let attributes = t.GetCustomAttributes(typeof(TestCaseAttribute), true)
                where attributes != null && attributes.Length > 0
                select new { Type = t, Attributes = attributes.Cast<TestCaseAttribute>() };

                foreach (var item in typesWithMyAttribute)
                {
                    foreach (var attr in item.Attributes)
                    {
                        TestCaseAttribute testCase = (TestCaseAttribute)attr;
                        if ((testCase != null) && (testCaseTypes.HasFlag(testCase.TestCaseType)))
                        {
                            ITestCase MyTestCase = (ITestCase)Activator.CreateInstance(item.Type);
                            if (MyTestCase != null)
                            {
                                testList.Add(testCase.TestPriority, MyTestCase);
                            }
                        }
                    }
                }

                foreach (var testItem in testList.OrderBy(i => i.Key))
                {
                    result = testItem.Value.Run(Logger, EndpointFactory, DataContextFactory) && result;
                    bTestSuiteFound = true;
                }
                testList.Clear();
            }

            if (!bTestSuiteFound)
            {
                Logger.AddLog("No test suites found!");
                result = false;
            }

            Logger.AddLog("");
            Logger.AddLog("Test result: " + result);
            return result;
        }
    }
}