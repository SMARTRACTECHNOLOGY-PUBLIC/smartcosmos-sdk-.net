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

namespace Smartrac.SmartCosmos.Objects.BatchTransmission
{
    /// <summary>
    /// Client for BatchTransmission Endpoints
    /// </summary>
    internal class BatchTransmissionEndpoint : BaseObjectsEndpoint, IBatchTransmissionEndpoint
    {
        /// <summary>
        /// file Transmission Request
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>BatchTransmissionActionResult</returns>
        public BatchTransmissionActionResult FileTransmissionRequest(FileTransmissionRequestRequest requestData, out FileTransmissionRequestResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return BatchTransmissionActionResult.Failed;
            }

            var request = CreateWebRequest("batch", WebRequestOption.Authorization);
            ExecuteWebRequestJSON<FileTransmissionRequestRequest, FileTransmissionRequestResponse>(request, requestData, out responseData, WebRequestMethods.Http.Put);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.Created:
                    case HttpStatusCode.OK: return BatchTransmissionActionResult.Successful;
                    default: return BatchTransmissionActionResult.Failed;
                }
            }
            return BatchTransmissionActionResult.Failed;
        }

        /// <summary>
        /// file Transmission Receipt
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>BatchTransmissionActionResult</returns>
        public BatchTransmissionActionResult FileTransmissionReceipt(FileTransmissionReceiptRequest requestData, out FileTransmissionReceiptResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return BatchTransmissionActionResult.Failed;
            }

            var request = CreateWebRequest("batch", WebRequestOption.Authorization);
            ExecuteWebRequestJSON<FileTransmissionReceiptRequest, FileTransmissionReceiptResponse>(request, requestData, out responseData);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.NoContent: return BatchTransmissionActionResult.Successful;
                    default: return BatchTransmissionActionResult.Failed;
                }
            }
            return BatchTransmissionActionResult.Failed;
        }

        /// <summary>
        /// Lookup a specific transmission by its system-assigned URN key
        /// </summary>
        /// <param name="urn">System-assigned URN assigned at creation</param>
        /// <param name="viewType">A valid JSON Serialization View name (case-sensitive)</param>
        /// <param name="responseData">Object data</param>
        /// <returns>ObjectManagementActionResult</returns>
        public BatchTransmissionActionResult Lookup(Urn urn, out FileTransmissionDataResponse responseData, ViewType viewType = ViewType.Standard)
        {
            responseData = null;
            try
            {
                if ((null == urn) || (!urn.IsValid()))
                {
                    if (null != Logger)
                        Logger.AddLog("urn is not valid", LogType.Error);
                    return BatchTransmissionActionResult.Failed;
                }

                Uri url = new Uri("/batch", UriKind.Relative).
                    AddSubfolder(urn.UUID);

                var request = CreateWebRequest(url, WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<FileTransmissionDataResponse>(request, out responseData);
                if (((responseData != null) && (responseData.HTTPStatusCode == HttpStatusCode.OK)) || responseData.HTTPStatusCode == HttpStatusCode.NoContent)
                    return BatchTransmissionActionResult.Successful;
                return BatchTransmissionActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return BatchTransmissionActionResult.Failed;
            }
        }
    }
}