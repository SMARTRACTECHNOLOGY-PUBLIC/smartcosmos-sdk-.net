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
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using Smartrac.Base;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Factory;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Profiles.DataContext;

namespace Smartrac.SmartCosmos.TestSuite
{
    /// <summary>
    /// Base builder for endpoints
    /// </summary>
    /// <typeparam name="T">Interface of a specific object which should be build</typeparam>
    /// <typeparam name="E">Instance of the specific Builder</typeparam>
    public class TestSuiteBuilder<T, E> : BaseBuilder<T, E>
        where T : ITestSuite
        where E : class
    {
        public TestSuiteBuilder(T instance)
            : base(instance)
        {
        }

        public E setLogger(IMessageLogger logger)
        {
            instance.Logger = logger;
            return this as E;
        }


        public E setTagDataContext(ITagDataContext dataContext)
        {
            instance.TagDataContext = dataContext;
            return this as E;
        }

        public E setFileDataContext(IFileDataContext dataContext)
        {
            instance.FileDataContext = dataContext;
            return this as E;
        }

        public E setRegistrationDataContext(IRegistrationDataContext dataContext)
        {
            instance.RegistrationDataContext = dataContext;
            return this as E;
        }

        public E setAccountManagmentDataContext(IAccountManagmentDataContext dataContext)
        {
            instance.AccountManagmentDataContext = dataContext;
            return this as E;
        }


        public E setUserManagmentDataContext(IUserManagmentDataContext dataContext)
        {
            instance.UserManagmentDataContext = dataContext;
            return this as E;
        }
        
        public E setFactory(IEndpointFactory factory)
        {
            instance.Factory = factory;
            return this as E;
        }

        protected override void onValidate()
        {
            Contract.Requires(instance.TagDataContext != null);
            Contract.Requires(instance.Factory != null);
        }
    }
}
