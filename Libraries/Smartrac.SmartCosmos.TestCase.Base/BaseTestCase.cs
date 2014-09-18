using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
                return false;
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
