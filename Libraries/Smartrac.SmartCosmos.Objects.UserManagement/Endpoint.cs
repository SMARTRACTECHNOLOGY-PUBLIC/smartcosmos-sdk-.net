﻿#region License

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
using System.Net;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.UserManagement
{
    /// <summary>
    /// Client for UserManagement Endpoints
    /// </summary>
    internal class UserManagementEndpoint : BaseObjectsEndpoint, IUserManagementEndpoint
    {
        /// <summary>
        /// Create a new user associated with the specified email address
        /// </summary>
        /// <param name="requestData">user data</param>
        /// <param name="responseData">result</param>
        /// <returns>UserManagementActionResult</returns>
        public UserActionResult CreateNewUser(UserManagementRequest requestData, out UserManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return UserActionResult.Failed;
                }

                var request = CreateWebRequest("/users", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<UserManagementRequest, UserManagementResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                            responseData.userUrn = new Urn(responseData.message);
                            return UserActionResult.Successful;

                        case HttpStatusCode.Conflict:
                            responseData.userUrn = new Urn(responseData.message);
                            return UserActionResult.Conflict;

                        default: return UserActionResult.Failed;
                    }
                }

                return UserActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserActionResult.Failed;
            }
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="requestData">user data</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>UserManagementActionResult</returns>
        public UserActionResult UpdateUser(UserManagementRequest requestData, out UserManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return UserActionResult.Failed;
                }

                var request = CreateWebRequest("/users", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<UserManagementRequest, UserManagementResponse>(request, requestData, out responseData);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent: return UserActionResult.Successful;
                        case HttpStatusCode.BadRequest: return UserActionResult.Failed;
                        default: return UserActionResult.Failed;
                    }
                }

                return UserActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific user by its system-assigned URN key
        /// </summary>
        /// <param name="userUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">user data</param>
        /// <returns>UserManagementActionResult</returns>
        public UserActionResult LookupSpecificUser(Urn userUrn, ViewType? viewType, out UserDataResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == userUrn) || (!userUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return UserActionResult.Failed;
                }

                string viewTypeParam = ((null != viewType) && viewType.HasValue) ? "?view=" + viewType.Value.GetDescription() : "";
                var request = CreateWebRequest("/users/" + userUrn.UUID + viewTypeParam, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<UserDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return UserActionResult.Successful;
                return UserActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific user by their email address
        /// </summary>
        /// <param name="eMailAddress">Exact case-sensitive email address to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">user data</param>
        /// <returns>UserManagementActionResult</returns>
        public UserActionResult LookupSpecificUser(string eMailAddress, ViewType? viewType, out UserDataResponse responseData)
        {
            responseData = null;
            try
            {
                if (String.IsNullOrEmpty(eMailAddress))
                {
                    if (null != Logger)
                        Logger.AddLog("eMail address is not valid", LogType.Error);
                    return UserActionResult.Failed;
                }

                string viewTypeParam = ((null != viewType) && viewType.HasValue) ? "?view=" + viewType.Value.GetDescription() : "";
                var request = CreateWebRequest("/users/user/" + eMailAddress + viewTypeParam, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<UserDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return UserActionResult.Successful;

                return UserActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserActionResult.Failed;
            }
        }

        /// <summary>
        /// Initiate a reset password workflow or specifically define the user's password
        /// </summary>
        /// <param name="eMailAddress">eMailAddress</param>
        /// /// <param name="eMailAddress">optional: newPassword</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>UserManagementActionResult</returns>
        public UserActionResult ChangeOrResetUserPassword(ChangeOrResetUserPasswordRequest requestData, out ChangeOrResetUserPasswordResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return UserActionResult.Failed;
                }

                var request = CreateWebRequest("/users/user", WebRequestOption.Authorization);
                var returnHTTPStatusCode = ExecuteWebRequestJSON<ChangeOrResetUserPasswordRequest, ChangeOrResetUserPasswordResponse>(
                    request, requestData, out responseData);

                switch (returnHTTPStatusCode)
                {
                    case HttpStatusCode.NoContent: return UserActionResult.Successful;
                    case HttpStatusCode.BadRequest: return UserActionResult.Failed;
                    default: return UserActionResult.Failed;
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserActionResult.Failed;
            }
        }
    }
}