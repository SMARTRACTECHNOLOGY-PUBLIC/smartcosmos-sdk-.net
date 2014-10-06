#region License
// SMART COSMOS .Net SDK
// (C) Copyright 2014 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
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
    [JsonConverter(typeof(DictionaryConverter))]
    public class TagProperties : Dictionary<string, object>
    {

    }

    /*
    [DataContract]
    public class TagProperties
    {
        /*
        /// <summary>
        /// Customer ID
        /// </summary>
        [DataMember]
        public string custId { get; set; }
        /// <summary>
        /// Order  ID
        /// </summary>
        [DataMember]
        public string orderId { get; set; }
        /// <summary>
        /// Order date
        /// </summary>
        [DataMember]
        public long? orderDate { get; set; }
        /// <summary>
        /// Order quantity
        /// </summary>
        [DataMember]
        public int? orderQty { get; set; }
        /// <summary>
        /// Order quantity unit
        /// </summary>
        [DataMember]
        public string orderQtyU { get; set; }
        /// <summary>
        /// Customer purchase order number
        /// </summary>
        [DataMember]
        public string customerPO { get; set; }
        /// <summary>
        /// Customer purchase order number
        /// </summary>
        [DataMember]
        public string customerPO { get; set; }

        [DataMember]
        public string plantId { get; set; }

        [DataMember]
        public string batchId { get; set; }
        [DataMember]
        public long? delivDate { get; set; }

        public TagProperties() : base()
        {
            this.custId = null;
            this.orderId = null;
            this.orderDate = null;
            this.orderQty = null;
            this.orderQtyU = null;
            this.customerPO = null;
            this.custId = null;
            this.custId = null;
            this.custId = null;
        }
    }

         */

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

        public TagRecord()
            : base()
        {
            this.props = new TagProperties();
        }
    }
}
