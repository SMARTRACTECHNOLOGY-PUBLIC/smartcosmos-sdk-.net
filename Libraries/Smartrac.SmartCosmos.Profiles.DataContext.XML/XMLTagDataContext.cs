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