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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.ObjectInteraction
{
    /// <summary>
    /// Client for ObjectInteractionItem Endpoints
    /// </summary>
    internal class ObjectInteractionEndpoint : BaseObjectsEndpoint, IObjectInteractionEndpoint
    {
        /// <summary>
        /// Capture a specific interaction
        /// </summary>
        /// <param name="requestData"> data</param>
        /// <param name="responseData"> result</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjInteractActionResult Create(CaptureObjectInteractionRequest requestData, out CaptureObjectInteractionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjInteractActionResult.Failed;
                }

                var request = CreateWebRequest("/interactions", WebRequestOption.Authorization);
                ExecuteWebRequestJSON<CaptureObjectInteractionRequest, CaptureObjectInteractionResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK:
                        case HttpStatusCode.Created:
                            responseData.interactionUrn = new Urn(responseData.message);
                            return ObjInteractActionResult.Successful;

                        default: return ObjInteractActionResult.Failed;
                    }
                }

                return ObjInteractActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of matching interactions, If no objectUrn query parameter is included, then all interactions are returned.
        /// </summary>
        /// <param name="objectUrn">Optional: Developer-assigned objectUrn to search for</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjInteractActionResult Lookup(Urn objectUrn, out QueryObjectInteractionsResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null != objectUrn) &&
                    (!objectUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjInteractActionResult.Failed;
                }

                Uri url = new Uri("/interactions", UriKind.Relative).
                    AddQuery("view", viewType.GetDescription()).
                    AddQuery("objectUrn", objectUrn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<QueryObjectInteractionsResponse>(request, out responseData);
                if (responseData != null)
                {
                    responseData.HTTPStatusCode = returnHTTPCode;
                    if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                        return ObjInteractActionResult.Successful;
                }

                return ObjInteractActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific interaction by its system-assigned URN key
        /// </summary>
        /// <param name="urn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjInteractActionResult LookupByUrn(Urn interactionUrn, out QueryObjectInteractionResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == interactionUrn) || (!interactionUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjInteractActionResult.Failed;
                }

                Uri url = new Uri("/interactions", UriKind.Relative).
                    AddSubfolder(interactionUrn.UUID).
                    AddQuery("view", viewType.GetDescription());

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<QueryObjectInteractionResponse>(request, out responseData);
                               
                responseData.HTTPStatusCode = returnHTTPCode;
                if (responseData.HTTPStatusCode == HttpStatusCode.OK || responseData.HTTPStatusCode == HttpStatusCode.NoContent)
                    return ObjInteractActionResult.Successful;
                

                return ObjInteractActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractActionResult.Failed;
            }
        }
    }
}