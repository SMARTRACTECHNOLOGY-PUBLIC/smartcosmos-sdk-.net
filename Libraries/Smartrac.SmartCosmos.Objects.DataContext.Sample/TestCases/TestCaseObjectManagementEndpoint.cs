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
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.ObjectManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseObjectManagementEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IObjectManagementDataContext dataContext = DataContextFactory.CreateObjectManagementDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip ObjectManagementEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IObjectManagementEndpoint tester = EndpointFactory.CreateObjectManagementEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateObjectManagementEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            ObjectActionResult actionResult;

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
            actionResult = tester.Create(requestNewObjectData, out responseNewObjectData);
            result = result && (actionResult == ObjectActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Update an existing Object");
            // create request
            ObjectManagementRequest requestUpdateObjectData = new ObjectManagementRequest
            {
                urn = dataContext.GetObjectUrn(),
                description = dataContext.GetDescription() + "_updated"
            };
            ObjectManagementResponse responseUpdateObjectData;
            // call endpoint  
            actionResult = tester.Update(requestUpdateObjectData, out responseUpdateObjectData);
            result = result && (actionResult == ObjectActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUpdateObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Lookup Specific Object by URN");
            ObjectDataResponse responseObjectData;
            // call endpoint  
            actionResult = tester.Lookup(dataContext.GetObjectUrn(), out responseObjectData, dataContext.GetViewType());
            result = result && (actionResult == ObjectActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Lookup Object by Object URN");
            // call endpoint  
            actionResult = tester.Lookup(responseNewObjectData.objectUrn, out responseObjectData, dataContext.GetViewType());
            result = result && (actionResult == ObjectActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseObjectData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectManagementEndpoint", "Query Objects");
            // create request
            QueryObjectsRequest requestQueryObjectData = new QueryObjectsRequest
            {
                type = dataContext.GetCategory()
            };
            // call endpoint 
            QueryObjectsResponse responseQueryObjectsData;
            actionResult = tester.QueryObjects(requestQueryObjectData, out responseQueryObjectsData);
            result = result && (actionResult == ObjectActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseQueryObjectsData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}
