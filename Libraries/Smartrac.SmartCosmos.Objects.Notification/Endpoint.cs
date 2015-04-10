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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Net;


namespace Smartrac.SmartCosmos.Objects.Notification
{
    public class NotificationEndpoint : BaseObjectsEndpoint, INotificationEndpoint
    {
        /// <summary>
        /// Enroll an existing endpoint running a web app ready to confirm a subscription and receive events
        /// </summary>
        /// <param name="requestData">All element available data</param>
        /// <param name="responseData">Urn or message with error description</param>
        /// <returns>NotificationActionResult</returns>
        public NotificationActionResult Create(EnrollNotificationsRequest requestData, out NotificationResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return NotificationActionResult.Failed;
                }

                var request = CreateWebRequest("/notifications/enroll", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<EnrollNotificationsRequest, NotificationResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            return NotificationActionResult.Successful;

                        default: return NotificationActionResult.Failed;
                    }
                }

                return NotificationActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return NotificationActionResult.Failed;
            }
        }

        /// <summary>
        /// Confirms the enrollment of an endpoint running a web app that received a `topicArn`
        /// </summary>
        /// <param name="requestData">topicArn, token</param>
        /// <param name="responseData">Returns code, message only for errors</param>
        /// <returns>NotificationActionResult</returns>
        public NotificationActionResult Confirm(EnrollmentConfirmationRequest requestData, out NotificationResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return NotificationActionResult.Failed;
                }

                Uri url = new Uri("/notifications/confirm/", UriKind.Relative).
                    AddSubfolder(requestData.topicArn);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                ExecuteWebRequestJSON<EnrollmentConfirmationRequest, NotificationResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent:
                            return NotificationActionResult.Successful;

                        default: return NotificationActionResult.Failed;
                    }
                }

                return NotificationActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return NotificationActionResult.Failed;
            }
        }

        /// <summary>
        /// Withdraw or terminate delivery of events to an existing endpoint
        /// </summary>
        /// <param name="deviceUrn">Notification urn</param>
        /// <param name="responseData">Returns code, message only for errors</param>
        /// <returns>NotificationActionResult</returns>
        public NotificationActionResult Delete(Urn notificationUrn,
                                    out NotificationResponse responseData)
        {
            responseData = null;
            try
            {
                if ((notificationUrn == null) || !notificationUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return NotificationActionResult.Failed;
                }

                var request = CreateWebRequest("/notifications/withdraw", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<Urn, NotificationResponse>(request, notificationUrn, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent:
                            return NotificationActionResult.Successful;

                        default: return NotificationActionResult.Failed;
                    }
                }

                return NotificationActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return NotificationActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific notification endpoint by its system-assigned URN key
        /// </summary>
        /// <param name="identificationElement">Notification urn</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>NotificationActionResult</returns>
        public NotificationActionResult Lookup(Urn notificationUrn,
                                                     out LookupNotificationsResponse responseData,
                                                     ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == notificationUrn) || !notificationUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("identification is not valid", LogType.Error);
                    return NotificationActionResult.Failed;
                }

                Uri url = new Uri("/notifications", UriKind.Relative).
                    AddSubfolder(notificationUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<LookupNotificationsResponse>(request, out responseData, WebRequestMethods.Http.Get);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.NoContent: 
                            return NotificationActionResult.Successful;
                        default: return NotificationActionResult.Failed;
                    }
                }

                return NotificationActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return NotificationActionResult.Failed;
            }
        }
    }
}
