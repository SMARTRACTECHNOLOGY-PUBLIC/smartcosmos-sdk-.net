#region License

// SMART COSMOS .Net SDK
// (C) Copyright 2013 - 2015 SMARTRAC TECHNOLOGY GmbH, (http://www.smartrac-group.com)
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

using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using System;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.Profiles.TagMetadata
{
    public class TagMetaDataResponse : BaseResponse
    {
        public int code { get; set; }

        public List<TagRecord> result { get; set; }
    }

    public class VerificationState
    {
        public int RR { get; set; }

        public int TestLicense { get; set; }
    }

    public class TagProperties : Dictionary<string, object>
    {
        public void SetValue(TagPropertyString key, string value)
        {
            this.SetValue(key.GetDescription(), value);
        }

        public void SetValue(string key, string value)
        {
            if (this.ContainsKey(key) && (this[key] != null))
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
            if (this.ContainsKey(key) && (this[key] != null))
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
            if (this.ContainsKey(key) && (this[key] != null))
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
            if (this.ContainsKey(key) && (this[key] != null))
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
            if (this.ContainsKey(key) && (this[key] != null))
            {
                if (this[key] is string)
                    return long.TryParse((string)this[key], out value);
                else
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
            if (this.ContainsKey(key) && (this[key] != null))
            {
                if (this[key] is string)
                    return double.TryParse((string)this[key], out value);
                else
                    value = (double)this[key];
                return true;
            }
            else
            {
                value = 0;
                return false;
            }
        }
    }

    public class TagRecord
    {
        public string tagId { get; set; }

        public int tagCode { get; set; }

        public VerificationState verificationState { get; set; }

        public TagProperties props { get; set; }

        public MaterialPerformanceList materialPerformance { get; set; }

        //public Dictionary<string, object> props { get; set; }

        /*
        public TagRecord()
            : base()
        {
            this.props = new TagProperties();
        }
        */
    }

    public class MaterialPerformanceList{

        public int rrAvg { get; set; }

        public int rrAvgStdDev { get; set; }

        public int rrMean { get; set; }

        public int rrMeanStdDevPos { get; set; }

        public int rrMeanStdDevNeg { get; set; }

        public String grading { get; set; }

    }

    

}