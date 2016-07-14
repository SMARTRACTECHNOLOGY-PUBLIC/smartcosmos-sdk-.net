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

using Smartrac.SmartCosmos.Base;
using Smartrac.SmartCosmos.Logging;
using System;
using System.Diagnostics.Contracts;

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

        /// <summary>
        /// keep connection alive
        /// </summary>
        /// <param name="keepAlive"></param>
        /// <returns></returns>
        public E setKeepAlive(bool keepAlive)
        {
            instance.KeepAlive = keepAlive;
            return this as E;
        }

        /// <summary>
        /// If set (CookieKey and CookieValueand CookieDomain), each call will contains a cookie
        /// </summary>
        public E setCookieValue(string cookieValue)
        {
            instance.CookieValue = cookieValue;
            return this as E;
        }

        /// <summary>
        /// If set (CookieKey and CookieValueand CookieDomain), each call will contains a cookie
        /// </summary>
        public E setCookieKey(string cookieKey)
        {
            instance.CookieKey = cookieKey;
            return this as E;
        }

        /// <summary>
        /// If set (CookieKey and CookieValue and CookieDomain), each call will contains a cookie
        /// </summary>
        public E setCookieDomain(string cookieDomain)
        {
            instance.CookieDomain = cookieDomain;
            return this as E;
        }

        /// <summary>
        /// set service url
        /// </summary>
        /// <param name="serverURL"></param>
        /// <returns></returns>
        public E setServerURL(string serverURL)
        {
            instance.ServerURL = serverURL;
            return this as E;
        }

        /// <summary>
        /// allo invalid server certificates
        /// </summary>
        /// <param name="allowInvalidServerCertificates"></param>
        /// <returns></returns>
        public E setAllowInvalidServerCertificates(bool allowInvalidServerCertificates)
        {
            instance.AllowInvalidServerCertificates = allowInvalidServerCertificates;
            return this as E;
        }

        /// <summary>
        /// Set client accept language
        /// </summary>
        /// <param name="acceptLanguage"></param>
        /// <returns></returns>
        public E setAcceptLanguage(string acceptLanguage)
        {
            instance.AcceptLanguage = acceptLanguage;
            return this as E;
        }

        /// <summary>
        /// set logger
        /// </summary>
        /// <param name="logger">logger</param>
        /// <returns>object</returns>
        public E setLogger(IMessageLogger logger)
        {
            instance.Logger = logger;
            return this as E;
        }

        /// <summary>
        /// set credentials
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPassword"></param>
        /// <returns></returns>
        public E setUserAccount(string userName, string userPassword)
        {
            instance.setUserAccount(userName, userPassword);
            return this as E;
        }

        /// <summary>
        /// Validate parameters
        /// </summary>
        protected override void onValidate()
        {
            Contract.Requires(!String.IsNullOrEmpty(instance.ServerURL), "Server URL is required");
        }
    }
}