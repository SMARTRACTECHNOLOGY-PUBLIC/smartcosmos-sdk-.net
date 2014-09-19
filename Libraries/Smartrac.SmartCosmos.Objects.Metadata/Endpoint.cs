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
using Smartrac.Base;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    /// <summary>
    /// Client for Metadata Endpoints
    /// </summary>
    class MetadataEndpoint : BaseObjectsEndpoint, IMetadataEndpoint
    {
        #region TypeSafeEncoding

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(bool value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(bool value, out TypeSafeEncodingResponse responseData)
        {
            return TypeSafeEncoding(Convert.ToString(value), out responseData, MetadataDataType.Boolean);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(int value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(int value, out TypeSafeEncodingResponse responseData)
        {
            return TypeSafeEncoding(Convert.ToString(value), out responseData, MetadataDataType.Boolean);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(DateTime value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(DateTime value, out TypeSafeEncodingResponse responseData)
        {
            return TypeSafeEncoding(Rfc3339DateTime.ToString(value), out responseData, MetadataDataType.Date);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(long value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(long value, out TypeSafeEncodingResponse responseData)
        {
            return TypeSafeEncoding(Convert.ToString(value), out responseData, MetadataDataType.Long);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(double value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(double value, out TypeSafeEncodingResponse responseData)
        {
            return TypeSafeEncoding(Convert.ToString(value), out responseData, MetadataDataType.Double);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(float value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(float value, out TypeSafeEncodingResponse responseData)
        {
            return TypeSafeEncoding(Convert.ToString(value), out responseData, MetadataDataType.Float);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult TypeSafeEncoding(string value, out string encodedValue, MetadataDataType metaDataType = MetadataDataType.String)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = TypeSafeEncoding(value, out responseData, metaDataType);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</paramMetadata>
        /// <param name="encodedValue">encodedValue</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeEncoding(string Value, out TypeSafeEncodingResponse responseData, MetadataDataType metaDataType = MetadataDataType.String)
        {
            responseData = null;
            try
            {
                if (!String.IsNullOrEmpty(Value))
                {
                    if (null != Logger)
                        Logger.AddLog("value is missing", LogType.Error);
                    return MetadataActionResult.Failed;
                }

                var request = CreateWebRequest("/metadata/mapper/encode/" + metaDataType.GetDescription());
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "text/plain";
                request.ContentLength = Value.Length;
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(Encoding.UTF8.GetBytes(Value), 0, Value.Length);
                }

                using (var response = request.GetResponse() as System.Net.HttpWebResponse)
                {
                    if (response == null)
                    {
                        return MetadataActionResult.Failed;
                    }

                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(TypeSafeEncodingResponse));
                    responseData = serializer.ReadObject(response.GetResponseStream()) as TypeSafeEncodingResponse;
                    if (responseData == null)
                    {
                        return MetadataActionResult.Failed;
                    }
                    else
                    {
                        responseData.HTTPStatusCode = response.StatusCode;
                        switch (response.StatusCode)
                        {
                            case HttpStatusCode.OK: return MetadataActionResult.Successful;
                            case HttpStatusCode.BadRequest: return MetadataActionResult.Failed;
                            default: return MetadataActionResult.Failed;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return MetadataActionResult.Failed;
            }
        }

        #endregion

        #region TypeSafeDecoding
        
        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out TypeSafeDecodingResponse responseData, MetadataDataType metaDataType = MetadataDataType.String)
        {
            return TypeSafeDecoding(new TypeSafeDecodingRequest 
                                        { 
                                            dataTypeObj = metaDataType,
                                            rawValue = value
                                        }, 
                                    out responseData);
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out string decodedValue, MetadataDataType metaDataType = MetadataDataType.String)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, metaDataType);
            decodedValue = (null == responseData) ? "" : responseData.decodedValue;
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out DateTime decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, MetadataDataType.Date);

            if (null == responseData)
                Rfc3339DateTime.TryParse(responseData.decodedValue, out decodedValue);
            else
            {
                decodedValue = DateTime.MinValue;
                return MetadataActionResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out long decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, MetadataDataType.Long);

            if (null == responseData)
            {
                decodedValue = Convert.ToInt64(responseData.decodedValue);
            }
            else
            {
                decodedValue = 0;
                return MetadataActionResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out int decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, MetadataDataType.Integer);

            if (null == responseData)
            {
                decodedValue = Convert.ToInt32(responseData.decodedValue);
            }
            else
            {
                decodedValue = 0;
                return MetadataActionResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out bool decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, MetadataDataType.Boolean);

            if (null == responseData)
            {
                decodedValue = Convert.ToBoolean(responseData.decodedValue);
            }
            else
            {
                decodedValue = false;
                return MetadataActionResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out float decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, MetadataDataType.Float);

            if (null == responseData)
            {
                float.TryParse(responseData.decodedValue, out decodedValue);
            }
            else
            {
                decodedValue = 0;
                return MetadataActionResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <param name="metaDataType">Valid MetadataDataType enum value</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(string value, out double decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = TypeSafeDecoding(value, out responseData, MetadataDataType.Double);

            if (null == responseData)
            {
                double.TryParse(responseData.decodedValue, out decodedValue);
            }
            else
            {
                decodedValue = 0;
                return MetadataActionResult.Failed;
            }
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult TypeSafeDecoding(TypeSafeDecodingRequest requestData, out TypeSafeDecodingResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            var request = CreateWebRequest("/metadata/mapper/encode/" + requestData.dataTypeObj.GetDescription(), WebRequestOption.Authorization);
            object responseDataObj = null;
            ExecuteWebRequestJSON(request, typeof(TypeSafeDecodingRequest), requestData, typeof(TypeSafeDecodingResponse), out responseDataObj);
            if (null != responseDataObj)
            {
                responseData = responseDataObj as TypeSafeDecodingResponse;
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return MetadataActionResult.Successful;
                        default: return MetadataActionResult.Failed;
                    }
                }
            }
            return MetadataActionResult.Failed;
        }

        #endregion

        /// <summary>
        /// Inserts a new key-value or updates an existing key value related to the specified entity
        /// </summary>
        /// <param name="requestData">request data</paramMetadata>
        /// <param name="responseData">response data</paramMetadata>
        /// <returns>MetadataActionResult</returns>
        protected MetadataActionResult MetadataUpsertion(TypeSafeDecodingRequest requestData, out TypeSafeDecodingResponse responseData)
        {
            // continue here
            responseData = null;
            return MetadataActionResult.Failed; 
            /*
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            var request = CreateWebRequest("/metadata/mapper/encode/" + requestData.dataTypeObj.GetDescription(), WebRequestOption.Authorization);
            object responseDataObj = null;
            ExecuteWebRequestJSON(request, typeof(TypeSafeDecodingRequest), requestData, typeof(TypeSafeDecodingResponse), out responseDataObj);
            if (null != responseDataObj)
            {
                responseData = responseDataObj as TypeSafeDecodingResponse;
                if (responseData != null)
                {
                    switch (responseData.HTTPStatusCode)
                    {
                        case HttpStatusCode.OK: return MetadataActionResult.Successful;
                        default: return MetadataActionResult.Failed;
                    }
                }
            }
            return MetadataActionResult.Failed;
             */ 
        }
    
    
    }
}
