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

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
{
    /// <summary>
    /// Client for tag metadata endpoint
    /// </summary>
    internal class TagMetadataEndpoint : BaseProfileEndpoint, ITagMetadataEndpoint
    {
        /// <summary>
        /// Get tag related data
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagMetaDataActionResult</returns>
        public TagMetaDataActionResult GetTagMetadata(TagMetaDataRequest requestData, out TagMetaDataResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/tag/properties", WebRequestOption.Authorization);
                var returnHTTPCode = ExecuteWebRequestJSON<TagMetaDataRequest, TagMetaDataResponse>(request, requestData, out responseData);

                if (returnHTTPCode == HttpStatusCode.OK)
                    return TagMetaDataActionResult.Successful;
                else
                    return TagMetaDataActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagMetaDataActionResult.Failed;
            }
        }

        /// <summary>
        /// Get a message to a single tag code
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagMetaDataActionResult</returns>
        public TagMetaDataActionResult GetTagMessage(TagMessageRequest requestData, out TagMessageResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/tag/message", WebRequestOption.Authorization | WebRequestOption.AcceptLanguage);
                var returnHTTPCode = ExecuteWebRequestJSON<TagMessageRequest, TagMessageResponse>(request, requestData, out responseData);

                if (returnHTTPCode == HttpStatusCode.OK)
                    return TagMetaDataActionResult.Successful;
                else
                    return TagMetaDataActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagMetaDataActionResult.Failed;
            }
        }
    }
}