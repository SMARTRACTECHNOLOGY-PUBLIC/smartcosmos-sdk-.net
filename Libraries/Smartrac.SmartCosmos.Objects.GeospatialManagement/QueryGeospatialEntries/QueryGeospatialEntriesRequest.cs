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

namespace Smartrac.SmartCosmos.Objects.GeospatialManagement
{
    [DataContract]
    public class QueryGeospatialEntriesRequest : BaseRequest
    {
        /// <summary>
        /// Optional: A case-sensitive starts with string pattern to match against. If omitted,
        /// </summary>
        [DataMember]
        public string nameLike { get; set; }

        /// <summary>
        /// Optional: A valid JSON Serialization View name (case-sensitive)
        /// </summary>
        [DataMember]
        public ViewType viewType { get; set; }

        public QueryGeospatialEntriesRequest()
            : base()
        {
            nameLike = null;
        }
    }
}