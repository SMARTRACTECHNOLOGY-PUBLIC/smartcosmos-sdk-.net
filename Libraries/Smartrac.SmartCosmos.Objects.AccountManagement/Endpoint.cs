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

namespace Smartrac.SmartCosmos.Objects.AccountManagement
{
    /// <summary>
    /// Client for AccountManagement Endpoints
    /// </summary>
    class AccountManagementEndpoint : BaseObjectsEndpoint, IAccountManagementEndpoint
    {
        /// <summary>
        /// Lookup my account details
        /// </summary>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        public AccountManagementActionResult GetAccountDetails(ViewType? viewType, out AccountDetailsResponse responseData)
        {
            responseData = null;
            try
            {
                string viewTypeParam = ((null != viewType) && viewType.HasValue) ? "?view=" + viewType.Value.GetDescription() : "";

                var request = CreateWebRequest("/account" + viewTypeParam, WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(AccountDetailsResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as AccountDetailsResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return AccountManagementActionResult.Successful;
                }
                return AccountManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return AccountManagementActionResult.Failed;
            }
        }

        /// <summary>
        /// Change the authenticated user's password, presuming they know their existing password to change to a new password.
        /// </summary>
        /// <param name="requestData">old and new password</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        public AccountManagementActionResult ChangeYourPassword(ChangeYourPasswordRequest requestData, out ChangeYourPasswordResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Change Your Password: required data is missing!", LogType.Error);
                    return AccountManagementActionResult.Failed;
                }

                var request = CreateWebRequest("/account/password/change");
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(ChangeYourPasswordRequest), requestData, typeof(ChangeYourPasswordResponse), out responseDataObj, WebRequestMethods.Http.Post);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ChangeYourPasswordResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return AccountManagementActionResult.Successful;
                }

                return AccountManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return AccountManagementActionResult.Failed;
            }
        }


        /// <summary>
        /// Trigger a password reset workflow via email for the specified Account associated with the indicated email address.
        /// </summary>
        /// <param name="requestData">contains your emailAddress</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        public AccountManagementActionResult ResetLostPassword(ResetLostPasswordRequest requestData, out ResetLostPasswordResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Reset Lost Password: required data is missing!", LogType.Error);
                    return AccountManagementActionResult.Failed;
                }

                var request = CreateWebRequest("/account/password/change");
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(ResetLostPasswordRequest), requestData, typeof(ResetLostPasswordResponse), out responseDataObj, WebRequestMethods.Http.Post);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ResetLostPasswordResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return AccountManagementActionResult.Successful;
                }

                return AccountManagementActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return AccountManagementActionResult.Failed;
            }
        }
    }
}