﻿#region License

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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;
using System;

namespace Smartrac.SmartCosmos.Objects.RelationshipManagement
{
    public class RelationshipManagementRequest : BaseRequest
    {
        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        /// <summary>
        /// referenceUrn must point to a pre-existing record of the given entityReferenceType
        /// </summary>
        public string referenceUrn
        {
            get
            {
                return (referenceUrnObj != null) ? referenceUrnObj.UUID : "";
            }
            set
            {
                referenceUrnObj = new Urn(value);
            }
        }

        /// <summary>
        /// referenceUrn must point to a pre-existing record of the given entityReferenceType
        /// </summary>
        [JsonIgnore]
        public Urn referenceUrnObj { get; set; }

        /// <summary>
        /// type is required and constrained to 255 characters
        /// </summary>

        public string type { get; set; }

        /// <summary>
        /// relatedEntityReferenceType is required and constrained to a valid EntityReferenceType value defined
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType relatedEntityReferenceType { get; set; }

        /// <summary>
        /// relatedReferenceUrn must point to a pre-existing record of the given relatedEntityReferenceType
        /// </summary>

        public string relatedReferenceUrn
        {
            get
            {
                return (relatedReferenceUrnObj != null) ? relatedReferenceUrnObj.UUID : "";
            }
            set
            {
                relatedReferenceUrnObj = new Urn(value);
            }
        }

        [JsonIgnore]
        public Urn relatedReferenceUrnObj { get; set; }

        /// <summary>
        /// moniker is optional and constrained to 2048 characters may be omitted; defaults to null
        /// </summary>

        public string moniker { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() &&
                referenceUrnObj.IsValid() &&
                relatedReferenceUrnObj.IsValid() &&
                !String.IsNullOrEmpty(type) &&
                type.Length <= 255 &&
                (String.IsNullOrEmpty(moniker) || moniker.Length <= 2048);
        }
    }
}