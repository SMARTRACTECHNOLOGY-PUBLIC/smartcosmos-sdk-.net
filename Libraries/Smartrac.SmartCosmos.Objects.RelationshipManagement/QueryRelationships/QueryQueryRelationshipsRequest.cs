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
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.RelationshipManagement
{
    public class QueryQueryRelationshipsRequest : BaseRequest
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

        public override bool IsValid()
        {
            return base.IsValid() &&
                referenceUrnObj.IsValid() &&
                relatedReferenceUrnObj.IsValid(); //&&
            //!String.IsNullOrEmpty(type) &&
            //type.Length <= 255;
        }
    }

    public class QueryQueryRelationshipByTypeRequest : QueryQueryRelationshipsRequest
    {
        /// <summary>
        /// Case-sensitive type of relationship to look for
        /// </summary>
        public string type { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() &&
              !String.IsNullOrEmpty(type) &&
              type.Length <= 255;
        }
    }

    public class QueryQueryRelationshipsByTypeRequest : BaseRequest
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
        /// Case-sensitive type of relationship to look for
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// Flips the query around, finding those relationships where the entityReferenceType and referenceUrn are in the related fields(defalts to false)
        /// </summary>
        public bool reverse { get; set; }

        public QueryQueryRelationshipsByTypeRequest()
            : base()
        {
            reverse = false;
        }

        public override bool IsValid()
        {
            return base.IsValid() &&
                referenceUrnObj.IsValid() &&
                !String.IsNullOrEmpty(type) &&
                type.Length <= 255;
        }
    }
}