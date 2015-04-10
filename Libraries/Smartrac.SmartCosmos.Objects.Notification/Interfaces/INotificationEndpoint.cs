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
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Notification
{
    public enum NotificationActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed

        /// <summary>
        /// item already exists
        /// </summary>
        //Conflict
    }

    public interface INotificationEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Enroll an existing endpoint running a web app ready to confirm a subscription and receive events
        /// </summary>
        /// <param name="requestData">All element available data</param>
        /// <param name="responseData">Urn or message with error description</param>
        /// <returns>NotificationActionResult</returns>
        NotificationActionResult Create(EnrollNotificationsRequest requestData, out NotificationResponse responseData);

        /// <summary>
        /// Confirms the enrollment of an endpoint running a web app that received a `topicArn`
        /// </summary>
        /// <param name="requestData">topicArn, token</param>
        /// <param name="responseData">Returns code, message only for errors</param>
        /// <returns>NotificationActionResult</returns>
        NotificationActionResult Confirm(EnrollmentConfirmationRequest requestData, out NotificationResponse responseData);

        /// <summary>
        /// Withdraw or terminate delivery of events to an existing endpoint
        /// </summary>
        /// <param name="deviceUrn">Notification urn</param>
        /// <param name="responseData">Returns code, message only for errors</param>
        /// <returns>NotificationActionResult</returns>
        NotificationActionResult Delete(Urn notificationUrn,
                                    out NotificationResponse responseData);

        /// <summary>
        /// Lookup a specific notification endpoint by its system-assigned URN key
        /// </summary>
        /// <param name="identificationElement">Notification urn</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>NotificationActionResult</returns>
        NotificationActionResult Lookup(Urn notificationUrn,
                                                     out LookupNotificationsResponse responseData,
                                                     ViewType viewType = ViewType.Standard);
    }
}