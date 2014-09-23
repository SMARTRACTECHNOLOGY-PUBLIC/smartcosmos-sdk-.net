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
using Smartrac.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectInteractionSession
{
    /// <summary>
    /// Client for ObjectInteractionSession Endpoints
    /// </summary>
    class ObjectInteractionSessionEndpoint : BaseObjectsEndpoint, IObjectInteractionSessionEndpoint
    {
        /// <summary>
        /// Start a new object interaction session
        /// </summary>
        /// <param name="requestData">Object data</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectInteractionSessionActionResult</returns>
        public ObjInteractSessionActionResult Start(StartObjectInteractionSessionRequest requestData, 
                                                          out StartObjectInteractionSessionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjInteractSessionActionResult.Failed;
                }

                var request = CreateWebRequest("/sessions", WebRequestOption.Authorization);
                object responseDataObj = null;
                ExecuteWebRequestJSON(request, typeof(StartObjectInteractionSessionRequest), requestData, typeof(StartObjectInteractionSessionResponse), out responseDataObj, WebRequestMethods.Http.Put);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as StartObjectInteractionSessionResponse;
                    if (responseData != null)
                    {
                        switch (responseData.HTTPStatusCode)
                        {
                            case HttpStatusCode.OK:
                                responseData.sessionUrn = new Urn(responseData.message);
                                return ObjInteractSessionActionResult.Successful;
                            default: return ObjInteractSessionActionResult.Failed;
                        }
                    }
                }

                return ObjInteractSessionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractSessionActionResult.Failed;
            }
        }
        /// <summary>
        /// Stop an existing object interaction session
        /// </summary>
        /// <param name="urn">Object interaction session urn</param>
        /// <param name="responseData">result</param>
        /// <returns>ObjectInteractionSessionActionResult</returns>
        public ObjInteractSessionActionResult Stop(StopObjectInteractionSessionRequest requestData,
                                                         out StopObjectInteractionSessionResponse responseData)
        {
            responseData = null;
            try
            {
                if ((requestData == null) || !requestData.IsValid())
                {
                    if (null != Logger)
                        Logger.AddLog("Request data is invalid!", LogType.Error);
                    return ObjInteractSessionActionResult.Failed;
                }

                var request = CreateWebRequest("/sessions", WebRequestOption.Authorization);
                object responseDataObj = null;
                HttpWebResponse webResponse;
                HttpStatusCode HttpCode = ExecuteWebRequestJSON(request, 
                                               typeof(StopObjectInteractionSessionRequest), requestData,
                                               typeof(StopObjectInteractionSessionResponse), out responseDataObj,
                                               out webResponse);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as StopObjectInteractionSessionResponse;
                    if ((responseData != null) &&
                        (webResponse != null) &&
                       (responseData.HTTPStatusCode == HttpStatusCode.NoContent) &&
                       (webResponse.Headers.Get("SmartCosmos-Event")  == "InteractionSessionStop"))
                    {
                        responseData.startTime = Rfc3339DateTime.Parse(webResponse.Headers.Get("SmartCosmos-Session-Start"));
                        return ObjInteractSessionActionResult.Successful;
                    }
                }

                return ObjInteractSessionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractSessionActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific session by its system-assigned URN key
        /// </summary>
        /// <param name="sessionUrn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjInteractSessionActionResult Lookup(Urn sessionUrn,
                                                           out ObjectInteractionSessionDataResponse responseData,
                                                           ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == sessionUrn) || (!sessionUrn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjInteractSessionActionResult.Failed;
                }

                var request = CreateWebRequest("/sessions/" + sessionUrn.UUID + "?view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(ObjectInteractionSessionDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ObjectInteractionSessionDataResponse;
                    if (responseData != null)
                    {
                        responseData.HTTPStatusCode = returnHTTPCode;
                        if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                            return ObjInteractSessionActionResult.Successful;
                    }
                }
                return ObjInteractSessionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractSessionActionResult.Failed;
            }
        }

        /// <summary>
        /// Lookup a specific session by its system-assigned URN key
        /// </summary>
        /// <param name="interactionUrn">A case-sensitive starts with string pattern to match against. If omitted, (optional)</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public ObjInteractSessionActionResult Lookup(string nameLike,
                                                           out ObjectInteractionSessionDataResponse responseData,
                                                           ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if (null == nameLike)
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return ObjInteractSessionActionResult.Failed;
                }

                var request = CreateWebRequest("/sessions?nameLike=" + nameLike + "&view=" + viewType.GetDescription(), WebRequestOption.Authorization);
                object responseDataObj;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(ObjectInteractionSessionDataResponse), out responseDataObj);
                if (null != responseDataObj)
                {
                    responseData = responseDataObj as ObjectInteractionSessionDataResponse;
                    if (responseData != null)
                    {
                        responseData.HTTPStatusCode = returnHTTPCode;
                        if (responseData.HTTPStatusCode == HttpStatusCode.OK)
                            return ObjInteractSessionActionResult.Successful;
                    }
                }
                return ObjInteractSessionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return ObjInteractSessionActionResult.Failed;
            }
        }

    }
}
