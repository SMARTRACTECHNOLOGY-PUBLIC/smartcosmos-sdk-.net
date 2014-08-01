#region License
// SMART COSMOS Profiles SDK
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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Smartrac.Logging;
using Smartrac.SmartCosmos.TestSuite;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.ClientEndpoint.DataImport;
using Smartrac.SmartCosmos.ClientEndpoint.PlatformAvailability;
using Smartrac.SmartCosmos.ClientEndpoint.TagMetadata;
using Smartrac.SmartCosmos.ClientEndpoint.TagVerification;

namespace Smartrac.SmartCosmos.TestSuite.Sample
{
    /// <summary>
    /// Test suite for SmartCosmos
    /// </summary>
    public class SampleTestSuite : BaseTestSuite, ISampleTestSuite
    {
        public bool RunPerformanceTests { get; set; }
        private Stopwatch stopwatch = new Stopwatch();

        public SampleTestSuite()
        {
            RunPerformanceTests = true;
        }

        public override bool Run()
        {
            bool result = true;

            // SAMPLE 1 - Test cases for platform availability endpoint
            result = TestCase_PlatformAvailabilityEndpoint() && result;

            // SAMPLE 2 - Test cases for tag metadata endpoint
            result = TestCase_TagMetadataEndpoint() && result;

            // SAMPLE 3 - Test cases for data import endpoint
            result = TestCase_DataImportEndpoint() && result;

            // SAMPLE 4 - Test cases for tag verification endpoint
            result = TestCase_TagVerificationEndpoint() && result;

            // SAMPLE 5 - Performance test for tag metadata endpoint
            if (RunPerformanceTests)
                result = PerformanceTestCase_TagMetadataEndpoint() && result;

            // SAMPLE 6 - Performance test for tag metadata endpoint (Parallel)
            if (RunPerformanceTests)
                result = PerformanceTestCase_TagMetadataEndpointParallel() && result;

            Logger.AddLog("");
            Logger.AddLog("Total result: " + result);
            return result;
        }

        /// <summary>
        /// Test cases for verification endpoint
        /// </summary>
        public bool TestCase_TagVerificationEndpoint()
        {
            bool result = true;

            OnBeforeTest("TagVerificationEndpoint", "VerifyTags");
            // create client for endpoint
            ITagVerificationEndpoint tester = Factory.CreateTagVerificationEndpoint();
            // create request
            VerifyTagsRequest requestVerifyTags = new VerifyTagsRequest(DataContext);
            // call endpoint
            VerifyTagsResponse responseVerifyTags;
            TagVerificationActionResult actionResult = tester.VerifyTags(requestVerifyTags, out responseVerifyTags);
            result = (actionResult == TagVerificationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseVerifyTags.ToJSON());
            OnAfterTest();

            OnBeforeTest("TagVerificationEndpoint", "GetVerificationMessage");
            // create request
            VerificationMessageRequest requestVerificationMessage = new VerificationMessageRequest();
            requestVerificationMessage.verificationState = 0;
            requestVerificationMessage.verificationType = DataContext.GetVerificationTypes().First<string>();
            VerificationMessageResponse responseVerificationMessage;
            // call endpoint
            actionResult = tester.GetVerificationMessage(requestVerificationMessage, out responseVerificationMessage);
            result = result && (actionResult == TagVerificationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseVerificationMessage.ToJSON());
            OnAfterTest();

            return result;
        }

        public bool TestCase_DataImportEndpoint()
        {
            bool result = true;
            DataActionResult actionResult;

            OnBeforeTest("DataImportEndpoint", "CheckImportState");
            // create client for endpoint
            IDataImportEndpoint tester = Factory.CreateDataImportEndpoint();

            ImportStateResponse responseImportState;
            // call endpoint
            actionResult = tester.CheckImportState(new ImportStateRequest(DataContext.GetImportId()), out responseImportState);
            result = result && (actionResult == DataActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseImportState.ToJSON());
            OnAfterTest();

            return result;
        }

