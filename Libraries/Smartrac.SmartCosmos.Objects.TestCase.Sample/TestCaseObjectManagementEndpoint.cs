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

using Smartrac.Logging;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.TestCase;
using Smartrac.SmartCosmos.Objects.ObjectManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(40)]
    public class TestCaseObjectManagementEndpoint : BaseTestCaseObjectManagementEndpoint
    {
       protected override bool ExecuteTests()
        {
            Urn objectUrn;
            Urn systemUrn;

            return RunTestCase_Create(out objectUrn) &&
                RunTestCase_Update() &&
                RunTestCase_LookupByObjectUrn(objectUrn, out systemUrn) &&
                RunTestCase_LookupByUrn(systemUrn) &&
                RunTestCase_Query();
        }

        protected virtual bool RunTestCase_Create(out Urn objectUrn)
        {
            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Create new object");
            // create request
            ObjectManagementNewRequest requestNewObjectData = new ObjectManagementNewRequest
            {
                urn = dataContext.GetObjectUrn(),
                type = dataContext.GetCategory(),
                name = dataContext.GetName(),
                description = dataContext.GetDescription(),
                activeFlag = dataContext.GetActiveFlag()
            };
            ObjectManagementResponse responseNewObjectData;
            // call endpoint
            ObjectActionResult actionResult = endpoint.Create(requestNewObjectData, out responseNewObjectData);
            OnAfterTest(actionResult);
            objectUrn = (responseNewObjectData == null) ? null : responseNewObjectData.objectUrn;
            return (actionResult == ObjectActionResult.Successful);
        }

        protected virtual bool RunTestCase_Update()
        {
            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Update an existing Object");
            // create request
            ObjectManagementRequest requestUpdateObjectData = new ObjectManagementRequest
            {
                urn = dataContext.GetObjectUrn(),
                description = dataContext.GetDescription() + "_updated"
            };
            ObjectManagementResponse responseUpdateObjectData;
            // call endpoint
            ObjectActionResult actionResult = endpoint.Update(requestUpdateObjectData, out responseUpdateObjectData);
            OnAfterTest(actionResult);
            return (actionResult == ObjectActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByUrn(Urn systemUrn)
        {
            ObjectDataResponse responseObjectData;
            if ((systemUrn == null) || (!systemUrn.IsValid()))
            {
            Logger.AddLog("Skip RunTestCase_Lookup", LogType.Info);
            }

                OnBeforeTest("Objects", "ObjectManagementEndpoint", "Lookup Specific Object by URN");
                // call endpoint
                ObjectActionResult actionResult = endpoint.Lookup(systemUrn, out responseObjectData, dataContext.GetViewType());
                OnAfterTest(actionResult);
            return (actionResult == ObjectActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByObjectUrn(Urn objectUrn, out Urn systemUrn)
        {
            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Lookup Object by Object URN");
            ObjectDataResponse responseObjectData;
            // call endpoint
            ObjectActionResult actionResult = endpoint.LookupByObjectUrn(objectUrn, out responseObjectData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            systemUrn = (responseObjectData == null) ? null : responseObjectData.urnObj;

            return (actionResult == ObjectActionResult.Successful);
        }

        protected virtual bool RunTestCase_Query()
        {
            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Query Objects");
            // create request
            QueryObjectsRequest requestQueryObjectData = new QueryObjectsRequest
            {
                type = dataContext.GetCategory()
            };
            // call endpoint
            QueryObjectsResponse responseQueryObjectsData;
            ObjectActionResult actionResult = endpoint.QueryObjects(requestQueryObjectData, out responseQueryObjectsData);
            OnAfterTest(actionResult);
            return (actionResult == ObjectActionResult.Successful);
        }
    }
}