using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagMetadata
{
    /// <summary>
    /// Client for tag metadata endpoint
    /// </summary>
    public class TagMetadataEndpoint : CommonEndpoint
    {
        public TagMetadataEndpoint(string aServerURL, bool allowInvalidServerCertificates)
            : base(aServerURL, allowInvalidServerCertificates)
        {
        }


        /// <summary>
        /// Get tag related data
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode GetTagMetadata(TagMetaDataRequest requestData, out TagMetaDataResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/tag/properties", WebRequestOption.Authorization);
                object responseDataObj = null;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(TagMetaDataRequest), requestData, typeof(TagMetaDataResponse), out responseDataObj);

                if (returnHTTPCode == HttpStatusCode.OK)
                {
                    responseData = responseDataObj as TagMetaDataResponse;
                }
                return returnHTTPCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        /// <summary>
        /// Get a message to a single tag code
        /// </summary>
        /// <param name="requestData">Input data</param>
        /// <param name="responseData">Output data</param>
        /// <returns>HTTP status code</returns>
        public HttpStatusCode GetTagMessage(TagMessageRequest requestData, out TagMessageResponse responseData)
        {
            responseData = null;
            try
            {
                var request = CreateWebRequest("/tag/message", WebRequestOption.Authorization | WebRequestOption.AcceptLanguage);
                object responseDataObj = null;
                var returnHTTPCode = ExecuteWebRequestJSON(request, typeof(TagMessageRequest), requestData, typeof(TagMessageResponse), out responseDataObj);

                if (returnHTTPCode == HttpStatusCode.OK)
                {
                    responseData = responseDataObj as TagMessageResponse;
                }
                return returnHTTPCode;
            }
            catch
            {
                return HttpStatusCode.InternalServerError;
            }
        }
    
    }
}
