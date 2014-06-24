using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagVerification
{
    [DataContract]
    public class VerificationMessageRequest
    {
        [DataMember]
        public string verificationType { get; set; }
        [DataMember]
        public int verificationState { get; set; }
    }
}
