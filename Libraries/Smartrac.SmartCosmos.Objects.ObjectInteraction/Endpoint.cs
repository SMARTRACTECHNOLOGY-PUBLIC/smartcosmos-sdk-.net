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

namespace Smartrac.SmartCosmos.Objects.ObjectInteraction
{
    /// <summaryObjectInteraction
    /// Client for ObjectInteraction Endpoints
    /// </summaryObjectInteraction
    class ObjectInteractionEndpoint : BaseObjectsEndpoint, IObjectInteractionEndpoint
    {
        /// <summaryObjectInteraction
        /// Capture a specific interaction
        /// </summaryObjectInteraction
        /// <param name="requestData"ObjectInteractionObject data</paramObjectInteraction
        /// <param name="responseData"ObjectInteraction result</paramObjectInteraction
        /// <returnsObjectInteractionObjectInteractionActionResult</returnsObjectInteraction
        public ObjectInteractionActionResult CaptureObjectInteraction(CaptureObjectInteractionRequest requestData, out CaptureObjectInteractionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjectInteractionActionResult.Failed;
                }

                var request = CreateWebRequest("/interactions", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(CaptureObjectInteractionRequest), requestData, typeof(CaptureObjectInteractionResponse), out responseDataObj, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as CaptureObjectInteractionResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.OK:
                                responseData.interactionUrn = new Urn(responseData.message);
                                return ObjectInteractionActionResult.Successful;
                            default: return ObjectInteractionActionResult.Failed;
                        }
                    }
                }

                return ObjectInteractionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectInteractionActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup an array of matching interactions, If no objectUrn query parameter is included, then all interactions are returned.
        /// </summary>
        /// <param name="objectUrn">Optional: Developer-assigned objectUrn to search for</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectInteractionActionResult LookupMatchingInteractions(Urn objectUrn, out QueryObjectInteractionsResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null != objectUrn) && 
                    (!objectUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjectInteractionActionResult.Failed;
                }

                var urnParam = (null != objectUrn) ? "&objectUrn=" + objectUrn.UUID : "";

                var request = CreateWebRequest("/interactions?view=" + viewType.GetDescription() + urnParam, WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(QueryObjectInteractionsResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as QueryObjectInteractionsResponse;
                    if (responseData != null)
                    {
                        responseData.HTTPStatusCode = returnHTTPCode;
                        if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                            return ObjectInteractionActionResult.Successful;
                    }
                }
                return ObjectInteractionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectInteractionActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific interaction by its system-assigned URN key
        /// </summary>
        /// <param name="interactionUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjectInteractionActionResult LookupSpecificObjectInteractionbyURN(Urn interactionUrn, out QueryObjectInteractionsResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == interactionUrn) || (!interactionUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjectInteractionActionResult.Failed;
                }

                var request = CreateWebRequest("/interactions/" + interactionUrn.UUID + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(QueryObjectInteractionsResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as QueryObjectInteractionsResponse;
                    if (responseData != null)
                    {
                        responseData.HTTPStatusCode = returnHTTPCode;
                        if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                            return ObjectInteractionActionResult.Successful;
                    }
                }
                return ObjectInteractionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjectInteractionActionResult.Failed;
            }
        }
    
    }
}
