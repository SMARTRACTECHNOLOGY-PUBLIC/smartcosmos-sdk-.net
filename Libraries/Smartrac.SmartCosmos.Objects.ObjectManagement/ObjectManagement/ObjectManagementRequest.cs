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

using System;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectManagement
{
    public class ObjectManagementRequest : BaseRequest
    {
        public string objectUrn
        {
            get
            {
                return (urn != null) ? urn.UUID : "";
            }
            set
            {
                urn = new Urn(value);
            }
        }

        public Urn urn { get; set; }

        public string type { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public bool? activeFlag { get; set; }

        public string moniker { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() &&
                urn.IsValid() &&
                (String.IsNullOrEmpty(type) || type.Length <= 255) &&
                (String.IsNullOrEmpty(name) || name.Length <= 255) &&
                (String.IsNullOrEmpty(description) || description.Length <= 255) &&
                (String.IsNullOrEmpty(moniker) || moniker.Length <= 2048);
        }
    }

    public class ObjectManagementNewRequest : ObjectManagementRequest
    {
        public override bool IsValid()
        {
            return base.IsValid() &&
                !String.IsNullOrEmpty(type) &&
                !String.IsNullOrEmpty(name);
        }
    }
}