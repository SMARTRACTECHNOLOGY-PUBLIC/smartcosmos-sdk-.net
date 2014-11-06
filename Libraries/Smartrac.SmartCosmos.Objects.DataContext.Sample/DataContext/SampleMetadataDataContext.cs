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

using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.Metadata;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    public class SampleMetadataDataContext : BaseMetadataDataContext
    {
        /// <summary>
        /// Case-sensitive urn of an existing entity of type entityReferenceType
        /// </summary>
        public override Urn GetEntityUrn()
        {
            return MyDataContext.GetSampleObjectUrn();
        }

        public override ViewType GetViewType()
        {
            return ViewType.Standard;
        }

        /// <summary>
        /// Valid EntityReferenceType enum value
        /// </summary>
        public override EntityReferenceType GetEntityReferenceType()
        {
            return EntityReferenceType.Object;
        }

        public override List<MetadataItem> GetMetadata()
        {
            List<MetadataItem> list = new List<MetadataItem>();
            list.Add(new MetadataItem
            {
                dataTypeObj = MetadataDataType.Boolean,
                key = "bar",
                rawValue = "AQ=="
            });
            list.Add(new MetadataItem
            {
                dataTypeObj = MetadataDataType.String,
                key = "foo",
                rawValue = "KioqKioqKioqKioqKg=="
            });
            return list;
        }
    }
}