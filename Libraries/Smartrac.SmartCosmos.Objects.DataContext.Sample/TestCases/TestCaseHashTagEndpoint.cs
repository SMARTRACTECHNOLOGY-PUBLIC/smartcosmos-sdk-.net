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
using Smartrac.SmartCosmos.Objects.HashTag;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.Objects.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseHashTagEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IHashTagDataContext dataContext = DataContextFactory.CreateHashTagDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip HashTagEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IHashTagEndpoint tester = EndpointFactory.CreateHashTagEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateHashTagEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            HashTagActionResult actionResult;

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
            actionResult = tester.Create(requestNewData, out responseNewData);
            result = result && (actionResult == HashTagActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "HashTagEndpoint", "Lookup specific hash tag by URN");
            HashTagDataResponse responseLookupData;
            // call endpoint  
            actionResult = tester.Lookup(responseNewData.tagUrn, out responseLookupData, dataContext.GetViewType());
            result = result && (actionResult == HashTagActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "HashTagEndpoint", "Lookup specific hash tag by name");
            // call endpoint  
            actionResult = tester.Lookup(dataContext.GetName(), out responseLookupData, dataContext.GetViewType());
            result = result && (actionResult == HashTagActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "HashTagEndpoint", "Lookup specific hash tag by object reference");
            HashTagListResponse responseLookupListData;
            // call endpoint  
            actionResult = tester.Lookup(dataContext.GetEntityReferenceType(),
                                         dataContext.GetReferenceUrn(),
                                         out responseLookupListData, 
                                         dataContext.GetViewType());
            result = result && (actionResult == HashTagActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupListData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "HashTagEndpoint", "Assign hash tags to an object reference");
            HashTagListRequest requestAssignData = new HashTagListRequest();
            requestAssignData.Add( dataContext.GetName() );
            DefaultResponse responseAssignData;
            // call endpoint  
            actionResult = tester.Assign(dataContext.GetEntityReferenceType(),
                                         dataContext.GetReferenceUrn(),
                                         requestAssignData,
                                         out responseAssignData);
            result = result && (actionResult == HashTagActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseAssignData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "HashTagEndpoint", "Delete hash tag");
            // call endpoint  
            actionResult = tester.Delete(responseNewData.tagUrn);
            result = result && (actionResult == HashTagActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            OnAfterTest();

            return result;
        }
    }
}
