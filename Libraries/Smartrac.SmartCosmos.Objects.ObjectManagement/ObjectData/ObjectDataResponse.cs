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

using System.Runtime.Serialization;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.ObjectManagement
{
    [DataContract]
    public class ObjectDataResponse : BaseResponse
    {
        [DataMember]
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

        public Urn urnObj { get; set; }

        [DataMember]
        public string objectUrn
        {
            get
            {
                return (objectUrnObj != null) ? objectUrnObj.UUID : "";
            }
            set
            {
                objectUrnObj = new Urn(value);
            }
        }

        public Urn objectUrnObj { get; set; }

        [DataMember]
        public string type { get; set; }

        [DataMember]
        public string name { get; set; }

        [DataMember]
        public string description { get; set; }

        [DataMember]
        public bool activeFlag { get; set; }

        [DataMember]
        public long lastModifiedTimestamp { get; set; }
    }
}