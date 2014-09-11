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
using Smartrac.SmartCosmos.Profiles.DataImport;
using Smartrac.SmartCosmos.Profiles.PlatformAvailability;
using Smartrac.SmartCosmos.Profiles.TagMetadata;
using Smartrac.SmartCosmos.Profiles.TagVerification;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.Registration;

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

            // SAMPLE 7 - Objects: File endpoint
            result = TestCase_FileEndpoint() && result;

            // SAMPLE 7 - Objects: Registration endpoint
            result = TestCase_RegistrationEndpoint() && result;

            Logger.AddLog("");
            Logger.AddLog("Total result: " + result);
            return result;
        }

        private void OnBeforeTest(string component, string endpoint, string function)
        {
            Logger.AddLog("-----------------------");
            Logger.AddLog("Component: " + component);
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

        #region PROFILES
        /// <summary>
        /// Test cases for verification endpoint
        /// </summary>
        public bool TestCase_TagVerificationEndpoint()
        {
            bool result = true;

            OnBeforeTest("Profiles", "TagVerificationEndpoint", "VerifyTags");
            // create client for endpoint
            ITagVerificationEndpoint tester = Factory.CreateTagVerificationEndpoint();
            // create request
            VerifyTagsRequest requestVerifyTags = new VerifyTagsRequest(TagDataContext);
            // call endpoint
            VerifyTagsResponse responseVerifyTags;
            TagVerificationActionResult actionResult = tester.VerifyTags(requestVerifyTags, out responseVerifyTags);
            result = (actionResult == TagVerificationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult.ToString());
            Logger.AddLog("Result Data: " + responseVerifyTags.ToJSON());
            OnAfterTest();

            OnBeforeTest("Profiles", "TagVerificationEndpoint", "GetVerificationMessage");
            // create request
            VerificationMessageRequest requestVerificationMessage = new VerificationMessageRequest();
            requestVerificationMessage.verificationState = 0;
            requestVerificationMessage.verificationType = TagDataContext.GetVerificationTypes().First<string>();
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

            OnBeforeTest("Profiles", "DataImportEndpoint", "CheckImportState");
            // create client for endpoint
            IDataImportEndpoint tester = Factory.CreateDataImportEndpoint();

            ImportStateResponse responseImportState;
            // call endpoint
            actionResult = tester.CheckImportState(new ImportStateRequest(TagDataContext.GetImportId()), out responseImportState);
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

            OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata");
            // create client for endpoint
            ITagMetadataEndpoint tester = Factory.CreateTagMetadataEndpoint();
            // create request
            TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(TagDataContext);

            TagMetaDataResponse responseTagMetaData;
            // call endpoint
            actionResult = tester.GetTagMetadata(requestTagMetaData, out responseTagMetaData);
            result = result && (actionResult == TagMetaDataActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseTagMetaData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMessage");
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

            OnBeforeTest("Profiles", "PlatformAvailabilityEndpoint", "Ping");
            // create client for endpoint
            IPlatformAvailabilityEndpoint tester = Factory.CreatePlatformAvailabilityEndpoint();
            // call endpoint & send response to console
            actionResult = tester.Ping();
            result = result && (actionResult == PlatformAvailabilityActionResult.Successful);

            Logger.AddLog("Result: " + actionResult);
            OnAfterTest();

            return result;
        }


        public bool PerformanceTestCase_TagMetadataEndpoint()
        {
            bool result = true;

            if (File.Exists(TagDataContext.GetTagDataFile()))
            {
                OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata - PerformanceTest");
                try
                {
                    ITagMetadataEndpoint tester = Factory.CreateTagMetadataEndpoint();
                    TagMetaDataRequest requestTagMetaData = new TagMetaDataRequest(TagDataContext);
                    requestTagMetaData.tagIds.Clear();
                    TagMetaDataResponse responseTagMetaData;
                    Stopwatch watch = new Stopwatch();
                    int tagCount = 0;
                    TagMetaDataActionResult actionResult;

                    // load data
                    XDocument doc;
                    doc = XDocument.Load(TagDataContext.GetTagDataFile());
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

            if (File.Exists(TagDataContext.GetTagDataFile()))
            {
                OnBeforeTest("Profiles", "TagMetadataEndpoint", "GetTagMetadata - PerformanceTest Parallel");
                try
                {
                    ITagMetadataEndpoint tester = Factory.CreateTagMetadataEndpoint();
                    List<TagMetaDataRequest> requestList = new List<TagMetaDataRequest>();
                    TagMetaDataRequest requestTagMetaData = null;
                    int tagCount = 0;

                    // load data
                    XDocument doc;
                    doc = XDocument.Load(TagDataContext.GetTagDataFile());
                    foreach (var tag in doc.Descendants("tag"))
                    {

                        var tagId = tag.Attribute("id");
                        if (null == tagId)
                            continue;

                        if ((null == requestTagMetaData) || (requestTagMetaData.tagIds.Count == 1000))
                        {
                            requestTagMetaData = new TagMetaDataRequest(TagDataContext);
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
        #endregion

        #region OBJECTS        
        public bool TestCase_FileEndpoint()
        {
            bool result = true;
            FileActionResult actionResult;

            OnBeforeTest("Objects", "FileEndpoint", "Related File Definitions Retrieval");
            // create client for endpoint
            IFileEndpoint tester = Factory.CreateFileEndpoint();
            FileDefinitionRetrievalListResponse responseListData;
            // create request & call endpoint
            actionResult = tester.RelatedFileDefinitionsRetrieval(FileDataContext.GetEntityReferenceType(),
                                                                  FileDataContext.GetUrnReference(),
                                                                  FileDataContext.GetViewType(),
                                                                  out responseListData
                                                                  );
            result = result && (actionResult == FileActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseListData.ToJSON());
            OnAfterTest();

            if ((null != responseListData) && (responseListData.Count > 0))
            {
                foreach (var item in responseListData)
                {
                    OnBeforeTest("Objects", "FileEndpoint", "File Delete");
                    // create request & call endpoint
                    actionResult = tester.FileDeletion(item.Urn);
                    result = result && (actionResult == FileActionResult.Successful);
                    // log response 
                    Logger.AddLog("Result: " + actionResult);
                    Logger.AddLog("Result Data: " + responseListData.ToJSON());
                    OnAfterTest();
                }
            }

            IEnumerable<FileDefinition> files = FileDataContext.GetFileDefinitions();
            foreach (var file in files)
            {
                OnBeforeTest("Objects", "FileEndpoint", "File Definition");
                // create request
                FileDefinitionRequest requestDefData = new FileDefinitionRequest
                {
                    entityReferenceType = FileDataContext.GetEntityReferenceType(),
                    referenceUrn = FileDataContext.GetUrnReference(),
                    mimeType = file.mimeType
                };
                FileDefinitionResponse responseDefData;
                actionResult = tester.GetFileDefinition(requestDefData, out responseDefData);
                result = result && (actionResult == FileActionResult.Successful);
                // log response 
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseDefData.ToJSON());
                OnAfterTest();

                OnBeforeTest("Objects", "FileEndpoint", "File Upload - Octet-Stream");
                Smartrac.SmartCosmos.Objects.File.FileUploadResponse responseUploadData;
                // create request & call endpoint
                actionResult = tester.UploadFileAsOctetStream(responseDefData.fileUrn, file.file, out responseUploadData);
                result = result && (actionResult == FileActionResult.Successful);
                // log response 
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseUploadData.ToJSON());
                OnAfterTest();

                OnBeforeTest("Objects", "FileEndpoint", "Specific File Definition Retrieval");
                // create request & call endpoint
                FileDefinitionRetrievalResponse responseRetrievalData;
                actionResult = tester.SpecificFileDefinitionRetrieval(responseDefData.fileUrn, FileDataContext.GetViewType(), out responseRetrievalData);
                result = result && (actionResult == FileActionResult.Successful);
                // log response 
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseRetrievalData.ToJSON());
                OnAfterTest();

                OnBeforeTest("Objects", "FileEndpoint", "File Content Retrieval");
                // create request & call endpoint
                FileContentRetrievalResponse responseContentData;
                actionResult = tester.FileContentRetrieval(responseDefData.fileUrn, out responseContentData);
                result = result && (actionResult == FileActionResult.Successful);
                // log response 
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseContentData.ToJSON());
                OnAfterTest();
            }

            OnBeforeTest("Objects", "FileEndpoint", "Related File Definitions Retrieval");
            // create request & call endpoint
            FileDefinitionRetrievalListResponse responseListResultData;
            actionResult = tester.RelatedFileDefinitionsRetrieval(FileDataContext.GetEntityReferenceType(),
                                                                   FileDataContext.GetUrnReference(),
                                                                   FileDataContext.GetViewType(),
                                                                   out responseListResultData);
            result = result && (actionResult == FileActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseListResultData.ToJSON());
            OnAfterTest();

            return result;
        }

        public bool TestCase_RegistrationEndpoint()
        {
            bool result = true;
            RegistrationActionResult actionResult;

            OnBeforeTest("Objects", "RegistrationEndpoint", "Realm Availability");
            // create client for endpoint
            IRegistrationEndpoint tester = Factory.CreateRegistrationEndpoint();
            RealmAvailabilityResponse responseRealmData;
            // call endpoint            
            actionResult = tester.GetRealmAvailability(RegistrationDataContext.GetRealm(), out responseRealmData);
            result = result && (actionResult == RegistrationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseRealmData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "RegistrationEndpoint", "Account Registration");
            // create client for endpoint
            AccountRegistrationRequest requestRegisterData = new AccountRegistrationRequest { 
                realm = RegistrationDataContext.GetRealm(), 
                emailAddress = RegistrationDataContext.GeteMailAddress() 
            };
            AccountRegistrationResponse responseRegisterData;
            // call endpoint            
            actionResult = tester.AccountRegistration(requestRegisterData, out responseRegisterData);
            result = result && (actionResult == RegistrationActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseRegisterData.ToJSON());
            OnAfterTest();

            return result;
        }

        #endregion

   }
}
