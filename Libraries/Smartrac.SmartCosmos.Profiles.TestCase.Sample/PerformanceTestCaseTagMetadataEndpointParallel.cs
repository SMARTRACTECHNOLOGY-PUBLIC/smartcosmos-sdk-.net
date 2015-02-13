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

using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.TestCase.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Smartrac.SmartCosmos.Profiles.TestCase.Sample
{
    [TestCaseAttribute(TestCaseType.Performance, 100)]
    public class PerformanceTestCaseTagMetadataEndpointParallel : BaseTestCaseTagMetadataEndpoint
    {
        protected override bool ExecuteTests()
        {
            return TestTagMetadata();
        }

        protected virtual bool TestTagMetadata()
        {
            bool result = true;

            foreach (string file in dataContext.GetTagDataFile())
            {
                if (File.Exists(file))
                {
                    OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata - PerformanceTest Parallel");
                    TagMetaDataActionResult actionResult = TagMetaDataActionResult.Failed;
                    try
                    {
                        List<TagMetaDataRequest> requestList = new List<TagMetaDataRequest>();
                        TagMetaDataRequest requestTagMetaData = null;
                        int tagCount = 0;

                        // load data
                        XDocument doc;
                        doc = XDocument.Load(file);
                        foreach (var tag in doc.Descendants("tag"))
                        {
                            var tagId = tag.Attribute("id");
                            if (null == tagId)
                                continue;

                            if ((null == requestTagMetaData) || (requestTagMetaData.tagIds.Count == 1000))
                            {
                                requestTagMetaData = new TagMetaDataRequest(dataContext);
                                requestTagMetaData.tagIds.Clear();
                                requestList.Add(requestTagMetaData);
                            }

                            tagCount++;
                            requestTagMetaData.tagIds.Add(tagId.Value);
                        }

                        Parallel.ForEach(requestList, request =>
                        {
                            TagMetaDataResponse responseTagMetaData;
                            Stopwatch watch = new Stopwatch();
                            watch.Start();

                            actionResult = endpoint.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
                            result = result && (actionResult == TagMetaDataActionResult.Successful);
                            watch.Stop();
                            Logger.AddLog(request.tagIds.Count + " tags checked. Result:" + result + "  Required time:" + watch.Elapsed);
                            request.tagIds.Clear();
                            watch.Reset();
                        }
                        );

                        Logger.AddLog("Test count: " + tagCount);
                    }
                    catch (Exception e)
                    {
                        Logger.AddLog(e.Message, LogType.Error);
                    }

                    OnAfterTest(actionResult);
                }
            }
            return result;
        }
    }
}