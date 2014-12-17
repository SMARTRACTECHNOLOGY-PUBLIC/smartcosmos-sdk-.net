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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.DataContextFactory;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Smartrac.SmartCosmos.TestCase.Base
{
    public class BaseTestCase<C, E> : ITestCase
        where C : IBaseDataContext
        where E : IBaseEndpoint
    {
        protected C dataContext;
        protected E endpoint;

        protected IMessageLogger Logger { get; set; }

        protected IEndpointFactory EndpointFactory { get; set; }

        protected IDataContextFactory DataContextFactory { get; set; }

        private Stopwatch stopwatch;

        /// <summary>
        /// Execute test case
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="endpointFactory"></param>
        /// <param name="dataContextFactory"></param>
        /// <returns></returns>
        public bool Run(IMessageLogger logger, IEndpointFactory endpointFactory, IDataContextFactory dataContextFactory)
        {
            Logger = logger;
            try
            {
                EndpointFactory = endpointFactory;
                DataContextFactory = dataContextFactory;
                if (!OnBeforeRun())
                    return true; // maybe deactivated or not all parameters available
                try
                {
                    if (!PrepareDataContext())
                        return false;

                    if (!PrepareEndpoint())
                        return false;

                    return ExecuteTests();
                }
                finally
                {
                    OnAfterRun();
                }
            }
            catch (Exception e)
            {
                if (Logger != null)
                    Logger.AddLog("Run" + e.Message, LogType.Error);
                return false;
            }
        }

        protected virtual bool PrepareDataContext()
        {
            try
            {
                dataContext = CreateDataContext();
                if (dataContext == null)
                {
                    Logger.AddLog("");
                    Logger.AddLog("Skip test cases in " + this.GetType().Name + ", because of missing data context", LogType.Info);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                if (Logger != null)
                    Logger.AddLog("PrepareDataContext: " + e.Message, LogType.Error);
                return false;
            }
        }

        protected virtual C CreateDataContext()
        {
            throw new NotImplementedException();
        }

        protected virtual bool PrepareEndpoint()
        {
            try
            {
                endpoint = CreateEndpoint();
                if (endpoint == null)
                {
                    Logger.AddLog("");
                    Logger.AddLog("Skip test cases, because of missing endpoint", LogType.Info);
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                if (Logger != null)
                    Logger.AddLog("PrepareDataContext: " + e.Message, LogType.Error);
                return false;
            }
        }

        protected virtual E CreateEndpoint()
        {
            throw new NotImplementedException();
        }

        protected virtual bool ExecuteTests()
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

        protected void OnAfterTest(Enum testResult)
        {
            stopwatch.Stop();
            Logger.AddLog("Required time: " + stopwatch.Elapsed);
            Logger.AddLog("Result: " + testResult);
            Logger.AddLog("");
            Logger.AddLog("");
        }
    }
}