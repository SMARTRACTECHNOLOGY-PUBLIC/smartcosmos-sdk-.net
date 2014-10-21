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

using System.Diagnostics;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.DataContextFactory;

namespace Smartrac.SmartCosmos.TestCase.Base
{
    public class BaseTestCase : ITestCase
    {
        protected IMessageLogger Logger { get; set; }

        protected IEndpointFactory EndpointFactory { get; set; }

        protected IDataContextFactory DataContextFactory { get; set; }

        private Stopwatch stopwatch;

        public bool Run(IMessageLogger logger, IEndpointFactory endpointFactory, IDataContextFactory dataContextFactory)
        {
            Logger = logger;
            EndpointFactory = endpointFactory;
            DataContextFactory = dataContextFactory;
            if (!OnBeforeRun())
                return true; // maybe deactivated or not all parameters available
            try
            {
                return DoRun();
            }
            finally
            {
                OnAfterRun();
            }
        }

        protected virtual bool DoRun()
        {
            return false;
        }

        protected virtual bool OnBeforeRun()
        {
            stopwatch = new Stopwatch();
            return Logger != null && EndpointFactory != null && DataContextFactory != null;
        }

        protected virtual void OnAfterRun()
        {
            stopwatch = null;
        }

        protected void OnBeforeTest(string component, string endpoint, string function)
        {
            Logger.AddLog("-----------------------");
            Logger.AddLog("Component: " + component);
            Logger.AddLog("Endpoint: " + endpoint);
            Logger.AddLog("Function: " + function);
            stopwatch.Reset();
            stopwatch.Start();
        }

        protected void OnAfterTest()
        {
            stopwatch.Stop();
            Logger.AddLog("Required time: " + stopwatch.Elapsed);
            Logger.AddLog("");
            Logger.AddLog("");
        }
    }
}