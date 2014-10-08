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
using Smartrac.SmartCosmos.Objects.ObjectInteractionSession;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.Objects.ObjectInteraction;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseObjectInteractionSessionEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IObjectInteractionSessionDataContext dataContext = DataContextFactory.CreateObjectInteractionSessionDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip ObjectInteractionSessionEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IObjectInteractionSessionEndpoint tester = EndpointFactory.CreateObjectInteractionSessionEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateObjectInteractionSessionEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            ObjInteractSessionActionResult actionResult;

            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Start a new object interaction session");
            // create request         
            StartObjectInteractionSessionRequest requestStartData = new StartObjectInteractionSessionRequest
            {
                name = dataContext.GetName(),
                description = dataContext.GetDescription(),
                activeFlag = dataContext.GetActiveFlag(),
                type = dataContext.GetInteractionType(),
                moniker = dataContext.GetMoniker(),
            };
            StartObjectInteractionSessionResponse responseStartData;
            // call endpoint  
            actionResult = tester.Start(requestStartData, out responseStartData);
            result = result && (actionResult == ObjInteractSessionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseStartData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Stop an existing object interaction session");
            StopObjectInteractionSessionResponse responseStopData;
            // call endpoint  
            actionResult = tester.Stop( responseStartData.sessionUrn,
                                        out responseStopData);
            result = result && (actionResult == ObjInteractSessionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseStopData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Lookup Specific Object Interaction Session by URN");
            ObjectInteractionSessionDataResponse responseLookupData;
            // call endpoint  
            actionResult = tester.Lookup(responseStartData.sessionUrn,
                                            out responseLookupData,
                                            dataContext.GetViewType());
            result = result && (actionResult == ObjInteractSessionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Lookup Object Interaction Sessions by Name");
            // call endpoint  
            actionResult = tester.Lookup(requestStartData.name,
                                         out responseLookupData,
                                         dataContext.GetViewType());
            result = result && (actionResult == ObjInteractSessionActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}
