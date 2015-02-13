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

using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Smartrac.SmartCosmos.Profiles.DataContext.XML
{
    [Serializable]
    [XmlRoot("TagDataContext")]
    public class XMLTagDataContext : BaseTagDataContext
    {
        [XmlArray("TagIds")]
        [XmlArrayItem(ElementName = "Tag")]
        public List<string> TagIds { get; set; }

        [XmlArray("TagProperties")]
        [XmlArrayItem(ElementName = "TagProperty")]
        public List<string> TagProperties { get; set; }

        [XmlArray("VerificationTypes")]
        [XmlArrayItem(ElementName = "VerificationType")]
        public List<string> VerificationTypes { get; set; }

        [XmlArray("ImportIds")]
        [XmlArrayItem(ElementName = "ImportId")]
        public List<string> ImportIds { get; set; }

        [XmlArray("TagDataFiles")]
        [XmlArrayItem(ElementName = "TagDataFile")]
        public List<string> TagDataFiles { get; set; }

        public XMLTagDataContext()
            : base()
        {
            TagIds = new List<string>();
            TagProperties = new List<string>();
            VerificationTypes = new List<string>();
            ImportIds = new List<string>();
            TagDataFiles = new List<string>();
        }

        public override IEnumerable<string> GetTagIds()
        {
            return TagIds;
        }

        public override IEnumerable<string> GetTagProperties()
        {
            return TagProperties;
        }

        public override IEnumerable<string> GetVerificationTypes()
        {
            return VerificationTypes;
        }

        public override IEnumerable<string> GetImportId()
        {
            return ImportIds;
        }

        public override IEnumerable<string> GetTagDataFile()
        {
            return TagDataFiles;
        }
    }
}