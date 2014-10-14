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
using System.Runtime.Serialization;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectManagement
{
    [DataContract]
    public class ObjectManagementRequest : BaseRequest
    {
        [DataMember(IsRequired = true)]
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

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public bool? activeFlag { get; set; }

        [DataMember]
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

    [DataContract]
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