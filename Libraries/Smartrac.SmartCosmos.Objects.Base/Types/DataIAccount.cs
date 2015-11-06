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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using System;

namespace Smartrac.SmartCosmos.Objects.Base
{
    /// <summary>
    /// TODO: implement data type
    /// </summary>
    public class DataIAccount
    {
        public bool activeFlag { get; set; }

        public string description { get; set; }

        public long lastModifiedTimestamp { get; set; }

        public string name { get; set; }

        public string moniker { get; set; }

        public bool ShouldSerializemoniker()
        {
            return !String.IsNullOrEmpty(moniker);
        }

        public string urn
        {
            get
            {
                return (accountUrnObj != null) ? accountUrnObj.UUID : "";
            }
            set
            {
                accountUrnObj = new Urn(value);
            }
        }

        [JsonIgnore]
        public Urn accountUrnObj { get; set; }
    }
}