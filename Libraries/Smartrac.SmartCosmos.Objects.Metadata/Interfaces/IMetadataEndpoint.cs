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