﻿#region License

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

namespace Smartrac.SmartCosmos.Objects.RelationshipManagement
{
    /// <summary>
    /// Client for RelationshipManagement Endpoints
    /// </summary>
    internal class RelationshipManagementEndpoint : BaseObjectsEndpoint, IRelationshipManagementEndpoint
    {
        /// <summary>
        /// Create a new relationship
        /// </summary>
        /// <param name="requestData">Relationship data</param>
        /// <param name="responseData">result</param>
        /// <returns>RelationshipManagementActionResult</returns>
        public RelationshipActionResult Create(RelationshipManagementRequest requestData, out RelationshipManagementResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return RelationshipActionResult.Failed;
                }

                var request = CreateWebRequest("/relationships/" + requestData.entityReferenceType + "/" + requestData.referenceUrn, WebRequestOption.Authorization);
                ExecuteWebRequestJSON<RelationshipManagementRequest, RelationshipManagementResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.Created:
                        case HttpStatusCode.OK:
                            responseData.relationshipUrn = new Urn(responseData.message);
                            return RelationshipActionResult.Successful;

                        default: return RelationshipActionResult.Failed;
                    }
                }

                return RelationshipActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RelationshipActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific relationship by its system-assigned URN key
        /// </summary>
        /// <param name="RelationshipUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Relationship data</param>
        /// <returns>RelationshipManagementActionResult</returns>
        public RelationshipActionResult Lookup(Urn relationshipUrn, out RelationshipDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == relationshipUrn) || (!relationshipUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return RelationshipActionResult.Failed;
                }

                var request = CreateWebRequest("/relationships/" + relationshipUrn.UUID + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                ExecuteWebRequestJSON<RelationshipDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return RelationshipActionResult.Successful;

                return RelationshipActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RelationshipActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of all defined relationships between any two entities
        /// </summary>
        /// <param name="requestData">filter data</param>
        /// <param name="responseData">list of relationships</param>
        /// <returns>RelationshipManagementActionResult</returns>
        public RelationshipActionResult Lookup(QueryQueryRelationshipsRequest requestData, out QueryObjectRelationshipsResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == requestData) || (!requestData.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("Request is not valid", LogType.Error);
                    return RelationshipActionResult.Failed;
                }

                var request = CreateWebRequest("/relationships/" + requestData.entityReferenceType + "/" +
                                                                   requestData.referenceUrn + "/" +
                                                                   requestData.relatedEntityReferenceType + "/" +
                                                                   requestData.relatedReferenceUrn +
                                                                   "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                var HTTPStatusCodeResult = ExecuteWebRequestJSON<QueryObjectRelationshipsResponse>(request, out responseData);
                if (responseData != null)
                {
                    responseData.HTTPStatusCode = HTTPStatusCodeResult;
                    if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                        return RelationshipActionResult.Successful;
                }

                return RelationshipActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RelationshipActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific relationships between any two entities
        /// </summary>
        /// <param name="RelationshipUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Relationship data</param>
        /// <returns>RelationshipManagementActionResult</returns>
        public RelationshipActionResult Lookup(QueryQueryRelationshipByTypeRequest requestData, out RelationshipDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == requestData) || (!requestData.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("Request is not valid", LogType.Error);
                    return RelationshipActionResult.Failed;
                }

                var request = CreateWebRequest("/relationships/" + requestData.entityReferenceType + "/" +
                                                                   requestData.referenceUrn + "/" +
                                                                   requestData.relatedEntityReferenceType + "/" +
                                                                   requestData.relatedReferenceUrn + "/" +
                                                                   requestData.type + "/" +
                                                                   "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                ExecuteWebRequestJSON<RelationshipDataResponse>(request, out responseData);
                if ((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK))
                    return RelationshipActionResult.Successful;

                return RelationshipActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RelationshipActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of all defined relationships of the specific type
        /// </summary>
        /// <param name="requestData">filter data</param>
        /// <param name="responseData">list of relationships</param>
        /// <returns>RelationshipManagementActionResult</returns>
        public RelationshipActionResult Lookup(QueryQueryRelationshipsByTypeRequest requestData, out QueryObjectRelationshipsResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == requestData) || (!requestData.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("Request is not valid", LogType.Error);
                    return RelationshipActionResult.Failed;
                }

                var request = CreateWebRequest("/relationships/" + requestData.entityReferenceType + "/" +
                                                                   requestData.referenceUrn + "/" +
                                                                   requestData.type +
                                                                   "?view=" + viewType.GetDescription() +
                                                                   "&reverse=" + requestData.reverse
                                                                   , WebRequestOption.Authorization);
                var HTTPStatusCodeResult = ExecuteWebRequestJSON<QueryObjectRelationshipsResponse>(request, out responseData);
                if (responseData != null)
                {
                    responseData.HTTPStatusCode = HTTPStatusCodeResult;
                    if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                        return RelationshipActionResult.Successful;
                }

                return RelationshipActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RelationshipActionResult.Failed;
            }
        }

        /// <summary>
        /// Deletes an existing relationship by its system-assigned URN key
        /// </summary>
        /// <param name="relationshipUrn">relationshipUrn of the relationship record</param>
        /// <returns>RelationshipManagementActionResult</returns>
        public RelationshipActionResult Delete(Urn relationshipUrn)
        {
            try
            {
                if ((null == relationshipUrn) || (!relationshipUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return RelationshipActionResult.Failed;
                }

                var request = CreateWebRequest("/relationships/" + relationshipUrn.UUID, WebRequestOption.Authorization);
                request.Method = "DELETE";

                using (var response = request.GetResponseSafe() as System.Net.HttpWebResponse)
                {
                    if (response != null)
                    {
                        try
                        {
                            if ((response.StatusCode == HttpStatusCode.NoContent) &&
                                (response.Headers.Get("SmartCosmos-Event") == "RelationshipDeleted"))
                            {
                                return RelationshipActionResult.Successful;
                            }
                        }
                        finally
                        {
                            response.Close();
                        }
                    }
                    return RelationshipActionResult.Failed;
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return RelationshipActionResult.Failed;
            }
        }
    }
}