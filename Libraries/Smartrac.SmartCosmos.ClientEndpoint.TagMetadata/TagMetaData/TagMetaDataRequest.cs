#region License
// SMART COSMOS Profiles SDK
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
#endregion

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Smartrac.SmartCosmos.DataContext;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagMetadata
{
    [DataContract]
    public class TagMetaDataRequest
    {
        [DataMember]
        public List<string> tagIds { get; set; }
        [DataMember]
        public List<string> verificationTypes { get; set; }
        [DataMember]
        public List<string> properties { get; set; }

        public TagMetaDataRequest()
        {
            this.tagIds = new List<string>();
            this.verificationTypes = new List<string>();
            this.properties = new List<string>();
        }

        public TagMetaDataRequest(IDataContext dataContext)
            : this()
        {
            this.tagIds.AddRange(dataContext.GetTagIds());
            this.verificationTypes.AddRange(dataContext.GetVerificationTypes());
            this.properties.AddRange(dataContext.GetTagProperties());
        }
    }
}
