using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;

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
        NotificationActionResult Create(EnrollNotificationsRequest requestData, out NotificationBaseResponse responseData);

        /// <summary>
        /// Confirms the enrollment of an endpoint running a web app that received a `topicArn`
        /// </summary>
        /// <param name="requestData">topicArn, token</param>
        /// <param name="responseData">Returns code, message only for errors</param>
        /// <returns>NotificationActionResult</returns>
        NotificationActionResult Confirm(EnrollmentConfirmationRequest requestData, out NotificationBaseResponse responseData);

        /// <summary>
        /// Withdraw or terminate delivery of events to an existing endpoint
        /// </summary>
        /// <param name="deviceUrn">Notification urn</param>
        /// <param name="responseData">Returns code, message only for errors</param>
        /// <returns>NotificationActionResult</returns>
        NotificationActionResult Delete(Urn notificationUrn,
                                    out NotificationBaseResponse responseData);

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
