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

using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectInteraction
{
    [DataContract]
    public class QueryObjectInteractionsResponse : List<QueryObjectInteractionsData>
    {
        public HttpStatusCode HTTPStatusCode { get; set; }

        public QueryObjectInteractionsResponse()
            : base()
        {
            this.HTTPStatusCode = HttpStatusCode.NotImplemented;
        }
    }

    /*
[
    {
       "entityReferenceType": "Object",
       "hasSessionMembership": false,
       "lastModifiedTimestamp": 1390779082063,
       "object": {
          "activeFlag": true,
          "description": null,
          "lastModifiedTimestamp": 1390779081986,
          "name": "foo",
          "interactionUrn": "urn:instagram:FooBar:ec3819a1-bc9b-4550-afda-4bd8cbb1dd16",
          "type": "Transaction",
          "urn": "urn:uuid:ac912b9e-fa52-48e4-a477-aa99f0a2bf92"
       },
       ">Session": null,
       "recordedTimestamp": 1390779082039,
       "referenceUrn": "urn:instagram:FooQux:47c23bc6-2a58-4e37-93b0-848776b42404",
       "urn": "urn:uuid:95b995e2-dde5-4dd0-b50c-e804999411d8"
    }
]
     */

    [DataContract]
    public class QueryObjectInteractionsData
    {
        /// <summary>
        /// entityReferenceType is required and constrained to a valid EntityReferenceType value
        /// </summary>
        [DataMember]
        public EntityReferenceType entityReferenceType { get; set; }

        [DataMember]
        public bool hasSessionMembership { get; set; }

        [DataMember]
        public long lastModifiedTimestamp { get; set; }

        [DataMember(Name = "object")]
        public List<QueryObjectInteractionsData> objectData { get; set; }

        [DataMember]
        public string objectInteractionSession { get; set; }

        [DataMember]
        public long recordedTimestamp { get; set; }

        [DataMember(IsRequired = true)]
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

        /// <summary>
        /// referenceUrn must point to a pre-existing record of the given entityReferenceType
        /// </summary>
        [DataMember(IsRequired = true)]
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

        public Urn referenceUrnObj { get; set; }
    }

    /*
       "object": {
          "activeFlag": true,
          "description": null,
          "lastModifiedTimestamp": 1390779081986,
          "name": "foo",
          "interactionUrn": "urn:instagram:FooBar:ec3819a1-bc9b-4550-afda-4bd8cbb1dd16",
          "type": "Transaction",
          "urn": "urn:uuid:ac912b9e-fa52-48e4-a477-aa99f0a2bf92"
       },
     */

    [DataContract]
    public class ObjectInteractionsData
    {
        [DataMember]
        public bool activeFlag { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public long lastModifiedTimestamp { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
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

        [DataMember]
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