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
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.AccountManager.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;


namespace Smartrac.SmartCosmos.AccountManager.Role
{
    public class RoleEndpoint : BaseAccountManagerEndpoint, IRoleEndpoint
    {
        /// <summary>
        /// Creates a role within a directory.
        /// </summary>
        /// <param name="name">Name of the role</param>
        /// <param name="responseData">result</param>
        /// <returns>RoleActionResult</returns>
        public RoleActionResult Create(RoleNameRequest name, out DefaultResponse responseData)
        {
            responseData = null;
            if ((null == name) || !name.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return RoleActionResult.Failed;
            }

            Uri url = new Uri("/roles", UriKind.Relative);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<RoleNameRequest, DefaultResponse>(request, name, out responseData, WebRequestMethods.Http.Put);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.Created: return RoleActionResult.Successful;
                    default: return RoleActionResult.Failed;
                }
            }

            return RoleActionResult.Failed;
        }

        /// <summary>
        /// Gets all roles from an application that belong to a single directory.
        /// </summary>
        /// <param name="responseData">result</param>
        /// <returns>RoleActionResult</returns>
        public RoleActionResult Lookup(out RolesResponse responseData)
        {
            responseData = null;
            
            Uri url = new Uri("/roles", UriKind.Relative);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<RolesResponse>(request, out responseData, WebRequestMethods.Http.Get);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return RoleActionResult.Successful;
                    default: return RoleActionResult.Failed;
                }
            }

            return RoleActionResult.Failed;
        }

        /// <summary>
        /// Deletes a role within a directory.
        /// </summary>
        /// <param name="roleUrn"></param>
        /// <param name="responseData">result</param>
        /// <returns>RoleActionResult</returns>
        public RoleActionResult Delete(Urn roleUrn, out DefaultResponse responseData)
        {
            responseData = null;
            if ((null == roleUrn) || !roleUrn.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return RoleActionResult.Failed;
            }

            Uri url = new Uri("/roles", UriKind.Relative).
                AddSubfolder(roleUrn.UUID);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            request.Method = "DELETE";
            HttpWebResponse response = request.GetResponseSafe() as System.Net.HttpWebResponse;
            if (response != null)
            {
                try
                {
                    if ((response.StatusCode == HttpStatusCode.NoContent)) 
                    {
                        return RoleActionResult.Successful;
                    }
                    else
                    {
                        responseData = responseData.FromJSON(response.GetResponseStream());
                        if (responseData is IHttpStatusCode)
                        {
                            (responseData as IHttpStatusCode).HTTPStatusCode = response.StatusCode;
                        }
                    }
                }
                finally
                {
                    response.Close();
                }
            }
            return RoleActionResult.Failed;
        }
    }
}
