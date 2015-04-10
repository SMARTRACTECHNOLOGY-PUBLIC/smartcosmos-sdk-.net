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
using Smartrac.SmartCosmos.ClientEndpoint.Base;

namespace Smartrac.SmartCosmos.Objects.Base
{
    public class DataIObject
    {
        public bool activeFlag { get; set; }

        public string description { get; set; }

        public long lastModifiedTimestamp { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string referenceUrn
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
    }
}