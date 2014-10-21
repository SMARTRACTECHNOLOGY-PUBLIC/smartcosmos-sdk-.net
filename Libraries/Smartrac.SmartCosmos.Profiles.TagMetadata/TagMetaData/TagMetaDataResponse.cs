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

#endregion License

using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
{
    [DataContract]
    public class TagMetaDataResponse : BaseResponse
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
    public class TagProperties : Dictionary<string, object>
    {
        public void SetValue(TagPropertyString key, string value)
        {
            this.SetValue(key.GetDescription(), value);
        }

        public void SetValue(string key, string value)
        {
            if (this.ContainsKey(key))
                this[key] = value;
            else
                this.Add(key, value);
        }

        public void SetValue(TagPropertyLong key, long value)
        {
            this.SetValue(key.GetDescription(), value);
        }

        public void SetValue(string key, long value)
        {
            if (this.ContainsKey(key))
                this[key] = value;
            else
                this.Add(key, value);
        }

        public void SetValue(TagPropertyNumber key, double value)
        {
            this.SetValue(key.GetDescription(), value);
        }
        
        public void SetValue(string key, double value)
        {
            if (this.ContainsKey(key))
                this[key] = value;
            else
                this.Add(key, value);
        }

        public bool GetValue(TagPropertyString key, out string value)
        {
            return GetValue(key.GetDescription(), out value);
        }

        public bool GetValue(string key, out string value)
        {
            if (this.ContainsKey(key))
            {
                value = (string)this[key];
                return true;
            }
            else
            {
                value = "";
                return false;
            }
        }

        public bool GetValue(TagPropertyLong key, out long value)
        {
            return GetValue(key.GetDescription(), out value);
        }

        public bool GetValue(string key, out long value)
        {
            if (this.ContainsKey(key))
            {
                value = (long)this[key];
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }

        public bool GetValue(TagPropertyNumber key, out double value)
        {
            return GetValue(key.GetDescription(), out value);
        }

        public bool GetValue(string key, out double value)
        {
            if (this.ContainsKey(key))
            {
                value = (float)this[key];
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }
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
        //public Dictionary<string, object> props { get; set; }

        /*
        public TagRecord()
            : base()
        {
            this.props = new TagProperties();
        }
        */
    }
}