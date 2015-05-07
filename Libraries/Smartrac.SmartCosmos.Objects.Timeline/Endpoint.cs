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
using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.Timeline
{
    /// <summary>
    /// Timeline Endpoints
    /// SMART COSMOS may be used to capture metadata about Timelines in order to make the association
    /// that this user used this Timeline. The identification field is the most important field in this data type,
    /// as it represents the unique identifier of the Timeline. On a laptop, this could be the computer's network
    /// MAC address, or serial number printed on an asset tag affixed to the computer, or on a cell phone it
    /// could be the manufacturer's IMEI string or perhaps the phone number.
    /// </summary>
    internal class TimelineEndpoint : BaseObjectsEndpoint, ITimelineEndpoint
    {
        /// <summary>
        /// Create a new Timeline associated with an arbitrary identification value
        /// </summary>
        /// <param name="requestData">Identification, name,  type</param>
        /// <param name="responseData">Code, urn</param>
        /// <returns>TimelineActionResult</returns>
        public TimelineActionResult Create(TimelineDefinitionRequest requestData, out TimelineDefinitionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return TimelineActionResult.Failed;
                }

                var request = CreateWebRequest("/timelines", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<TimelineDefinitionRequest, TimelineDefinitionResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            return TimelineActionResult.Successful;

                        default: return TimelineActionResult.Failed;
                    }
                }

                return TimelineActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TimelineActionResult.Failed;
            }
            //return TimelineActionResult.Successful;
        }

        /// <summary>
        /// Update an existing Timeline
        /// </summary>
        /// <param name="requestData">Identification or urn must be used to identify of element</param>
        /// <param name="responseData">Code, message</param>
        /// <returns>TimelineActionResult</returns>
        public TimelineActionResult Update(TimelineUpdateRequest requestData, out TimelineUpdateResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return TimelineActionResult.Failed;
                }

                var request = CreateWebRequest("/timelines", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<TimelineUpdateRequest, TimelineUpdateResponse>(request, requestData, out responseData);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent:
                            return TimelineActionResult.Successful;

                        default: return TimelineActionResult.Failed;
                    }
                }

                return TimelineActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TimelineActionResult.Failed;
            }
            //return TimelineActionResult.Successful;
        }

        /// <summary>
        /// Lookup a specific Timeline by its system-assigned URN key
        /// </summary>
        /// <param name="timelineUrn">Timeline urn</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>TimelineActionResult</returns>
        public TimelineActionResult Lookup(Urn timelineUrn,
                                            out TimelineLookupResponse responseData,
                                            ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == timelineUrn) || (!timelineUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return TimelineActionResult.Failed;
                }

                Uri url = new Uri("/timelines", UriKind.Relative).
                    AddSubfolder(timelineUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<TimelineLookupResponse>(request, out responseData, WebRequestMethods.Http.Get);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return TimelineActionResult.Successful;
                        default: return TimelineActionResult.Failed;
                    }
                }

                return TimelineActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TimelineActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific Timeline by its system-assigned URN key
        /// </summary>
        /// <param name="identificationElement">Identification</param>
        /// <param name="responseData">All element available data</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <returns>TimelineActionResult</returns>
        public TimelineActionResult Lookup(string nameLike,
                                           out TimelineLookupsResponse responseData,
                                           ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if (! String.IsNullOrEmpty(nameLike))
                {
                    if (null != Logger)
                        Logger.AddLog("nameLike is empty", LogType.Error);
                    return TimelineActionResult.Failed;
                }

                Uri url = new Uri("/timelines", UriKind.Relative).
                    AddQuery("nameLike", nameLike).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<TimelineLookupsResponse>(request, out responseData, WebRequestMethods.Http.Get);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return TimelineActionResult.Successful;
                        default: return TimelineActionResult.Failed;
                    }
                }

                return TimelineActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TimelineActionResult.Failed;
            }
        }
    }
}