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
using Smartrac.SmartCosmos.ClientEndpoint.Base;

namespace Smartrac.SmartCosmos.Objects.Device
{
    public class DeviceUpdateRequest : BaseRequest
    {
        //[JsonConverter(typeof(StringEnumConverter))]
        //public EntityReferenceType entityReferenceType { get; set; }

        [JsonIgnore]
        public Urn referenceUrnObj { get; set; }

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

        public string identification { get; set; }

        public string name { get; set; }

        public string type { get; set; }

        public string description { get; set; }

        public bool activeFlag { get; set; }

        public string moniker { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() &&
                ((referenceUrnObj != null && referenceUrnObj.UUID != null) || (identification != null && identification.Length <= 255)) &&
                ((name == null) || (name.Length <= 255)) &&
                ((type == null) || (type.Length <= 255)) &&
                ((description == null) || (description.Length <= 2048)) &&
                ((moniker == null) || (moniker.Length <= 2048));
        }
    }
}