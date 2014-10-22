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
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.Base;
using System;

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
            TagIds.Add("E280113020003054990802EB");
            //   TagIds.Add("E12345678912345777");

            TagProperties = new List<string>();
            TagProperties.Add(TagPropertyString.customerId.GetDescription());     
            TagProperties.Add(TagPropertyString.orderId.GetDescription());     
            TagProperties.Add(TagPropertyLong.orderDate.GetDescription());   
            TagProperties.Add(TagPropertyNumber.orderQuantity.GetDescription());
            TagProperties.Add(TagPropertyString.orderQuantityUnit.GetDescription());
            TagProperties.Add(TagPropertyString.customerPurchaseOrder.GetDescription());
            TagProperties.Add(TagPropertyString.supplierPurchaseOrder.GetDescription());
            TagProperties.Add(TagPropertyString.deliveryId.GetDescription());
            TagProperties.Add(TagPropertyLong.deliveryDate.GetDescription());   
            TagProperties.Add(TagPropertyNumber.deliveryQuantity.GetDescription());
            TagProperties.Add(TagPropertyString.deliveryQuantityUnit.GetDescription());   
            TagProperties.Add(TagPropertyNumber.yield.GetDescription());
            TagProperties.Add(TagPropertyString.batchId.GetDescription());
            TagProperties.Add(TagPropertyString.subRoll.GetDescription());
            TagProperties.Add(TagPropertyString.plantId.GetDescription());
            TagProperties.Add(TagPropertyLong.inlayManufacturerDate.GetDescription());
            TagProperties.Add(TagPropertyString.inlayType.GetDescription());   
            TagProperties.Add(TagPropertyNumber.attenuation.GetDescription());   
            TagProperties.Add(TagPropertyNumber.checkState.GetDescription());

            VerificationTypes = new List<string>();
            VerificationTypes.Add("RR");

            SampleDataFile = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), @"..\..\..\..\..\Documentation\SampleData\SampleData30k.xml");
            if (!File.Exists(SampleDataFile))
                SampleDataFile = "";
        }

        public override IEnumerable<string> GetTagIds()
        {
            return TagIds; //.AsEnumerable<string>();
        }

        public override IEnumerable<string> GetTagProperties()
        {
            return TagProperties; //.AsEnumerable<string>();
        }

        public override IEnumerable<string> GetVerificationTypes()
        {
            return VerificationTypes;
            //return VerificationTypes.AsEnumerable<string>();
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