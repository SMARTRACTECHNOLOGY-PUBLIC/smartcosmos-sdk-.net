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

namespace Smartrac.SmartCosmos.Flows.AccountManagement
{
    public class AccountDetailsResponse : BaseResponse
    {
        private Urn urn_;

        public string urn
        {
            get
            {
                return (urn_ != null) ? urn_.UUID : "";
            }
            set
            {
                urn_ = new Urn(value);
            }
        }

        public long lastModifiedTimestamp { get; set; }

        public string email { get; set; }

        public string fullName { get; set; }

        public bool activeFlag { get; set; }

        public AccountProfile profile { get; set; }

        [JsonIgnore]
        public Urn Urn
        {
            get
            {
                return urn_;
            }
        }
    }

    public class AccountAction
    {
        public string name { get; set; }
        public bool enabled { get; set; }
    }

    public class AccountModule
    {
        public string name { get; set; }
        public List<AccountAction> actions { get; set; }
    }

    public class AccountProfile
    {
        public string name { get; set; }
        public List<AccountModule> modules { get; set; }
    }
}