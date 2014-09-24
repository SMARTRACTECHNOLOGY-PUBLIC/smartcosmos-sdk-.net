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
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.TagManagement
{
    /// <summary>
    /// Client for TagManagement Endpoints
    /// </summary>
    class TagManagementEndpoint : BaseObjectsEndpoint, ITagManagementEndpoint
    {
        /// <summary>
        /// Create a new Tag associated with the specified name
        /// </summary>
        /// <param name="requestData">Tag data</param>
        /// <param name="responseData">result</param>
        /// <returns>TagManagementActionResult</returns>
        public TagActionResult Create(TagManagementRequest requestData, out TagManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return TagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(TagManagementRequest), requestData, typeof(TagManagementResponse), out responseDataObj, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as TagManagementResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.Created:
                            case HttpStatusCode.OK:
                                responseData.tagUrn = new Urn(responseData.message);
                                return TagActionResult.Successful;
                            default: return TagActionResult.Failed;
                        }
                    }
                }

                return TagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific Tag by its system-assigned URN key
        /// </summary>
        /// <param name="tagUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>TagManagementActionResult</returns>
        public TagActionResult Lookup(Urn tagUrn, out TagDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == tagUrn) || (!tagUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return TagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags/" + tagUrn.UUID + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(TagDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as TagDataResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return TagActionResult.Successful;
                }
                return TagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific Tag by their name
        /// </summary>
        /// <param name="tagName">Exact case-sensitive name to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>TagManagementActionResult</returns>
        public TagActionResult Lookup(string tagName, out TagDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if (String.IsNullOrEmpty(tagName))
                {
                    if (null != Logger)
                        Logger.AddLog("eMail address is not valid", LogType.Error);
                    return TagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags/tag/" + tagName + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(TagDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as TagDataResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return TagActionResult.Successful;
                }
                return TagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagActionResult.Failed;
            }
        }

        /// <summary>
        /// Query all tags from a specific object
        /// </summary>
        /// <param name="tagName">Exact case-sensitive name to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>TagManagementActionResult</returns>
        public TagActionResult Lookup(EntityReferenceType entityReferenceType, Urn referenceUrn, out TagDataListResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == referenceUrn) || (!referenceUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("referenceUrn is not valid", LogType.Error);
                    return TagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags?view=" + viewType.GetDescription() +
                    "&entityReferenceType=" + entityReferenceType.GetDescription() +
                    "&referenceUrn=" + referenceUrn.UUID
                    , WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(TagDataListResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as TagDataListResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return TagActionResult.Successful;
                }
                return TagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagActionResult.Failed;
            }
        }


        /// <summary>
        /// Assign tags to objects
        /// </summary>
        /// <param name="tagName">Exact case-sensitive email address to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>TagManagementActionResult</returns>
        public TagActionResult Assign(EntityReferenceType entityReferenceType, Urn referenceUrn, out DefaultResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == referenceUrn) || (!referenceUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("referenceUrn is not valid", LogType.Error);
                    return TagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags/" + entityReferenceType.GetDescription() + 
                                               "/" + referenceUrn.UUID, WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(DefaultResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as DefaultResponse;
                    if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                        return TagActionResult.Successful;
                }
                return TagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagActionResult.Failed;
            }
        }


        /// <summary>
        /// Deletes an existing tag by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        public TagActionResult Delete(Urn tagUrn)
        {
            try
            {
                if (null == tagUrn)
                {
                    if (null != Logger)
                        Logger.AddLog("tagUrn is null", LogType.Error);
                    return TagActionResult.Failed;
                }

                if (!tagUrn.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid fileUrn: " + tagUrn.UUID, LogType.Error);
                    return TagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags/tag/" + tagUrn.UUID);
                request.Method = "DELETE";

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    if ((response.StatusCode == HttpStatusCode.NoContent) &&
                       (response.Headers.Get("SmartCosmos-Event") == "FileDeleted"))
                    {
                        return TagActionResult.Successful;
                    }
                    else
                    {
                        return TagActionResult.Failed;
                    }
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagActionResult.Failed;
            }
        }

    }
}
