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
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.ComponentModel;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    public enum MetadataDataType
    {
        [Description("StringType")]
        String,

        [Description("DateType")]
        Date,

        [Description("IntegerType")]
        Integer,

        [Description("LongType")]
        Long,

        [Description("BooleanType")]
        Boolean,

        [Description("FloatType")]
        Float,

        [Description("DoubleType")]
        Double,

        [Description("JSONType")]
        JSON,

        [Description("XMLType")]
        XML,

        [Description("Custom")]
        Custom
    }

    public enum MetadataActionResult
    {
        /// <summary>
        /// action was successful
        /// </summary>
        Successful,

        /// <summary>
        /// problem occurred, check message parameter for detailed information
        /// </summary>
        Failed
    }

    public interface IMetadataEndpoint : IBaseEndpoint
    {
        // Functions for type safe decoding

        #region TypeSafeEncoding

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(bool value, out string encodedValue);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(bool value, out TypeSafeEncodingResponse responseData);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(int value, out string encodedValue);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(int value, out TypeSafeEncodingResponse responseData);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(DateTime value, out string encodedValue);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(DateTime value, out TypeSafeEncodingResponse responseData);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(long value, out string encodedValue);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(long value, out TypeSafeEncodingResponse responseData);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(double value, out string encodedValue);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(double value, out TypeSafeEncodingResponse responseData);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(float value, out string encodedValue);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="Value">value</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(float value, out TypeSafeEncodingResponse responseData);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(string value, out string encodedValue,
            MetadataDataType metaDataType = MetadataDataType.String);

        /// <summary>
        /// Encodes a strongly typed value using the platform's built-in encoder
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="encodedValue">encodedValue</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Encode(string Value,
            out TypeSafeEncodingResponse responseData, MetadataDataType metaDataType = MetadataDataType.String);

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
        MetadataActionResult Decode(string value,
            out TypeSafeDecodingResponse responseData,
            MetadataDataType metaDataType = MetadataDataType.String);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out string decodedValue, MetadataDataType metaDataType = MetadataDataType.String);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out DateTime decodedValue);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out long decodedValue);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out int decodedValue);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out bool decodedValue);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out float decodedValue);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <param name="metaDataType">Valid MetadataDataType enum value</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(string value, out double decodedValue);

        /// <summary>
        /// Decodes a strongly typed value previously encoded using the platform's built-in encoder
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Decode(TypeSafeDecodingRequest requestData, out TypeSafeDecodingResponse responseData);

        #endregion TypeSafeDecoding

        /// <summary>
        /// Inserts a new key-value or updates an existing key value related to the specified entity
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Create(CreateMetadataRequest requestData, out CreateMetadataResponse responseData);

        /// <summary>
        /// Deletes an existing metadata key by its system-assigned URN key
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data, is null in case of success</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Delete(DeleteMetadataRequest requestData, out DeleteMetadataResponse responseData);

        /// <summary>
        /// Lookup all metadata that matches the specified key pattern
        /// </summary>
        /// <param name="requestData">request data</param>
        /// <param name="responseData">response data</param>
        /// <returns>MetadataActionResult</returns>
        MetadataActionResult Lookup(Urn referenceUrn, EntityReferenceType entityReferenceType, string key, out LookupMetadataResponse responseData, ViewType viewType = ViewType.Standard);
    }
}