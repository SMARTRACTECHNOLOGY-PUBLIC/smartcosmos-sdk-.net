using Smartrac.SmartCosmos.ClientEndpoint.Base;
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

    }
}
