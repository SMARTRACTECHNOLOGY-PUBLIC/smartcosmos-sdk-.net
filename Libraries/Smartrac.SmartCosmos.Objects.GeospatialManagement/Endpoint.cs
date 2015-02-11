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

using Newtonsoft.Json;
using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    /// <summary>
    /// Client for GeospatialManagement Endpoints
    /// </summary>
    internal class GeospatialManagementEndpoint : BaseObjectsEndpoint, IGeospatialManagementEndpoint
    {
        protected override JsonSerializerSettings GetJsonSerializerSettings()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
            };

            settings.Converters.Add(new GeoJSONCreationConverter());
            return settings;
        }

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
                ExecuteWebRequestJSON<GeospatialManagementNewRequest, GeospatialManagementNewResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
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
                HttpWebResponse webResponse = null;
                ExecuteWebRequestJSON<GeospatialManagementUpdateRequest, GeospatialManagementUpdateResponse>(request, requestData, out responseData, out webResponse, WebRequestMethods.Http.Post);
                if (responseData != null)
                {
                    if ((responseData.HTTPStatusCode == HttpStatusCode.NoContent)) // &&
                    //(webResponse.Headers.Get("SmartCosmos-Event") == "GeospatialEntryUpdated"))
                    {
                        return GeoActionResult.Successful;
                    }
                    else
                    {
                        return GeoActionResult.Failed;
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

                Uri url = new Uri("/geospatial", UriKind.Relative).
                    AddQuery("nameLike", requestData.nameLike).
                    AddQuery("view", requestData.viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var HTTPStatusCodeResult = ExecuteWebRequestJSON<QueryGeospatialEntriesResponse>(request, out responseData);
                if (responseData != null)
                {
                    responseData.HTTPStatusCode = HTTPStatusCodeResult;
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return GeoActionResult.Successful;
                        default: return GeoActionResult.Failed;
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

                Uri url = new Uri("/geospatial", UriKind.Relative).
                    AddSubfolder(geospatialUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                ExecuteWebRequestJSON<GeospatialEntryDataResponse>(request, out responseData);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return GeoActionResult.Successful;
                        default: return GeoActionResult.Failed;
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