        public bool TestCase_TagMetadataEndpoint()
        {
            bool result = true;
            TagMetaDataActionResult actionResult;

            OnBeforeTest("TagMetadataEndpoint", "GetTagMetadata");
            // create client for endpoint
            ITagMetadataEndpoint tester = Factory.CreateTagMetadataEndpoint();
            // create request
            TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(DataContext);

            TagMetaDataResponse responseTagMetaData;
            // call endpoint
            actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
            result = result && (actionResult == TagMetaDataActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseTagMetaData.ToJSON());
            OnAfterTest();

            OnBeforeTest("TagMetadataEndpoint", "GetTagMessage");
            // create request
            TagMessageRequest requestTagMessage = new TagMessageRequest();
            requestTagMessage.tagCode = 0;
            TagMessageResponse responseTagMessage;
            // call endpoint
            actionResult = tester.GetTagMessage(requestTagMessage, out responseTagMessage);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseTagMessage.ToJSON());
            OnAfterTest();

            return result;
        }

        public bool TestCase_PlatformAvailabilityEndpoint()
        {
            bool result = true;
            PlatformAvailabilityActionResult actionResult;

            OnBeforeTest("PlatformAvailabilityEndpoint", "Ping");
            // create client for endpoint
            IPlatformAvailabilityEndpoint tester = Factory.CreatePlatformAvailabilityEndpoint();
            // call endpoint & send response to console
            actionResult = tester.Ping();
            result = result && (actionResult == PlatformAvailabilityActionResult.Successful);

            Logger.AddLog("Result: " + actionResult);
            OnAfterTest();

            return result;
        }

        private void OnBeforeTest(string endpoint, string function)
        {
            Logger.AddLog("-----------------------");
            Logger.AddLog("Endpoint: " + endpoint);
            Logger.AddLog("Function: " + function);
            stopwatch.Reset();
            stopwatch.Start();
        }

        private void OnAfterTest()
        {
            stopwatch.Stop();
            Logger.AddLog("Required time: " + stopwatch.Elapsed);
            Logger.AddLog("");
            Logger.AddLog("");
        }

        public bool PerformanceTestCase_TagMetadataEndpoint()
        {
            bool result = true;

            if (File.Exists(DataContext.GetDataFile()))
            {
                OnBeforeTest("TagMetadataEndpoint", "GetTagMetadata - PerformanceTest");
                try
                {
                    ITagMetadataEndpoint tester = Factory.CreateTagMetadataEndpoint();
                    TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(DataContext);
                    requestTagMetaData.tagIds.Clear();
                    TagMetaDataResponse responseTagMetaData;
                    Stopwatch watch = new Stopwatch();
                    int tagCount = 0;
                    TagMetaDataActionResult actionResult;

                    // load data
                    XDocument doc;
                    doc = XDocument.Load(DataContext.GetDataFile());
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

        public bool PerformanceTestCase_TagMetadataEndpointParallel()
        {
            bool result = true;

            if (File.Exists(DataContext.GetDataFile()))
            {
                OnBeforeTest("TagMetadataEndpoint", "GetTagMetadata - PerformanceTest Parallel");
                try
                {
                    ITagMetadataEndpoint tester = Factory.CreateTagMetadataEndpoint();
                    List<TagMetaDataRequest> requestList = new List<TagMetaDataRequest>();
                    TagMetaDataRequest requestTagMetaData = null;
                    int tagCount = 0;

                    // load data
                    XDocument doc;
                    doc = XDocument.Load(DataContext.GetDataFile());
                    foreach (var tag in doc.Descendants("tag"))
                    {

                        var tagId = tag.Attribute("id");
                        if (null == tagId)
                            continue;

                        if ((null == requestTagMetaData) || (requestTagMetaData.tagIds.Count == 1000))
                        {
                            requestTagMetaData = new TagMetaDataRequest(DataContext);
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

                            TagMetaDataActionResult actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
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

                OnAfterTest();
            }

            return result;
        }

    }
}
