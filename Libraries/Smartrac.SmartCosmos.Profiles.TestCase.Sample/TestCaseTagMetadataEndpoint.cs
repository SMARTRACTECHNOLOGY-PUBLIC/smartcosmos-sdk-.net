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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.TestCase.Base;
using System;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace Smartrac.SmartCosmos.Profiles.TestCase.Sample
{
    [TestCaseAttribute(20)]
    public class TestCaseTagMetadataEndpoint : BaseTestCaseTagMetadataEndpoint
    {

        protected override bool ExecuteTests()
        {
            return TestGetTagMessage() && 
                TestGetTagMetadata(new TagMetaDataRequest(dataContext)) && 
                TestGetTagMetadata(new TagMetaDataRequest(dataContext, true));
        }

        protected virtual bool TestGetTagMessage()
        {
            OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMessage");
            // create request
            TagMessageRequest requestData = new TagMessageRequest();
            requestData .tagCode = dataContext.GetTagMessage();
            requestData.tagCode = 0;
            TagMessageResponse responseData;
            TagMetaDataActionResult actionResult = endpoint.GetTagMessage(requestData, out responseData);
            OnAfterTest(actionResult);
            return (actionResult == TagMetaDataActionResult.Successful);
        }
        
        protected virtual bool TestGetTagMetadata(TagMetaDataRequest requestTagMetaData)
        {
            TagMetaDataActionResult actionResult = TagMetaDataActionResult.Failed;
            OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata");
            try
            {
                TagMetaDataResponse responseTagMetaData;

                Stopwatch watch = new Stopwatch();
                int tagCount = requestTagMetaData.tagIds.Count;
                // validate
                watch.Start();
                actionResult = endpoint.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                watch.Stop();
                requestTagMetaData.tagIds.Clear();
                watch.Reset();

                if (responseTagMetaData != null)
                {
                    string batchId;
                    string plantId;
                    long delivDate;
                    foreach (var tag in responseTagMetaData.result)
                    {
                        if (tag.props != null)
                        {
                            tag.props.GetValue(TagPropertyString.batchId, out batchId);
                            tag.props.GetValue(TagPropertyString.plantId, out plantId);
                            tag.props.GetValue(TagPropertyLong.deliveryDate, out delivDate);

                            Logger.AddLog("Sample data for tag: batch=" + batchId + ", plantId=" + plantId + ", deliveryDate=" + String.Format("{0:dd/MM/yyyy}", DateTimeExtensions.FromUnixTimestamp(delivDate)));
                        }
                    }
                }
                OnAfterTest(actionResult);
            }
            catch (Exception e)
            {
                Logger.AddLog(e.Message, LogType.Error);
                return false;
            }

            return (actionResult == TagMetaDataActionResult.Successful);
        }
    }
}