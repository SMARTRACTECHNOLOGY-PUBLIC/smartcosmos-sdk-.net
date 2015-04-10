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
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.HashTag
{
    public class HashTagDataResponse : BaseResponse
    {
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
        /// Serialization Level: Standard
        /// </summary>
        public string description { get; set; }

        public string name { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public string moniker { get; set; }

        /// <summary>
        /// Serialization Level: Standard
        /// </summary>
        public bool? activeFlag { get; set; }

        /// <summary>
        /// Serialization Level: Standard
        /// </summary>
        public long? lastModifiedTimestamp { get; set; }

        /// <summary>
        /// Serialization Level: Full
        /// </summary>
        public DataIAccount account { get; set; }

        public HashTagDataResponse()
            : base()
        {
            description = null;
            activeFlag = null;
            moniker = null;
            activeFlag = null;
            lastModifiedTimestamp = null;
            account = null;
        }
    }
}