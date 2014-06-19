using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Smartrac.SmartCosmos.ClientEndpoint.DataImport
{
    [DataContract]
    public class ImportStateResponse
    {
        [DataMember]
        public int code { get; set; }
        [DataMember]
        public int state { get; set; }
        [DataMember]
        public int progress { get; set; }
        [DataMember]
        public string message { get; set; }
    }

    [DataContract]
    public class ImportStateRequest
    {
        [DataMember]
        public string importId { get; set; }

        public ImportStateRequest(string aImportId)
        {
            this.importId = aImportId;
        }
    }
}
