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

#endregion License

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectInteraction
{
    public class ObjectInteractionItem
    {
        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        public bool hasSessionMembership { get; set; }

        public long lastModifiedTimestamp { get; set; }

        [JsonProperty(PropertyName = "object")]
        public ObjectInteractionData objectData { get; set; }

        public string objectInteractionSession { get; set; }

        public string type { get; set; }

        public long recordedTimestamp { get; set; }

        public string interactionUrn
        {
            get
            {
                return (interactionUrnObj != null) ? interactionUrnObj.UUID : "";
            }
            set
            {
                interactionUrnObj = new Urn(value);
            }
        }

        [JsonIgnore]
        public Urn interactionUrnObj { get; set; }

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

        [JsonIgnore]
        public Urn referenceUrnObj { get; set; }
    }

    public class ObjectInteractionData
    {
        public bool activeFlag { get; set; }

        public string description { get; set; }

        public long lastModifiedTimestamp { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string interactionUrn
        {
            get
            {
                return (interactionUrnObj != null) ? interactionUrnObj.UUID : "";
            }
            set
            {
                interactionUrnObj = new Urn(value);
            }
        }

        public Urn interactionUrnObj { get; set; }

        public string urn
        {
            get
            {
                return (urnObj != null) ? urnObj.UUID : "";
            }
            set
            {
                urnObj = new Urn(value);
            }
        }

        public Urn urnObj { get; set; }
    }
}