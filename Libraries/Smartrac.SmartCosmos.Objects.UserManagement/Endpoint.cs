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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.UserManagement
{
    /// <summary>
    /// Client for UserManagement Endpoints
    /// </summary>
    class UserManagementEndpoint : BaseObjectsEndpoint, IUserManagementEndpoint
    {
        /// <summary>
        /// Create a new user associated with the specified email address
        /// </summary>
        /// <param name="requestData">user data</param>
        /// <param name="responseData">result</param>
        /// <returns>UserManagementActionResult</returns>
        public UserManagementActionResult CreateNewUser(UserManagementRequest requestData, out UserManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return UserManagementActionResult.Failed;
                }

                var request = CreateWebRequest("/users", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(UserManagementRequest), requestData, typeof(UserManagementResponse), out responseDataObj, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as UserManagementResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.Created:
                                responseData.userUrn = new Urn(responseData.message);
                                return UserManagementActionResult.Successful;
                            case HttpStatusCode.Conflict:
                                responseData.userUrn = new Urn(responseData.message);
                                return UserManagementActionResult.Conflict;
                            default: return UserManagementActionResult.Failed;
                        }
                    }
                }

                return UserManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserManagementActionResult.Failed;
            }
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="requestData">user data</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>UserManagementActionResult</returns>
        public UserManagementActionResult UpdateUser(UserManagementRequest requestData, out UserManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return UserManagementActionResult.Failed;
                }

                var request = CreateWebRequest("/users", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(UserManagementRequest), requestData, typeof(UserManagementResponse), out responseDataObj, WebRequestMethods.Http.Post);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as UserManagementResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.NoContent: return UserManagementActionResult.Successful;
                            case HttpStatusCode.BadRequest: return UserManagementActionResult.Failed;
                            default: return UserManagementActionResult.Failed;
                        }
                    }
                }

                return UserManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserManagementActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific user by its system-assigned URN key
        /// </summary>
        /// <param name="userUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">user data</param>
        /// <returns>UserManagementActionResult</returns>
        public UserManagementActionResult LookupSpecificUser(Urn userUrn, ViewType? viewType, out UserDataResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == userUrn) || (!userUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return UserManagementActionResult.Failed;
                }

                string viewTypeParam = ((null != viewType) && viewType.HasValue) ? "?view=" + viewType.Value.GetDescription() : "";
                var request = CreateWebRequest("/users/" + userUrn.UUID + viewTypeParam, WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(UserDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as UserDataResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return UserManagementActionResult.Successful;
                }
                return UserManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserManagementActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific user by their email address
        /// </summary>
        /// <param name="eMailAddress">Exact case-sensitive email address to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">user data</param>
        /// <returns>UserManagementActionResult</returns>
        public UserManagementActionResult LookupSpecificUser(string eMailAddress, ViewType? viewType, out UserDataResponse responseData)
        {
            responseData = null;
            try
            {
                if (String.IsNullOrEmpty(eMailAddress))
                {
                    if (null != Logger)
                        Logger.AddLog("eMail address is not valid", LogType.Error);
                    return UserManagementActionResult.Failed;
                }

                string viewTypeParam = ((null != viewType) && viewType.HasValue) ? "?view=" + viewType.Value.GetDescription() : "";
                var request = CreateWebRequest("/users/user/" + eMailAddress + viewTypeParam, WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(UserDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as UserDataResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return UserManagementActionResult.Successful;
                }
                return UserManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserManagementActionResult.Failed;
            }
        }

        /// <summary>
        /// Initiate a reset password workflow or specifically define the user's password
        /// </summary>
        /// <param name="eMailAddress">eMailAddress</param>
        /// /// <param name="eMailAddress">optional: newPassword</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>UserManagementActionResult</returns>
        public UserManagementActionResult ChangeOrResetUserPassword(ChangeOrResetUserPasswordRequest requestData, out ChangeOrResetUserPasswordResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return UserManagementActionResult.Failed;
                }

                var request = CreateWebRequest("/users/user", WebRequestOption.Authorization);
                object responseDataObj = null;
                var returnHTTPStatusCode = ExecuteWebRequestJSON(request, typeof(ChangeOrResetUserPasswordRequest), requestData, typeof(ChangeOrResetUserPasswordResponse), out responseDataObj, WebRequestMethods.Http.Post);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ChangeOrResetUserPasswordResponse;
                    switch (returnHTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent: return UserManagementActionResult.Successful;
                        case HttpStatusCode.BadRequest: return UserManagementActionResult.Failed;
                        default: return UserManagementActionResult.Failed;
                    }
                }

                return UserManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return UserManagementActionResult.Failed;
            }
        }

    }
}
