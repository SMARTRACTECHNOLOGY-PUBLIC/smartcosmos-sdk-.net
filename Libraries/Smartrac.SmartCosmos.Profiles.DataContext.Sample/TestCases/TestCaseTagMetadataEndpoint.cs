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
using System.Diagnostics;
using System.IO;
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Profiles.DataContext.Sample
{
    [TestCaseAttribute(20)]
    public class TestCaseTagMetadataEndpoint : BaseProfilesTestCase
    {
        protected override bool DoRun()
        {
            ITagDataContext dataContext = DataContextFactory.CreateTagDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip test cases for TagMetadataEndpoint, because of missing data context", LogType.Info);
                return true;
            }

            ITagMetadataEndpoint tester = EndpointFactory.CreateTagMetadataEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateTagMetadataEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            if (File.Exists(dataContext.GetTagDataFile()))
            {
                OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata");
                try
                {
                    TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(dataContext);
                    TagMetaDataResponse responseTagMetaData;
                    TagMetaDataActionResult actionResult = TagMetaDataActionResult.Failed;
                    Stopwatch watch = new Stopwatch();
                    int tagCount = requestTagMetaData.tagIds.Count;

                    // validate
                    watch.Start();
                    actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                    result = result && (actionResult == TagMetaDataActionResult.Successful);
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
                    OnAfterTest(actionResult, responseTagMetaData);
                }
                catch (Exception e)
                {
                    Logger.AddLog(e.Message, LogType.Error);
                }
            }

            return result;
        }
    }
}