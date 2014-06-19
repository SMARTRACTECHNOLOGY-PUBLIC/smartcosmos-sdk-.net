using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
    }
}
