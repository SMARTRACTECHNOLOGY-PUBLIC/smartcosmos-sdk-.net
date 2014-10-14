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

using System;
using System.Net;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    /// <summary>
    /// Client for GeospatialManagement Endpoints
    /// </summary>
    internal class GeospatialManagementEndpoint : BaseObjectsEndpoint, IGeospatialManagementEndpoint
    {
        /// <summary>
        /// Create a new geospatial entry
        /// </summary>
        /// <param name="requestData">Geospatial data</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        public GeoActionResult Create(GeospatialManagementNewRequest requestData, out GeospatialManagementNewResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return GeoActionResult.Failed;
                }

                var request = CreateWebRequest("/geospatial", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(GeospatialManagementNewRequest), requestData, typeof(GeospatialManagementNewResponse), out responseDataObj, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as GeospatialManagementNewResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.Created:
                            case HttpStatusCode.OK:
                                responseData.geospatialUrn = new Urn(responseData.message);
                                return GeoActionResult.Successful;

                            default: return GeoActionResult.Failed;
                        }
                    }
                }

                return GeoActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return GeoActionResult.Failed;
            }
        }

        /// <summary>
        /// Update an existing geospatial entry
        /// </summary>
        /// <param name="requestData">Geospatial data</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        public GeoActionResult Update(GeospatialManagementUpdateRequest requestData, out GeospatialManagementUpdateResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return GeoActionResult.Failed;
                }

                var request = CreateWebRequest("/geospatial", WebRequestOption.Authorization);
                object responseDataObj = null;
                HttpWebResponse webResponse = null;
                ExecuteWebRequestJSON(request, typeof(GeospatialManagementNewRequest), requestData, typeof(GeospatialManagementUpdateResponse), out responseDataObj, out webResponse, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as GeospatialManagementUpdateResponse;
                    if (responseData != null)
                    {
                        if ((responseData.HTTPStatusCode == HttpStatusCode.NoContent) &&
                           (webResponse.Headers.Get("SmartCosmos-Event") == "GeospatialEntryUpdated"))
                        {
                            return GeoActionResult.Successful;
                        }
                        else
                        {
                            return GeoActionResult.Failed;
                        }
                    }
                }

                return GeoActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return GeoActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of matching geospatial entries. If no nameLike query parameter is included, then all interactions are returned.
        /// </summary>
        /// <param name="requestData">Geospatial data</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        public GeoActionResult Lookup(QueryGeospatialEntriesRequest requestData, out QueryGeospatialEntriesResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return GeoActionResult.Failed;
                }

                string nameLikeParam = (requestData.nameLike != null) ? "&nameLike=" + requestData.nameLike : "";

                var request = CreateWebRequest("/geospatial?view=" + requestData.viewType.GetDescription() + nameLikeParam, WebRequestOption.Authorization);
                object responseDataObj = null;
                var HTTPStatusCodeResult = ExecuteWebRequestJSON(request, typeof(QueryGeospatialEntriesResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as QueryGeospatialEntriesResponse;
                    if (responseData != null)
                    {
                        responseData.HTTPStatusCode = HTTPStatusCodeResult;
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.OK: return GeoActionResult.Successful;
                            default: return GeoActionResult.Failed;
                        }
                    }
                }

                return GeoActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return GeoActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific geospatial entity by its system-assigned URN key
        /// </summary>
        /// <param name="geospatialUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">result</param>
        /// <returns>GeospatialManagementActionResult</returns>
        public GeoActionResult Lookup(Urn geospatialUrn, out GeospatialEntryDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((geospatialUrn == null) || !geospatialUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return GeoActionResult.Failed;
                }

                var request = CreateWebRequest("/geospatial/" + geospatialUrn.UUID + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(GeospatialEntryDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as GeospatialEntryDataResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.OK: return GeoActionResult.Successful;
                            default: return GeoActionResult.Failed;
                        }
                    }
                }

                return GeoActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return GeoActionResult.Failed;
            }
        }
    }
}