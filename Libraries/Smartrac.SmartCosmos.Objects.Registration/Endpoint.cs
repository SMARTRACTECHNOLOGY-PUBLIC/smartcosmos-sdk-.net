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

namespace Smartrac.SmartCosmos.Objects.Registration
{
    /// <summary>
    /// Client for Registration Endpoints
    /// </summary>
    class RegistrationEndpoint : BaseObjectsEndpoint, IRegistrationEndpoint
    {
        /// <summary>
        /// Check to see if the named realm is available for registration
        /// </summary>
        /// <param name="realm">Check to see if the named realm is available for registration</param>
        /// <returns>RegistrationActionResult</returns>
        public RegistrationActionResult GetRealmAvailability(string realm, out RealmAvailabilityResponse responseData)
        {
            responseData = null;
            try
            {
                if (String.IsNullOrEmpty(realm))
                {
                    if (null != Logger)
                        Logger.AddLog("realm is empty", LogType.Error);
                    return RegistrationActionResult.Failed;
                }

                var request = CreateWebRequest("/registration/realm/" + realm);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(RealmAvailabilityResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as RealmAvailabilityResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return RegistrationActionResult.Successful;
                }
                return RegistrationActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RegistrationActionResult.Failed;
            }
        }

        /// <summary>
        /// Register for a new SMART COSMOS account
        /// </summary>
        /// <param name="requestData">Account data (realm and email address)</param>
        /// <param name="responseData">Registration result</param>
        /// <returns>RegistrationActionResult</returns>
        public RegistrationActionResult AccountRegistration(AccountRegistrationRequest requestData, out AccountRegistrationResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || String.IsNullOrEmpty(requestData.emailAddress) ||  String.IsNullOrEmpty(requestData.realm))
                {
                    if (null != Logger)
                        Logger.AddLog("Account registration is incorrect or required data is missing!", LogType.Error);
                    return RegistrationActionResult.Failed;
                }

                var request = CreateWebRequest("/registration/register");
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(AccountRegistrationRequest), requestData, typeof(AccountRegistrationResponse), out responseDataObj, WebRequestMethods.Http.Post);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as AccountRegistrationResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return RegistrationActionResult.Successful;
                }

                return RegistrationActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RegistrationActionResult.Failed;
            }
        }
    }
}