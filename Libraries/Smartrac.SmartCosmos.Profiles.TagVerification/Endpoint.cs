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
using Smartrac.SmartCosmos.Profiles.Base;

namespace Smartrac.SmartCosmos.Profiles.TagVerification
{
    /// <summary>
    /// Client for tag verification endpoint
    /// </summary>
    class TagVerificationEndpoint : BaseProfileEndpoint, ITagVerificationEndpoint
    {
        /// <summary>
        /// Verify tags for a verification type
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagVerificationActionResult</returns>
        public TagVerificationActionResult VerifyTags(VerifyTagsRequest requestData, out VerifyTagsResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/verification/tags", WebRequestOption.Authorization);
                object responseDataObj = null;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(VerifyTagsRequest), requestData, typeof(VerifyTagsResponse), out responseDataObj);

                if (null != responseDataObj)
                {
                    responseData = responseDataObj as VerifyTagsResponse;
                }
                if (returnHTTPCode == HttpStatusCode.OK)
                    return TagVerificationActionResult.Successful;
                else
                    return TagVerificationActionResult.Failed;
            }
            catch(Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagVerificationActionResult.Failed;
            }
        }


        /// <summary>
        /// Get a message to a single verification state
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>TagVerificationActionResult</returns>
        public TagVerificationActionResult GetVerificationMessage(VerificationMessageRequest requestData, out VerificationMessageResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/verification/message", WebRequestOption.Authorization | WebRequestOption.AcceptLanguage);
                object responseDataObj = null;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(VerificationMessageRequest), requestData, typeof(VerificationMessageResponse), out responseDataObj);

                if (null != responseDataObj)
                {
                    responseData = responseDataObj as VerificationMessageResponse;
                }
                if (returnHTTPCode == HttpStatusCode.OK)
                    return TagVerificationActionResult.Successful;
                else
                    return TagVerificationActionResult.Failed;
            }
            catch(Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return TagVerificationActionResult.Failed;
            }
        }

    }
}