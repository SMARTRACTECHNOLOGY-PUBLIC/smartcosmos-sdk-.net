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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.HashTag
{
    /// <summary>
    /// Client for HashTag Endpoints
    /// Hash Tags have grown in popularity across the Web for assigning quick search terms to everything from blog postings
    /// to Twitter posts. The platform provides a hash tag object for this purpose as well.
    /// </summary>
    internal class HashTagEndpoint : BaseObjectsEndpoint, IHashTagEndpoint
    {
        /// <summary>
        /// Create a new hash hash tag associated with the specified name
        /// </summary>
        /// <param name="requestData">Hash hash tag data</param>
        /// <param name="responseData">result</param>
        /// <returns>HashTagActionResult</returns>
        public HashTagActionResult Create(HashTagRequest requestData, out HashTagResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return HashTagActionResult.Failed;
                }

                var request = CreateWebRequest("/tags", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<HashTagRequest, HashTagResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            responseData.tagUrn = new Urn(responseData.message);
                            return HashTagActionResult.Successful;

                        default: return HashTagActionResult.Failed;
                    }
                }

                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific hash tag by its system-assigned URN key
        /// </summary>
        /// <param name="tagUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        public HashTagActionResult Lookup(Urn tagUrn, out HashTagDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == tagUrn) || (!tagUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return HashTagActionResult.Failed;
                }

                Uri url = new Uri("/tags", UriKind.Relative).
                    AddSubfolder(tagUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<HashTagDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return HashTagActionResult.Successful;
                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific hash tag by their name
        /// </summary>
        /// <param name="tagName">Exact case-sensitive name to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        public HashTagActionResult Lookup(string tagName, out HashTagDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if (String.IsNullOrEmpty(tagName))
                {
                    if (null != Logger)
                        Logger.AddLog("eMail address is not valid", LogType.Error);
                    return HashTagActionResult.Failed;
                }

                Uri url = new Uri("/tags/tag/", UriKind.Relative).
                    AddSubfolder(tagName).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<HashTagDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return HashTagActionResult.Successful;
                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }

        /// <summary>
        /// Query all tags from a specific object
        /// </summary>
        /// <param name="tagName">Exact case-sensitive name to locate</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Tag data</param>
        /// <returns>HashTagActionResult</returns>
        public HashTagActionResult Lookup(EntityReferenceType entityReferenceType, Urn referenceUrn, out HashTagListResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == referenceUrn) || (!referenceUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("referenceUrn is not valid", LogType.Error);
                    return HashTagActionResult.Failed;
                }

                Uri url = new Uri("/tags", UriKind.Relative).
                    AddQuery("view", viewType.GetDescription()).
                    AddQuery("entityReferenceType", entityReferenceType.GetDescription()).
                    AddQuery("referenceUrn", referenceUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);

                var returnHTTPCode = ExecuteWebRequestJSON<HashTagListResponse>(request, out responseData);
                if ((responseData != null) &&
                    ((responseData.HTTPStatusCode == HttpStatusCode.OK) ||
                    (responseData.HTTPStatusCode == HttpStatusCode.NoContent)
                    ))
                    return HashTagActionResult.Successful;
                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }

        /// <summary>
        ///  Assign hash tags to objects
        /// </summary>
        /// <param name="entityReferenceType">object reference type</param>
        /// <param name="referenceUrn">object reference</param>
        /// <param name="requestData">Hash tag list</param>
        /// <param name="responseData">result</param>
        /// <returns>HashTagActionResult</returns>
        public HashTagActionResult Assign(EntityReferenceType entityReferenceType, Urn referenceUrn, HashTagListRequest requestData, out HashTagListResponse responseData)
        {
            responseData = null;
            try
            {
                if ((null == referenceUrn) || (!referenceUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("referenceUrn is not valid", LogType.Error);
                    return HashTagActionResult.Failed;
                }

                Uri url = new Uri("/tags", UriKind.Relative).
                    AddSubfolder(entityReferenceType.GetDescription()).
                    AddSubfolder(referenceUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<HashTagListRequest, HashTagListResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return HashTagActionResult.Successful;
                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }

        /// <summary>
        /// Deletes an existing hash tag by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        public HashTagActionResult Delete(Urn tagUrn)
        {
            try
            {
                if ((null == tagUrn) || (!tagUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid tagUrn: " + tagUrn.UUID, LogType.Error);
                    return HashTagActionResult.Failed;
                }

                Uri url = new Uri("/tags/tag/", UriKind.Relative).
                    AddSubfolder(tagUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = "DELETE";

                using (var response = request.GetResponseSafe() as System.Net.HttpWebResponse)
                {
                    if (response != null)
                    {
                        try
                        {
                            if ((response.StatusCode == HttpStatusCode.NoContent) // &&
                                //(response.Headers.Get("SmartCosmos-Event") == "FileDeleted")
                               )
                            {
                                return HashTagActionResult.Successful;
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                }
                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }

        /// <summary>
        /// Deletes an existing hash tag by its system-assigned URN key
        /// </summary>
        /// <param name="fileUrn">fileUrn of the file record</param>
        /// <returns>FileActionResult</returns>
        public HashTagActionResult Delete(string tagName)
        {
            try
            {
                if (String.IsNullOrEmpty(tagName))
                {
                    if (null != Logger)
                        Logger.AddLog("Invalid tagName", LogType.Error);
                    return HashTagActionResult.Failed;
                }

                Uri url = new Uri("/tags/tag/", UriKind.Relative).
                    AddSubfolder(tagName);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                request.Method = "DELETE";

                using (var response = request.GetResponseSafe() as System.Net.HttpWebResponse)
                {
                    if (response != null)
                    {
                        try
                        {
                            if ((response.StatusCode == HttpStatusCode.NoContent) ||
                                 (response.StatusCode == HttpStatusCode.OK))
                            {
                                return HashTagActionResult.Successful;
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                }
                return HashTagActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HashTagActionResult.Failed;
            }
        }
    }
}