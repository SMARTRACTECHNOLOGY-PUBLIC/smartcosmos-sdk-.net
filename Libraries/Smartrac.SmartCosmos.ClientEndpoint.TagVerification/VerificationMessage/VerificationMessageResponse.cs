﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;

namespace Smartrac.SmartCosmos.ClientEndpoint.TagVerification
{
    [DataContract]
    public class VerificationMessageResponse
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public string message { get; set; }
    }
}