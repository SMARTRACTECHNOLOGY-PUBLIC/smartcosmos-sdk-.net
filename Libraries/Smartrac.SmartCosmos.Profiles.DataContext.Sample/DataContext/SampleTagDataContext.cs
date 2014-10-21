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

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Smartrac.SmartCosmos.Profiles.DataContext.Sample
{
    public class SampleTagDataContext : BaseTagDataContext
    {
        private List<string> TagIds;
        private List<string> TagProperties;
        private List<string> VerificationTypes;

        private string SampleDataFile;

        public SampleTagDataContext()
        {
            TagIds = new List<string>();
            TagIds.Add("0EEEE100000001");     // existing dummy TID
            TagIds.Add("0EEEE200000002");     // existing dummy TID
            //   TagIds.Add("E280110C20005042D5B602B1999");
            //   TagIds.Add("E280110C2000505110850282");
            //   TagIds.Add("E280110C20005103115E0282");
            TagIds.Add("0EEEE100000781");
            //   TagIds.Add("E12345678912345777");

            TagProperties = new List<string>();
            TagProperties.Add("plantId");     // Manufacturer production side ID
            TagProperties.Add("batchId");     // Roll number / batch ID
            TagProperties.Add("delivDate");   // Delivery date
            TagProperties.Add("delivQty");    // Delivery quantity

            VerificationTypes = new List<string>();
            VerificationTypes.Add("RR");

            SampleDataFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"..\..\..\..\..\Documentation\SampleData\SampleData30k.xml");
            if (!File.Exists(SampleDataFile))
                SampleDataFile = "";
        }

        public override IEnumerable<string> GetTagIds()
        {
            return TagIds.AsEnumerable<string>();
        }

        public override IEnumerable<string> GetTagProperties()
        {
            return TagProperties.AsEnumerable<string>();
        }

        public override IEnumerable<string> GetVerificationTypes()
        {
            return VerificationTypes.AsEnumerable<string>();
        }

        public override string GetImportId()
        {
            return "20140702_022852-85";
        }

        public override string GetTagDataFile()
        {
            return SampleDataFile;
        }
    }
}