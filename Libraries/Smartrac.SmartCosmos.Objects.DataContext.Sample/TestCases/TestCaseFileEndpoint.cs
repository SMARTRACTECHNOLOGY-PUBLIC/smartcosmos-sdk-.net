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
using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute(100)]
    public class TestCaseFileEndpoint : BaseObjectsTestCase
    {
        protected override bool DoRun()
        {
            IFileDataContext dataContext = DataContextFactory.CreateFileDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip FileEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            IFileEndpoint tester = EndpointFactory.CreateFileEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateFileEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            FileActionResult actionResult;

            OnBeforeTest("Objects", "FileEndpoint", "Related File Definitions Retrieval");
            FileDefinitionRetrievalListResponse responseListData;
            // create request & call endpoint
            actionResult = tester.LookupDefinitions(dataContext.GetEntityReferenceType(),
                                                                  dataContext.GetUrnReference(),
                                                                  out responseListData,
                                                                  dataContext.GetViewType()
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
                    actionResult = tester.Delete(item.Urn);
                    result = result && (actionResult == FileActionResult.Successful);
                    // log response
                    Logger.AddLog("Result: " + actionResult);
                    Logger.AddLog("Result Data: " + responseListData.ToJSON());
                    OnAfterTest();
                }
            }

            IEnumerable<FileDefinition> files = dataContext.GetFileDefinitions();
            foreach (var file in files)
            {
                OnBeforeTest("Objects", "FileEndpoint", "File Definition");
                // create request
                FileDefinitionRequest requestDefData = new FileDefinitionRequest
                {
                    entityReferenceType = dataContext.GetEntityReferenceType(),
                    referenceUrn = dataContext.GetUrnReference(),
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
                actionResult = tester.LookupDefinition(responseDefData.fileUrn, out responseRetrievalData, dataContext.GetViewType());
                result = result && (actionResult == FileActionResult.Successful);
                // log response
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseRetrievalData.ToJSON());
                OnAfterTest();

                OnBeforeTest("Objects", "FileEndpoint", "File Content Retrieval");
                // create request & call endpoint
                FileContentRetrievalResponse responseContentData;
                actionResult = tester.LookupContent(responseDefData.fileUrn, out responseContentData);
                result = result && (actionResult == FileActionResult.Successful);
                // log response
                Logger.AddLog("Result: " + actionResult);
                Logger.AddLog("Result Data: " + responseContentData.ToJSON());
                OnAfterTest();
            }

            OnBeforeTest("Objects", "FileEndpoint", "Related File Definitions Retrieval");
            // create request & call endpoint
            FileDefinitionRetrievalListResponse responseListResultData;
            actionResult = tester.LookupDefinitions(dataContext.GetEntityReferenceType(),
                                                                   dataContext.GetUrnReference(),
                                                                   out responseListResultData,
                                                                   dataContext.GetViewType());
            result = result && (actionResult == FileActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseListResultData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}