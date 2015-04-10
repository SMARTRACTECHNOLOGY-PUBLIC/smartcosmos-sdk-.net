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
using Smartrac.SmartCosmos.Profiles.DataContext;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
{
    public class TagMetaDataRequest : BaseRequest
    {
        public List<string> tagIds { get; set; }

        public List<string> verificationTypes { get; set; }

        public List<string> properties { get; set; }

        //elective parameter
        public List<string> materialPerformance { get; set; }

        public TagMetaDataRequest()
        {
            this.tagIds = new List<string>();
            this.verificationTypes = new List<string>();
            this.properties = new List<string>();
            this.materialPerformance = new List<string>();
        }

        public TagMetaDataRequest(ITagDataContext dataContext, bool addMaterialPerformance = false)
            : this()
        {
            this.tagIds.AddRange(dataContext.GetTagIds());
            this.verificationTypes.AddRange(dataContext.GetVerificationTypes());
            this.properties.AddRange(dataContext.GetTagProperties());
            if (addMaterialPerformance)
            {
                this.materialPerformance.AddRange(dataContext.GetMaterialPerformance());
            }
        }
    }
}