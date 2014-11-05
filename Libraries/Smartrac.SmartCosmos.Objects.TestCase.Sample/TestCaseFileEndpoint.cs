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
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.TestCase;
using Smartrac.SmartCosmos.Objects.File;
using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.Objects.DataContext;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(100)]
    public class TestCaseFileEndpoint : BaseTestCaseFileEndpoint
    {
        protected override bool ExecuteTests()
        {
            FileDefinitionRetrievalListResponse responseListData;
            IEnumerable<FileDefinition> files = dataContext.GetFileDefinitions();
            Urn fileUrn;

            bool bResult = RunTestCase_RetrievalFiles(out responseListData) && 
                           RunTestCase_Delete(responseListData);

            foreach (var file in files)
            {
                bResult = RunTestCase_Define(file, out fileUrn) &&
                          RunTestCase_UploadOctetStream(file, fileUrn) &&
                          RunTestCase_LookupByUrn(fileUrn) &&
                          RunTestCase_DownloadContent(fileUrn) &&
                          RunTestCase_Delete(fileUrn) &&
                          bResult;
            }
            return RunTestCase_Lookup() && bResult;
        }

        protected virtual bool RunTestCase_RetrievalFiles(out FileDefinitionRetrievalListResponse responseListData)
        {
            OnBeforeTest("Objects", "FileEndpoint", "Related File Definitions Retrieval");
            // create request & call endpoint
            FileActionResult actionResult = endpoint.LookupDefinitions(dataContext.GetEntityReferenceType(),
                                                                  dataContext.GetUrnReference(),
                                                                  out responseListData,
                                                                  dataContext.GetViewType()
                                                                  );
            OnAfterTest(actionResult);
            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_Delete(FileDefinitionRetrievalListResponse responseListData)
        {
            if ((null == responseListData) || (responseListData.Count == 0))
                return true;

            FileActionResult actionResult = FileActionResult.Successful;
            foreach (var item in responseListData)
            {
                if (item.Urn.IsValid())
                {
                    OnBeforeTest("Objects", "FileEndpoint", "File Delete");
                    // create request & call endpoint
                    actionResult = endpoint.Delete(item.Urn);
                    OnAfterTest(actionResult);
                    if (actionResult != FileActionResult.Successful)
                        return false;
                }
            }
            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_Delete(Urn fileUrn)
        {
            if ((null == fileUrn))
                return true;

            FileActionResult actionResult = FileActionResult.Successful;
            if (fileUrn.IsValid())
            {
                OnBeforeTest("Objects", "FileEndpoint", "File Delete");
                // create request & call endpoint
                actionResult = endpoint.Delete(fileUrn);
                OnAfterTest(actionResult);
                if (actionResult != FileActionResult.Successful)
                    return false;
            }
            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_Define(FileDefinition file, out Urn fileUrn)
        {
            OnBeforeTest("Objects", "FileEndpoint", "File Definition");
            // create request
            FileDefinitionRequest requestDefData = new FileDefinitionRequest
            {
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetUrnReference(),
                mimeType = file.mimeType
            };
            FileDefinitionResponse responseDefData;
            FileActionResult actionResult = endpoint.GetFileDefinition(requestDefData, out responseDefData);
            OnAfterTest(actionResult);

            fileUrn = (responseDefData == null) ? null : responseDefData.fileUrn;

            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_UploadOctetStream(FileDefinition file, Urn fileUrn)
        {
            OnBeforeTest("Objects", "FileEndpoint", "File Upload - Octet-Stream");
            Smartrac.SmartCosmos.Objects.File.FileUploadResponse responseUploadData;
            // create request & call endpoint
            FileActionResult actionResult = endpoint.UploadFileAsOctetStream(fileUrn, file.file, out responseUploadData);
            OnAfterTest(actionResult);
            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByUrn(Urn fileUrn)
        {
            OnBeforeTest("Objects", "FileEndpoint", "Specific File Definition Retrieval");
            // create request & call endpoint
            FileDefinitionRetrievalResponse responseRetrievalData;
            FileActionResult actionResult = endpoint.LookupDefinition(fileUrn, out responseRetrievalData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_DownloadContent(Urn fileUrn)
        {
            OnBeforeTest("Objects", "FileEndpoint", "File Content Retrieval");
            // create request & call endpoint
            FileContentRetrievalResponse responseContentData;
            FileActionResult actionResult = endpoint.LookupContent(fileUrn, out responseContentData);
            OnAfterTest(actionResult);
            return (actionResult == FileActionResult.Successful);
        }

        protected virtual bool RunTestCase_Lookup()
        {
            OnBeforeTest("Objects", "FileEndpoint", "Related File Definitions Retrieval");
            // create request & call endpoint
            FileDefinitionRetrievalListResponse responseListResultData;
            FileActionResult actionResult = endpoint.LookupDefinitions(dataContext.GetEntityReferenceType(),
                                                                   dataContext.GetUrnReference(),
                                                                   out responseListResultData,
                                                                   dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == FileActionResult.Successful);
        }
    }
}