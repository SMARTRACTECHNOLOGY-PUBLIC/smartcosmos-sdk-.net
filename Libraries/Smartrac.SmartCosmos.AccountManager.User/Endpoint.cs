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
using System.Net;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.AccountManager.User
{
    /// <summary>
    /// User's Accounts Endpoints
    /// </summary>
    public class UserEndpoint : BaseAccountManagerEndpoint, IUserEndpoint
    {
        /// <summary>
        /// Creates a user account and stores it.
        /// </summary>
        /// <param name="requestData">User data</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        public UserActionResult Create(UserRequest requestData, out DefaultResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return UserActionResult.Failed;
            }

            Uri url = new Uri("/users", UriKind.Relative);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<UserRequest, DefaultResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.Created: return UserActionResult.Successful;
                    default: return UserActionResult.Failed;
                }
            }

            return UserActionResult.Failed;
        }

        /// <summary>
        /// Returns user's account information.
        /// </summary>
        /// <param name="userEmail">User email</param>
        /// <param name="responseData">User data</param>
        /// <param name="showRoles">Show or not user roles</param>
        /// <returns>UserActionResult</returns>
        public UserActionResult Lookup(Email userEmail, out DefaultResponse responseData, bool showRoles = false)
        {
            responseData = null;
            if ((null == userEmail) || !userEmail.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return UserActionResult.Failed;
            }

            Uri url = new Uri("/users/user", UriKind.Relative)
                .AddSubfolder(userEmail.Mail)
                .AddQuery("roles", showRoles.ToString());

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<DefaultResponse>(request, out responseData, WebRequestMethods.Http.Get);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return UserActionResult.Successful;
                    default: return UserActionResult.Failed;
                }
            }
            return UserActionResult.Failed;
        }

        /// <summary>
        /// Send a reset password email.
        /// </summary>
        /// <param name="userEmail">User email</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        public UserActionResult ResetLostPassword(Email userEmail, out DefaultResponse responseData)
        {
            responseData = null;
            if (null == userEmail || !userEmail.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return UserActionResult.Failed;
            }

            Uri url = new Uri("/users/user", UriKind.Relative)
                .AddSubfolder(userEmail.Mail)
                .AddSubfolder("reset");

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<object, DefaultResponse>(request, null, out responseData, WebRequestMethods.Http.Post);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return UserActionResult.Successful;
                    default: return UserActionResult.Failed;
                }
            }
            return UserActionResult.Failed;
        }

        /// <summary>
        /// Updates user's account information.
        /// </summary>
        /// <param name="userData">User data</param>
        /// <param name="userEmail">email</param>
        /// <param name="responseData">result</param>
        /// <returns></returns>
        public UserActionResult Update(UserUpdateRequest userData, Email userEmail, out DefaultResponse responseData)
        {
            responseData = null;
            if (null == userData || null == userEmail || !userEmail.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return UserActionResult.Failed;
            }

            Uri url = new Uri("/users/user", UriKind.Relative)
                .AddSubfolder(userEmail.Mail);

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<UserUpdateRequest, DefaultResponse>(request, userData, out responseData, WebRequestMethods.Http.Post);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return UserActionResult.Successful;
                    default: return UserActionResult.Failed;
                }
            }
            return UserActionResult.Failed;
        }

        /// <summary>
        /// Assigns a role to a user, both within the same directory.
        /// </summary>
        /// <param name="userEmail">email</param>
        /// <param name="roleUrns">List of roles URNs</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        public UserActionResult Assign(Email userEmail, List<UserRoleUrn> roleUrns, out UserRolesResponse responseData)
        {
            responseData = null;
            if ((null == userEmail) || !userEmail.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return UserActionResult.Failed;
            }

            Uri url = new Uri("/users/user", UriKind.Relative)
                .AddSubfolder(userEmail.Mail)
                .AddSubfolder("roles");

            var request = CreateWebRequest(url, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<List<UserRoleUrn>, UserRolesResponse>(request, roleUrns, out responseData, WebRequestMethods.Http.Post);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return UserActionResult.Successful;
                    default: return UserActionResult.Failed;
                }
            }
            return UserActionResult.Failed;
        }

        /// <summary>
        /// Removes a role from a user, both within the same directory.
        /// </summary>
        /// <param name="userEmail">User email</param>
        /// <param name="roleUrn">Role URN</param>
        /// <param name="responseData">result</param>
        /// <returns>UserActionResult</returns>
        public UserActionResult Dissociate(Email userEmail, Urn roleUrn, out DefaultResponse responseData)
        {
            responseData = null;
            if (null == roleUrn || !roleUrn.IsValid() || null == userEmail || !userEmail.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return UserActionResult.Failed;
            }

            Uri url = new Uri("/users/user", UriKind.Relative).
                AddSubfolder(userEmail.Mail).
                AddSubfolder("roles").
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
                        return UserActionResult.Successful;
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
            return UserActionResult.Failed;
        }
    }
}
