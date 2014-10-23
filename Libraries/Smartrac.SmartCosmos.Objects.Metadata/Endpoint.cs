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
using System.Collections.Generic;
using System.Net;
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
    internal class MetadataEndpoint : BaseObjectsEndpoint, IMetadataEndpoint
    {
        // Functions for type safe decoding

        #region TypeSafeEncoding

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(bool value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(bool value, out TypeSafeEncodingResponse responseData)
        {
            return Encode(Convert.ToString(value), out responseData, MetadataDataType.Boolean);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(int value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(int value, out TypeSafeEncodingResponse responseData)
        {
            return Encode(Convert.ToString(value), out responseData, MetadataDataType.Boolean);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(DateTime value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(DateTime value, out TypeSafeEncodingResponse responseData)
        {
            return Encode(Rfc3339DateTime.ToString(value), out responseData, MetadataDataType.Date);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(long value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(long value, out TypeSafeEncodingResponse responseData)
        {
            return Encode(Convert.ToString(value), out responseData, MetadataDataType.Long);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(double value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(double value, out TypeSafeEncodingResponse responseData)
        {
            return Encode(Convert.ToString(value), out responseData, MetadataDataType.Double);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(float value, out string encodedValue)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(float value, out TypeSafeEncodingResponse responseData)
        {
            return Encode(Convert.ToString(value), out responseData, MetadataDataType.Float);
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(string value, out string encodedValue, MetadataDataType metaDataType = MetadataDataType.String)
        {
            TypeSafeEncodingResponse responseData;
            MetadataActionResult result = Encode(value, out responseData, metaDataType);
            encodedValue = (null == responseData) ? "" : responseData.rawValue;
            return result;
        }

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Encode(string Value, out TypeSafeEncodingResponse responseData, MetadataDataType metaDataType = MetadataDataType.String)
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

                HttpWebResponse response = request.GetResponseSafe() as System.Net.HttpWebResponse;
                if (response != null)
                {
                    try
                    {
                        responseData = responseData.FromJSON(response.GetResponseStream());
                        if (responseData != null)
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
                    finally
                    {
                        response.Close();
                    }
                }

                return MetadataActionResult.Failed;
            }
            catch (Exception e)
            {
                if (null != Logger)
                    Logger.AddLog(e.Message, LogType.Error);
                return MetadataActionResult.Failed;
            }
        }

        #endregion TypeSafeEncoding

        // Functions for type safe encoding

        #region TypeSafeDecoding

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out TypeSafeDecodingResponse responseData, MetadataDataType metaDataType = MetadataDataType.String)
        {
            return Decode(new TypeSafeDecodingRequest
                                        {
                                            dataTypeObj = metaDataType,
                                            rawValue = value
                                        },
                                    out responseData);
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out string decodedValue, MetadataDataType metaDataType = MetadataDataType.String)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, metaDataType);
            decodedValue = (null == responseData) ? "" : responseData.decodedValue;
            return result;
        }

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out DateTime decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, MetadataDataType.Date);

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
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out long decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, MetadataDataType.Long);

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
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out int decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, MetadataDataType.Integer);

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
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out bool decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, MetadataDataType.Boolean);

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
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out float decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, MetadataDataType.Float);

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
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(string value, out double decodedValue)
        {
            TypeSafeDecodingResponse responseData;
            MetadataActionResult result = Decode(value, out responseData, MetadataDataType.Double);

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
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Decode(TypeSafeDecodingRequest requestData, out TypeSafeDecodingResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            var request = CreateWebRequest("/metadata/mapper/encode/" + requestData.dataTypeObj.GetDescription(), WebRequestOption.Authorization);
            ExecuteWebRequestJSON<TypeSafeDecodingRequest, TypeSafeDecodingResponse>(request, requestData, out responseData);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return MetadataActionResult.Successful;
                    default: return MetadataActionResult.Failed;
                }
            }
            return MetadataActionResult.Failed;
        }

        #endregion TypeSafeDecoding

        /// <summary>
        /// Inserts a new key-value or updates an existing key value related to the specified entity
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Create(CreateMetadataRequest requestData, out CreateMetadataResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            var request = CreateWebRequest("/metadata/" + requestData.entityReferenceType.GetDescription() + "/" + requestData.entityUrn.UUID, WebRequestOption.Authorization);
            ExecuteWebRequestJSON<List<MetadataItem>, CreateMetadataResponse>(request, requestData.MetaDataList, out responseData);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return MetadataActionResult.Successful;
                    default: return MetadataActionResult.Failed;
                }
            }

            return MetadataActionResult.Failed;
        }

        /// <summary>
        /// Deletes an existing metadata key by its system-assigned URN key
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data, is null in case of success</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Delete(DeleteMetadataRequest requestData, out DeleteMetadataResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            var request = CreateWebRequest("/metadata/" + requestData.entityReferenceType.GetDescription() + "/" + requestData.entityUrn.UUID + "/" + requestData.key);
            request.Method = "DELETE";

            HttpWebResponse response = request.GetResponseSafe() as System.Net.HttpWebResponse;
            if (response != null)
            {
                try
                {
                    if ((response.StatusCode == HttpStatusCode.NoContent) &&
                        (response.Headers.Get("SmartCosmos-Event") == "MetadataDeleted"))
                    {
                        return MetadataActionResult.Successful;
                    }
                    else
                    {
                        responseData = responseData.FromJSON(response.GetResponseStream());
                        if (responseData is IHttpStatusCode)
                        {
                            (responseData as IHttpStatusCode).HTTPStatusCode = response.StatusCode;
                        }
                    }
                }
                finally
                {
                    response.Close();
                }
            }

            return MetadataActionResult.Failed;
        }

        /// <summary>
        /// Lookup all metadata that matches the specified key pattern
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        public MetadataActionResult Lookup(LookupMetadataRequest requestData, out LookupMetadataResponse responseData)
        {
            responseData = null;
            if ((null == requestData) || !requestData.IsValid())
            {
                if (null != Logger)
                    Logger.AddLog("request data is invalid", LogType.Error);
                return MetadataActionResult.Failed;
            }

            //metadata/{entityReferenceType}/{entityUrn}{?view,key}
            var request = CreateWebRequest("/metadata/" +
                                        requestData.entityReferenceType.GetDescription() + "/" +
                                        requestData.entityUrn.UUID +
                                        "?view=" + requestData.viewType.GetDescription() +
                                        requestData.key ?? "&key" + requestData.key
                                        , WebRequestOption.Authorization);
            ExecuteWebRequestJSON<LookupMetadataResponse>(request, out responseData);
            if (responseData != null)
            {
                switch (responseData.HTTPStatusCode)
                {
                    case HttpStatusCode.OK: return MetadataActionResult.Successful;
                    default: return MetadataActionResult.Failed;
                }
            }

            return MetadataActionResult.Failed;
        }
    }
}