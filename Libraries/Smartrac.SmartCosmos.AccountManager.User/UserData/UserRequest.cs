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

namespace Smartrac.SmartCosmos.AccountManager.User
{
    public class UserRequest : BaseRequest
    {
        public string emailAddress
        {
            get
            {
                return (EmailAddress != null) ? EmailAddress.Mail : "";
            }
            set
            {
                EmailAddress = new Email(value);
            }
        }

        public string surname { get; set; }

        [JsonIgnore]
        public Email EmailAddress { get; set; }

        public string givenName { get; set; }

        public bool isValid()
        {
            if (EmailAddress.IsValid())
            {
                return true;
            }
            return false;
        }
    }
}