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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using System;
using System.Text;

namespace Smartrac.SmartCosmos.Objects.Base
{
    /// <summary>
    /// Base class for all SMART COSMOS Objects endpoints
    /// </summary>
    public class BaseObjectsEndpoint : BaseEndpoint
    {

        public BaseObjectsEndpoint()
            : base()
        {
            this.ServiceSubUrl = "";
            this.ServerURL = "";//https://www.smart-cosmos.com
        }
        
        /// <summary>
        /// Set the user account which should be used for the authorization
        /// </summary>
        /// <param name="UserName">User name</param>
        /// <param name="userPassword">User password</param>
        public override void setUserAccount(string userName, string userPassword)
        {
            if ((userName == "") || (userPassword == ""))
            {
                if (null != Logger)
                    Logger.AddLog("Clear authorization token");

                AuthorizationToken = "";
                return;
            }

            /*
            Several interface calls requires a HTTPS basic access authentication with your SMART COSMOS credentials.
            The authorization header is constructed as follows:
                User name and hashed password are combined into a string "username:password"
                The resulting string literal is then encoded using the RFC2045-MIME variant of Base64, except not limited to 76 char/line
                The authorization method and a space i.e. "Basic " is then put before the encoded string.
            For example, if the user agent uses 'Aladdin' as the user name and 'open sesame' as the password then the header is formed as follows:.
            Authorization: Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==
            */

            AuthorizationToken =
                "Basic " +
                Convert.ToBase64String(Encoding.UTF8.GetBytes(userName + ":" + userPassword));
        }
    }
}