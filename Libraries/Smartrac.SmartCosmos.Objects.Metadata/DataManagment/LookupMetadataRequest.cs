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
    public class LookupMetadataRequest : BaseRequest
    {
        /// <summary>
        /// Valid EntityReferenceType enum value
        /// </summary>
        public EntityReferenceType entityReferenceType { get; set; }

        /// <summary>
        /// Case-sensitive urn of an existing entity of type entityReferenceType
        /// </summary>
        public Urn referenceUrn { get; set; }

        /// <summary>
        /// Optional: A valid, case-sensitive startsWith key name pattern. If omitted, then all key-values are returned.
        /// </summary>
        [DataMember(IsRequired=false)]
        public string key { get; set; }
        
        /// <summary>
        /// Optional: A valid, case-sensitive startsWith key name pattern. If omitted, then all key-values are returned.
        /// </summary>
        [DataMember]
        public ViewType viewType { get; set; }

        public LookupMetadataRequest() : base()
        {
            this.key = null;
            this.viewType = ViewType.Standard;
        }

        public override bool IsValid()
        {
            return base.IsValid() &&
                (referenceUrn != null) &&
                referenceUrn.IsValid();
        }
    }
}
