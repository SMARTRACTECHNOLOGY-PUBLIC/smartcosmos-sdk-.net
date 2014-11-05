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
using System.Net;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Registration
{
    /// <summary>
    /// Client for Registration Endpoints
    /// </summary>
    internal class RegistrationEndpoint : BaseObjectsEndpoint, IRegistrationEndpoint
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

                Uri url = new Uri("/registration/realm/", UriKind.Relative).
                    AddSubfolder(realm);

                var request = CreateWebRequest(url);
                var returnHTTPCode = ExecuteWebRequestJSON<RealmAvailabilityResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return RegistrationActionResult.Successful;

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
        public RegistrationActionResult RegisterAccount(AccountRegistrationRequest requestData, out AccountRegistrationResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Account registration is incorrect or required data is missing!", LogType.Error);
                    return RegistrationActionResult.Failed;
                }

                var request = CreateWebRequest("/registration/register");
                ExecuteWebRequestJSON<AccountRegistrationRequest, AccountRegistrationResponse>(request, requestData, out responseData);
                if ((responseData != null) &&
                    ((responseData.HTTPStatusCode == HttpStatusCode.Created)
                      || (responseData.HTTPStatusCode == HttpStatusCode.OK)
                      )
                    )
                    return RegistrationActionResult.Successful;

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
        /// Confirm you register for a new SMART COSMOS account
        /// </summary>
        /// <param name="requestData">Confirm account data (token and email address)</param>
        /// <param name="responseData">Confirm registration result</param>
        /// <returns>RegistrationActionResult</returns>
        public RegistrationActionResult ConfirmAccountRegistration(ConfirmRegistrationRequest requestData, out ConfirmRegistrationResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request is incorrect or required data is missing!", LogType.Error);
                    return RegistrationActionResult.Failed;
                }

                Uri url = new Uri("/registration/confirm", UriKind.Relative).
                    AddSubfolder(requestData.emailVerificationToken).
                    AddSubfolder(requestData.emailAddress);

                var request = CreateWebRequest(url);
                ExecuteWebRequestJSON<ConfirmRegistrationResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return RegistrationActionResult.Successful;
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