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
using System.Xml.Linq;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Profiles.DataContext.Sample
{
    [TestCaseAttribute(TestCaseType.Performance)]
    public class PerformanceTestCaseTagMetadataEndpoint : BaseTestCase
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
                OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata - PerformanceTest");
                try
                {
                    TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(dataContext);
                    requestTagMetaData.tagIds.Clear();
                    TagMetaDataResponse responseTagMetaData;
                    Stopwatch watch = new Stopwatch();
                    int tagCount = 0;
                    TagMetaDataActionResult actionResult;

                    // load data
                    XDocument doc;
                    doc = XDocument.Load(dataContext.GetTagDataFile());
                    foreach (var tag in doc.Descendants("tag"))
                    {
                        var tagId = tag.Attribute("id");
                        if (null == tagId)
                            continue;

                        requestTagMetaData.tagIds.Add(tagId.Value);

                        // validate
                        if (requestTagMetaData.tagIds.Count == 1000)
                        {
                            tagCount += requestTagMetaData.tagIds.Count;
                            watch.Start();
                            actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                            result = result && (actionResult == TagMetaDataActionResult.Successful);
                            watch.Stop();
                            Logger.AddLog(requestTagMetaData.tagIds.Count + " tags checked. Result:" + actionResult + "  Required time:" + watch.Elapsed);
                            requestTagMetaData.tagIds.Clear();
                            watch.Reset();
                        }
                    }

                    // validate rest
                    if (requestTagMetaData.tagIds.Count > 0)
                    {
                        tagCount += requestTagMetaData.tagIds.Count;
                        watch.Start();
                        actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                        result = result && (actionResult == TagMetaDataActionResult.Successful);
                        watch.Stop();
                        Logger.AddLog(requestTagMetaData.tagIds.Count + " tags checked. Result:" + result + "  Required time:" + watch.Elapsed);
                        requestTagMetaData.tagIds.Clear();
                        watch.Reset();
                    }

                    Logger.AddLog("Test count: " + tagCount);
                }
                catch (Exception e)
                {
                    Logger.AddLog(e.Message, LogType.Error);
                }

                OnAfterTest();
            }

            return result;
        }
    }
}