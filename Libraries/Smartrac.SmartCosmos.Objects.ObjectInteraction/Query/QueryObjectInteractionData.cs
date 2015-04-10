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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectInteraction
{
    public class ObjectInteractionItem
    {
        public ObjectInteractionItem()
            : base()
        {
            lastModifiedTimestamp = null;
            account = null;
            moniker = null;
            objectInteractionSession = null;
            recordedTimestamp = null;
            receivedTimestamp = null;
        }

        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceType { get; set; }

        public bool hasSessionMembership { get; set; }

        /// <summary>
        /// Serialization Level: Standard
        /// </summary>
        public long? lastModifiedTimestamp { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public DataIAccount account { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public string moniker { get; set; }

        [JsonProperty(PropertyName = "object")]
        public DataIObject objectData { get; set; }

        /// <summary>
        /// Serialization Level: Standard
        /// </summary>
        public DataIObjectInteractionSession objectInteractionSession { get; set; }

        public string type { get; set; }

        /// <summary>
        /// Serialization Level: Standard
        /// </summary>
        public long? recordedTimestamp { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public long? receivedTimestamp { get; set; }

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

        [JsonIgnore]
        public Urn urnObj { get; set; }

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
}