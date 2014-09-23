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
using System.Runtime.Serialization;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    [DataContract]
    public class LookupMetadataResponse : DefaultResponse
    {
        [DataMember]
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

        [DataMember]
        public MetadataDataType dataTypeObj { get; set; }

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        public EntityReferenceType entityReferenceTypeObj { get; set; }

        /// <summary>
        /// Opaque Base64 encoded value when using built-in encoding
        /// </summary>
        [DataMember]
        public string rawValue { get; set; }
    }
}
