using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagMetadata
{
    [DataContract]
    public class TagMessageResponse
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}
