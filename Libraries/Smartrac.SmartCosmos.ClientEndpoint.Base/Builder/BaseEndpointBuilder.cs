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

using System;
using System.Diagnostics.Contracts;
using Smartrac.Base;
using Smartrac.Logging;

namespace Smartrac.SmartCosmos.ClientEndpoint.Base
{
    /// <summary>
    /// Base builder for endpoints
    /// </summary>
    /// <typeparam name="T">Interface of a specific object which should be build</typeparam>
    /// <typeparam name="E">Instance of the specific Builder</typeparam>
    public class BaseEndpointBuilder<T, E> : BaseBuilder<T, E>
        where T : IBaseEndpoint
        where E : class
    {
        public BaseEndpointBuilder(T instance)
            : base(instance)
        {
        }

        public E setKeepAlive(bool keepAlive)
        {
            instance.KeepAlive = keepAlive;
            return this as E;
        }

        public E setServerURL(string serverURL)
        {
            instance.ServerURL = serverURL;
            return this as E;
        }

        public E setAllowInvalidServerCertificates(bool allowInvalidServerCertificates)
        {
            instance.AllowInvalidServerCertificates = allowInvalidServerCertificates;
            return this as E;
        }

        public E setAcceptLanguage(string acceptLanguage)
        {
            instance.AcceptLanguage = acceptLanguage;
            return this as E;
        }

        public E setLogger(IMessageLogger logger)
        {
            instance.Logger = logger;
            return this as E;
        }

        public E setUserAccount(string userName, string userPassword)
        {
            instance.setUserAccount(userName, userPassword);
            return this as E;
        }

        protected override void onValidate()
        {
            Contract.Requires(!String.IsNullOrEmpty(instance.ServerURL));
        }
    }
}