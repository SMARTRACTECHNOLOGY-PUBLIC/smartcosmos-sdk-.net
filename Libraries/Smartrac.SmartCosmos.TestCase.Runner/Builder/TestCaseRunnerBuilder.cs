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

using Smartrac.SmartCosmos.Base;
using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.DataContextFactory;
using System.Diagnostics.Contracts;

namespace Smartrac.SmartCosmos.TestCase.Runner
{
    /// <summary>
    /// TestCaseRunnerBuilder
    /// </summary>
    /// <typeparam name="T">Interface of a specific object which should be build</typeparam>
    /// <typeparam name="E">Instance of the specific Builder</typeparam>
    public class TestCaseRunnerBuilder : BaseBuilder<ITestCaseRunner, TestCaseRunnerBuilder>
    {
        public TestCaseRunnerBuilder() :
            base(new TestCaseRunner())
        {
        }

        public TestCaseRunnerBuilder setLogger(IMessageLogger logger)
        {
            instance.Logger = logger;
            return this;
        }

        public TestCaseRunnerBuilder setDataContextFactory(IDataContextFactory dataContextFactory)
        {
            instance.DataContextFactory = dataContextFactory;
            return this;
        }

        public TestCaseRunnerBuilder setEndpointFactory(IEndpointFactory endpointFactory)
        {
            instance.EndpointFactory = endpointFactory;
            return this;
        }

        protected override void onValidate()
        {
            Contract.Requires(instance.DataContextFactory != null);
            Contract.Requires(instance.EndpointFactory != null);
        }
    }
}