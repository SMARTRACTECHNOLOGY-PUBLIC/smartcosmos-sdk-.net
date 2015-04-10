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

namespace Smartrac.SmartCosmos.Objects.ObjectManagement
{
    /// <summary>
    /// Client for ObjectManagement Endpoints
    /// </summary>
    internal class ObjectManagementEndpoint : BaseObjectsEndpoint, IObjectManagementEndpoint
    {
        /// <summary>
        /// Create a new Object associated with the specified email address
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult Create(ObjectManagementNewRequest requestData, out ObjectManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                var request = CreateWebRequest("/objects", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<ObjectManagementRequest, ObjectManagementResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            responseData.objectUrn = new Urn(responseData.message);
                            return ObjectActionResult.Successful;

                        default: return ObjectActionResult.Failed;
                    }
                }

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Update an existing Object
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result, empty if successful</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult Update(ObjectManagementRequest requestData, out ObjectManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                var request = CreateWebRequest("/objects", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<ObjectManagementRequest, ObjectManagementResponse>(request, requestData, out responseData, WebRequestMethods.Http.Post);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.NoContent: return ObjectActionResult.Successful;
                        case HttpStatusCode.BadRequest: return ObjectActionResult.Failed;
                        default: return ObjectActionResult.Failed;
                    }
                }

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific object by its system-assigned URN key
        /// </summary>
        /// <param name="urn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult Lookup(Urn urn, out ObjectDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == urn) || (!urn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                Uri url = new Uri("/objects", UriKind.Relative).
                    AddSubfolder(urn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<ObjectDataResponse>(request, out responseData);
                if (((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK)) || responseData.HTTPStatusCode == HttpStatusCode.NoContent)
                    return ObjectActionResult.Successful;
                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific object by their arbitrary developer assigned object URN
        /// </summary>
        /// <param name="objectUrn">Case-sensitive object URN to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="exact">Defaults to true; when false, a starts-with search is performed</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult LookupByObjectUrn(Urn objectUrn, out ObjectDataResponse responseData, ViewType viewType = ViewType.Standard, bool exact = true)
        {
            responseData = null;
            try
            {
                if ((null == objectUrn) || (!objectUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                Uri url = new Uri("/objects/object/", UriKind.Relative).
                    AddSubfolder(objectUrn.UUID).
                    AddQuery("view", viewType.GetDescription()).
                    AddQuery("exact", exact.ToString());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<ObjectDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK) || responseData.HTTPStatusCode == HttpStatusCode.NoContent)
                    return ObjectActionResult.Successful;

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of matching objects
        /// </summary>
        /// <param name="requestData">Object query request (e.g. filters like name, objectURN, ...)</param>
        /// <param name="responseData">List of objects</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectActionResult QueryObjects(QueryObjectsRequest requestData, out QueryObjectsResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectActionResult.Failed;
                }

                Uri url = new Uri("/objects", UriKind.Relative).
                    AddQuery("objectUrnLike", requestData.objectUrnLike).
                    AddQuery("type", requestData.type).
                    AddQuery("nameLike", requestData.nameLike).
                    AddQuery("monikerLike", requestData.monikerLike).
                    AddQuery("modifiedAfter", requestData.modifiedAfter).
                    AddQuery("view", requestData.viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var HTTPStatusCode = ExecuteWebRequestJSON<QueryObjectsResponse>(request, out responseData);

                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.NoContent:
                            return ObjectActionResult.Successful;

                        case HttpStatusCode.BadRequest:
                            return ObjectActionResult.Failed;
                    }
                }

                return ObjectActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectActionResult.Failed;
            }
        }
    }
}