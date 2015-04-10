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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Flows.Base;
using Smartrac.SmartCosmos.Logging;
using System;
using System.Net;
using System.Text;

namespace Smartrac.SmartCosmos.Flows.AccountManagement
{
    /// <summary>
    /// Client for AccountManagement Endpoints
    /// </summary>
    internal class FlowsAccountManagementEndpoint : BaseFlowsEndpoint, IFlowsAccountManagementEndpoint
    {
        /// <summary>
        /// Lookup my account details
        /// </summary>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        public AccountActionResult GetAccountDetails(out AccountDetailsResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/account", WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<AccountDetailsResponse>(request, out responseData);

                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return AccountActionResult.Successful;
                /*if (responseData == null || responseData.HTTPStatusCode == HttpStatusCode.OK)
                    return AccountActionResult.Successful;*/

                return AccountActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return AccountActionResult.Failed;
            }
        }

        /// <summary>
        /// Trigger a password reset workflow via email for the specified Account associated with the indicated email address.
        /// </summary>
        /// <param name="requestData">contains your emailAddress</param>
        /// <param name="responseData">out: Account details</param>
        /// <returns>AccountManagementActionResult</returns>
        public AccountActionResult ResetLostPassword(ResetLostPasswordRequest requestData, out ResetLostPasswordResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Reset Lost Password: required data is missing!", LogType.Error);
                    return AccountActionResult.Failed;
                }

                var request = CreateWebRequest("/account/password/reset", WebRequestOption.Authorization);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "text/plain";
                request.ContentLength = requestData.emailAddress.Length;
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(Encoding.UTF8.GetBytes(requestData.emailAddress), 0, requestData.emailAddress.Length);
                }
                ExecuteWebRequestJSON<ResetLostPasswordResponse>(request, out responseData, WebRequestMethods.Http.Post);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return AccountActionResult.Successful;

                return AccountActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return AccountActionResult.Failed;
            }
        }
    }
}