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
using System.Security.Cryptography;
using System.Text;

namespace Smartrac.SmartCosmos.Profiles.Base
{
    /// <summary>
    /// Base class for all SMART COSMOS Profile endpoints
    /// </summary>
    public class BaseProfileEndpoint : BaseEndpoint
    {
        public BaseProfileEndpoint()
            : base()
        {
            this.ServiceSubUrl = "/profiles/rest";
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
                if (AuthorizationToken != "")
                {
                    if (null != Logger)
                        Logger.AddLog("Clear authorization token");

                    AuthorizationToken = "";
                }
                return;
            }

            //if (null != Logger)
            //    Logger.AddLog("Login with user " + userName);

            // UserName and hased password are combined into a string "UserName:hashedpassword"
            // For example, if the user agent uses 'Aladdin' as the UserName and 'open sesame' as the password then the header is formed as follows:.
            // SHA512 hash of the password: 8470cdd3bf1ef85d5f092bce5ae5af97ce50820481bf43b2413807fec37e2785b533a65d4c7d71695b141d81ebcd4b6c4def4284e6067f0b9ddc318b1b230205
            // Authorization: Basic QWxhZGRpbjo4NDcwY2RkM2JmMWVmODVkNWYwOTJiY2U1YWU1YWY5N2NlNTA4MjA0ODFiZjQzYjI0MTM4MDdmZWMzN2UyNzg1YjUzM2E2NWQ0YzdkNzE2OTViMTQxZDgxZWJjZDRiNmM0ZGVmNDI4NGU2MDY3ZjBiOWRkYzMxOGIxYjIzMDIwNQ==
            AuthorizationToken =
                "Basic " +
                Convert.ToBase64String(
                  Encoding.UTF8.GetBytes(
                    userName +
                    ":" +
                    BitConverter.ToString(
                      SHA512.Create().ComputeHash(
                        Encoding.UTF8.GetBytes(userPassword)
                      )
                    ).Replace("-", "").ToLower()));
        }
    }
}