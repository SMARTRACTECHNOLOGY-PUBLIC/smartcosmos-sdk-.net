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
#endregion

using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Runtime.Serialization;
using System.Text;
using GeoJSON.Net;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    [DataContract]
    public class GeospatialEntryDataResponse : BaseResponse
    {
        [DataMember(IsRequired = true)]
        public string urn
        {
            get
            {
                return (geospatialUrn != null) ? geospatialUrn.UUID : "";
            }
            set
            {
                geospatialUrn = new Urn(value);
            }
        }
        public Urn geospatialUrn { get; set; }

        [DataMember]
        public string name { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public bool activeFlag { get; set; }
        [DataMember]
        public long lastModifiedTimestamp { get; set; }
        [DataMember]
        public string moniker { get; set; }
        [DataMember]
        public GeoJSONObject geometricShape { get; set; }

        public GeospatialEntryDataResponse()
            : base()
        {
            description = null;
            activeFlag = true;
            moniker = null;
        }
    }
 
}