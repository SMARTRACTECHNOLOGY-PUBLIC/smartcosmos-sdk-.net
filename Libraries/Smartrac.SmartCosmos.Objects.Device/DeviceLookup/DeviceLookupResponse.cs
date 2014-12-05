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

using Newtonsoft.Json;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using System.Collections.Generic;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.Device
{
    public class DeviceLookupsResponse : List<DeviceLookupResponse>, IHttpStatusCode
    {
        public HttpStatusCode HTTPStatusCode { get; set; }

        public DeviceLookupsResponse()
            : base()
        {
            this.HTTPStatusCode = HttpStatusCode.NotImplemented;
        }
    }

    public class DeviceLookupResponse : DefaultResponse
    {
        [JsonIgnore]
        public Urn referenceUrnObj { get; set; }

        public string urn
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

        public long lastModifiedTimestamp { get; set; }

        public string moniker { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public string identification { get; set; }

        public string type { get; set; }

    }
}