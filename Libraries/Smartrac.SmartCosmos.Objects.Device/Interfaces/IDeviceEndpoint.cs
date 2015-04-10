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

namespace Smartrac.SmartCosmos.Objects.Device
{
    public enum DeviceActionResult
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

    public interface IDeviceEndpoint : IBaseEndpoint
    {
        /// <summary>
        /// Create a new device associated with an arbitrary identification value
        /// </summary>
        /// <param name="requestData">Identification, name,  type</param>
        /// <param name="responseData">Code, urn</param>
        /// <returns>DeviceActionResult</returns>
        DeviceActionResult Create(DeviceDefinitionRequest requestData, out DeviceDefinitionResponse responseData);

        /// <summary>
        /// Update an existing device
        /// </summary>
        /// <param name="requestData">Identification or urn must be used to identify of element</param>
        /// <param name="responseData">Code, message</param>
        /// <returns>DeviceActionResult</returns>
        DeviceActionResult Update(DeviceUpdateRequest requestData, out DeviceUpdateResponse responseData);

        /// <summary>
        /// Lookup a specific device by its system-assigned URN key
        /// </summary>
        /// <param name="deviceUrn">Device urn</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>DeviceActionResult</returns>
        DeviceActionResult Lookup(Urn deviceUrn,
                                    out DeviceLookupResponse responseData,
                                    ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup a specific device by its system-assigned URN key
        /// </summary>
        /// <param name="identificationElement">Identification</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>DeviceActionResult</returns>
        DeviceActionResult Lookup(Identification identificationElement,
                                                     out DeviceLookupResponse responseData,
                                                     ViewType viewType = ViewType.Standard);

        /// <summary>
        /// Lookup an array of matching devices
        /// </summary>
        /// <param name="identificationElement">Name or part of it</param>
        /// <param name="responseData">Array of elements with all available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>DeviceActionResult</returns>
        DeviceActionResult Lookup(Name identificationElement,
                                                     out DeviceLookupsResponse responseData,
                                                     ViewType viewType = ViewType.Standard);
    }
}