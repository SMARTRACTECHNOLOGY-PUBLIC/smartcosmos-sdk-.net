﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagVerification
{
    /// <summary>
    /// Client for tag verification endpoint
    /// </summary>
    public class TagVerificationEndpoint : CommonEndpoint
    {
        public TagVerificationEndpoint(string aServerURL, bool allowInvalidServerCertificates, IMessageLogger logger)
            : base(aServerURL, allowInvalidServerCertificates, logger)
        {
        }
        

        /// <summary>
        /// Verify tags for a verification type
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode VerifyTags(VerifyTagsRequest requestData, out VerifyTagsResponse responseData)
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
                return returnHTTPCode;
            }
            catch(Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HttpStatusCode.InternalServerError;
            }
        }


        /// <summary>
        /// Get a message to a single verification state
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode GetVerificationMessage(VerificationMessageRequest requestData, out VerificationMessageResponse responseData)
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
                return returnHTTPCode;
            }
            catch(Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return HttpStatusCode.InternalServerError;
            }
        }

    }
}