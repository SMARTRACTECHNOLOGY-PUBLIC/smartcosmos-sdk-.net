using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagMetadata
{
    [DataContract]
    public class TagMessageRequest
    {
        [DataMember]
        public int tagCode { get; set; }
    }
}
