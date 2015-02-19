using Smartrac.SmartCosmos.ClientEndpoint.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartrac.SmartCosmos.Flows.DeviceManagement
{

    public enum DeviceManagementActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed
    }
    
    public interface IDeviceManagementEndpoint : IBaseEndpoint
    {
        DeviceManagementActionResult Create();

        DeviceManagementActionResult Update();

        DeviceManagementActionResult Delete();
    }
}
