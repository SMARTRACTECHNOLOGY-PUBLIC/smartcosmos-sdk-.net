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

using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.Device
{
    /// <summary>
    /// Device Endpoints
    /// SMART COSMOS may be used to capture metadata about devices in order to make the association 
    /// that this user used this device. The identification field is the most important field in this data type, 
    /// as it represents the unique identifier of the device. On a laptop, this could be the computer's network 
    /// MAC address, or serial number printed on an asset tag affixed to the computer, or on a cell phone it 
    /// could be the manufacturer's IMEI string or perhaps the phone number.
    /// </summary>
    internal class DeviceEndpoint : BaseObjectsEndpoint, IDeviceEndpoint
    {
        /// <summary>
        /// Create a new device associated with an arbitrary identification value
        /// </summary>
        /// <param name="requestData">Identification, name,  type</param>
        /// <param name="responseData">Code, urn</param>
        /// <returns>DeviceActionResult</returns>
        public DeviceActionResult Create(DeviceDefinitionRequest requestData, out DeviceDefinitionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return DeviceActionResult.Failed;
                }
                
                var request = CreateWebRequest("/devices", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<DeviceDefinitionRequest, DeviceDefinitionResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            return DeviceActionResult.Successful;

                        default: return DeviceActionResult.Failed;
                    }
                }

                return DeviceActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DeviceActionResult.Failed;
            }
            //return DeviceActionResult.Successful;
        }


        /// <summary>
        /// Update an existing device
        /// </summary>
        /// <param name="requestData">Identification or urn must be used to identify of element</param>
        /// <param name="responseData">Code, message</param>
        /// <returns>DeviceActionResult</returns>
        public DeviceActionResult Update(DeviceUpdateRequest requestData, out DeviceUpdateResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return DeviceActionResult.Failed;
                }

                var request = CreateWebRequest("/devices", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<DeviceUpdateRequest, DeviceUpdateResponse>(request, requestData, out responseData, WebRequestMethods.Http.Post);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent:
                            return DeviceActionResult.Successful;

                        default: return DeviceActionResult.Failed;
                    }
                }

                return DeviceActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DeviceActionResult.Failed;
            }
            //return DeviceActionResult.Successful;
        }


        /// <summary>
        /// Lookup a specific device by its system-assigned URN key
        /// </summary>
        /// <param name="deviceUrn">Device urn</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>DeviceActionResult</returns>
        public DeviceActionResult Lookup(Urn deviceUrn,
                                                     out DeviceLookupResponse responseData,
                                                     ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == deviceUrn) || (!deviceUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return DeviceActionResult.Failed;
                }

                Uri url = new Uri("/devices", UriKind.Relative).
                    AddSubfolder(deviceUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<DeviceLookupResponse>(request, out responseData, WebRequestMethods.Http.Get);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return DeviceActionResult.Successful;
                        default: return DeviceActionResult.Failed;
                    }
                }
               
                return DeviceActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DeviceActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific device by its system-assigned URN key
        /// </summary>
        /// <param name="identificationElement">Identification</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>DeviceActionResult</returns>
        public DeviceActionResult Lookup(Identification identificationElement,
                                                     out DeviceLookupResponse responseData,
                                                     ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == identificationElement) || !identificationElement.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("identification is not valid", LogType.Error);
                    return DeviceActionResult.Failed;
                }

                Uri url = new Uri("/devices/device", UriKind.Relative).
                    AddSubfolder(identificationElement.Value).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<DeviceLookupResponse>(request, out responseData, WebRequestMethods.Http.Get);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return DeviceActionResult.Successful;
                        default: return DeviceActionResult.Failed;
                    }
                }

                return DeviceActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DeviceActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of matching devices
        /// </summary>
        /// <param name="identificationElement">Name or part of it</param>
        /// <param name="responseData">Array of elements with all available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>DeviceActionResult</returns>
        public DeviceActionResult Lookup(Name nameLike,
                                                     out DeviceLookupsResponse responseData,
                                                     ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == nameLike) || !nameLike.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("name is not valid", LogType.Error);
                    return DeviceActionResult.Failed;
                }

                Uri url = new Uri("/devices", UriKind.Relative).
                    AddQuery("nameLike", nameLike.NameLike).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<DeviceLookupsResponse>(request, out responseData, WebRequestMethods.Http.Get);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return DeviceActionResult.Successful;
                        default: return DeviceActionResult.Failed;
                    }
                }

                return DeviceActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return DeviceActionResult.Failed;
            }
        }
    }
}
