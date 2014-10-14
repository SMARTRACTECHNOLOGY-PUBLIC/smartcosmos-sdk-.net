﻿#region License

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
using System.Collections.Generic;
using System.Runtime.Serialization;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.Metadata
{
    public class CreateMetadataRequest : BaseRequest
    {
        /// <summary>
        /// Valid EntityReferenceType enum value
        /// </summary>
        public EntityReferenceType entityReferenceType { get; set; }

        /// <summary>
        /// Case-sensitive urn of an existing entity of type entityReferenceType
        /// </summary>
        public Urn entityUrn { get; set; }

        private List<MetadataItem> MetaDataList_ = new List<MetadataItem>();

        /// <summary>
        /// list of Meta data values
        /// </summary>
        public List<MetadataItem> MetaDataList { get { return MetaDataList_; } }

        public CreateMetadataRequest()
            : base()
        {
            MetaDataList_ = new List<MetadataItem>();
        }

        public override bool IsValid()
        {
            return base.IsValid() &&
                MetaDataList.Count > 0 &&
                MetaDataList.TrueForAll(i => i.IsValid());
        }
    }

    [DataContract]
    public class MetadataItem : BaseRequest
    {
        [DataMember]
        public string dataType
        {
            get
            {
                return dataTypeObj.GetDescription();
            }

            set
            {
                dataTypeObj = MetadataDataType.Boolean.GetFlagByDescription(value);
            }
        }

        public MetadataDataType dataTypeObj { get; set; }

        /// <summary>
        /// Case-sensitive key used to index the metadata raw value
        /// </summary>
        [DataMember]
        public string key { get; set; }

        [DataMember]
        public string rawValue { get; set; }

        public override bool IsValid()
        {
            return base.IsValid() &&
                !String.IsNullOrEmpty(key);
        }
    }
}