using Smartrac.SmartCosmos.ClientEndpoint.Base;
using System;


namespace Smartrac.SmartCosmos.Objects.Device
{
    public class DeviceEndpointBuilder : BaseEndpointBuilder<IDeviceEndpoint, DeviceEndpointBuilder>
    {
        public DeviceEndpointBuilder() :
            base(new DeviceEndpoint())
        {

        }
    }
}
