using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagVerification
{
    [DataContract]
    public class VerifyTagsRequest
    {
        [DataMember]
        public List<string> tagIds { get; set; }
        [DataMember]
        public string verificationType { get; set; }

        public VerifyTagsRequest()
        {
            this.tagIds = new List<string>();
        }
    }
}
