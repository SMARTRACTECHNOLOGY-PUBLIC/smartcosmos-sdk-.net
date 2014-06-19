using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagMetadata
{
    [DataContract]
    public class TagMetaDataResponse
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public List<TagRecord> result { get; set; }
    }

    [DataContract]
    public class VerificationState
    {
        [DataMember]
        public int RR { get; set; }
        [DataMember]
        public int TestLicense { get; set; }
    }

    [DataContract]
    public class TagProperties
    {
        [DataMember]
        public string plantId { get; set; }
        [DataMember]
        public string customerPO { get; set; }
        [DataMember]
        public string batchId { get; set; }
        [DataMember]
        public object delivDate { get; set; }
    }

    [DataContract]
    public class TagRecord
    {
        [DataMember]
        public string tagId { get; set; }
        [DataMember]
        public int tagCode { get; set; }
        [DataMember]
        public VerificationState verificationState { get; set; }
        [DataMember]
        public TagProperties props { get; set; }
    }


}
