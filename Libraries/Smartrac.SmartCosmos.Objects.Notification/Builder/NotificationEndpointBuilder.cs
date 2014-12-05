using Smartrac.SmartCosmos.ClientEndpoint.Base;
using System;


namespace Smartrac.SmartCosmos.Objects.Notification
{
    public class NotificationEndpointBuilder : BaseEndpointBuilder<INotificationEndpoint, NotificationEndpointBuilder>
    {
        public NotificationEndpointBuilder() :
            base(new NotificationEndpoint())
        {

        }
    }
}
