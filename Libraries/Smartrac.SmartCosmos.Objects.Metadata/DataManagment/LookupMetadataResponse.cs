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
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System;
using System.Collections.Generic;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    public class LookupMetadataResponse : List<MetadataLookupItem>, IResponseMessage, IHttpStatusCode
    {
        //IResponseMessage
        public int code { get; set; }

        public string message { get; set; }

        //IHttpStatusCode
        public HttpStatusCode HTTPStatusCode { get; set; }

        public LookupMetadataResponse()
            : base()
        {
            this.code = 0;
            this.HTTPStatusCode = HttpStatusCode.NotImplemented;
        }
    }

    public class MetadataLookupItem
    {
        public MetadataLookupItem()
            : base()
        {
            moniker = null;
            account = null;
            lastModifiedTimestamp = null;
        }

        public string dataType
        {
            get
            {
                return dataTypeObj.GetDescription();
            }

            set
            {
                dataTypeObj = MetadataDataType.Boolean.GetFlagByDescription(value);
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public MetadataDataType dataTypeObj { get; set; }

        public string entityReferenceType
        {
            get
            {
                return entityReferenceTypeObj.GetDescription();
            }

            set
            {
                entityReferenceTypeObj = EntityReferenceType.Object.GetFlagByDescription(value);
            }
        }

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

        [JsonConverter(typeof(StringEnumConverter))]
        public EntityReferenceType entityReferenceTypeObj { get; set; }

        /// <summary>
        /// Opaque Base64 encoded value when using built-in encoding
        /// </summary>

        public string rawValue { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public DataIAccount account { get; set; }

        /// <summary>
        /// Serialization Level: Standard
        /// </summary>
        public long? lastModifiedTimestamp { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public string moniker { get; set; }

        public string key { get; set; }
    }
}