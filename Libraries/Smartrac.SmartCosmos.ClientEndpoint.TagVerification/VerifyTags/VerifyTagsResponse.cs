using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagVerification
{
    [DataContract]
    public class VerifyTagsResponse
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public List<TagProperties> result { get; set; }
    }

    [DataContract]
    public class TagProperties
    {
        [DataMember]
        public string tagId { get; set; }
        [DataMember]
        public int tagCode { get; set; }
        [DataMember]
        public int state { get; set; }
    }
}
