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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.Objects.HashTag;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(51)]
    public class TestCaseHashTagEndpoint : BaseTestCaseHashTagEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn tagUrn;

            return RunTestCase_Create(out tagUrn) &&
                RunTestCase_LookupByUrn(tagUrn) &&
                RunTestCase_LookupByName() &&
                RunTestCase_Assign() &&
                RunTestCase_LookupByObjectUrn() &&
                RunTestCase_Delete();
        }

        protected virtual bool RunTestCase_Create(out Urn tagUrn)
        {
            OnBeforeTest("Objects", "HashTagEndpoint", "Create new hash tag");
            // create request
            HashTagRequest requestNewData = new HashTagRequest
            {
                activeFlag = dataContext.GetActiveFlag(),
                description = dataContext.GetDescription(),
                moniker = dataContext.GetMoniker(),
                name = dataContext.GetName()
            };
            HashTagResponse responseNewData;
            // call endpoint
            HashTagActionResult actionResult = endpoint.Create(requestNewData, out responseNewData);
            OnAfterTest(actionResult);
            tagUrn = (responseNewData == null) ? null : responseNewData.tagUrn;
            return (actionResult == HashTagActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByUrn(Urn tagUrn)
        {
            OnBeforeTest("Objects", "HashTagEndpoint", "Lookup specific hash tag by URN");
            HashTagDataResponse responseLookupData;
            // call endpoint
            HashTagActionResult actionResult = endpoint.Lookup(tagUrn, out responseLookupData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == HashTagActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByName()
        {
            OnBeforeTest("Objects", "HashTagEndpoint", "Lookup specific hash tag by name");
            HashTagDataResponse responseLookupData;
            // call endpoint
            HashTagActionResult actionResult = endpoint.Lookup(dataContext.GetName(), out responseLookupData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == HashTagActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByObjectUrn()
        {
            OnBeforeTest("Objects", "HashTagEndpoint", "Lookup specific hash tag by object reference");
            HashTagListResponse responseLookupListData;
            // call endpoint
            HashTagActionResult actionResult = endpoint.Lookup(dataContext.GetEntityReferenceType(),
                                         dataContext.GetReferenceUrn(),
                                         out responseLookupListData,
                                         dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == HashTagActionResult.Successful);
        }

        protected virtual bool RunTestCase_Assign()
        {
            OnBeforeTest("Objects", "HashTagEndpoint", "Assign hash tags to an object reference");
            HashTagListRequest requestAssignData = new HashTagListRequest();
            requestAssignData.Add(dataContext.GetName());
            HashTagListResponse responseAssignData;
            // call endpoint
            HashTagActionResult actionResult = endpoint.Assign(dataContext.GetEntityReferenceType(),
                                         dataContext.GetReferenceUrn(),
                                         requestAssignData,
                                         out responseAssignData);
            OnAfterTest(actionResult);
            return (actionResult == HashTagActionResult.Successful);
        }

        protected virtual bool RunTestCase_Delete()
        {
            OnBeforeTest("Objects", "HashTagEndpoint", "Delete hash tag");
            // call endpoint
            HashTagActionResult actionResult = endpoint.Delete(dataContext.GetName());
            OnAfterTest(actionResult);
            return (actionResult == HashTagActionResult.Successful);
        }
    }
}