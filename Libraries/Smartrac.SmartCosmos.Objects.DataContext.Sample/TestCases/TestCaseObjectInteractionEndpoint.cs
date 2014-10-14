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
using Smartrac.SmartCosmos.Objects.ObjectInteraction;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseObjectInteractionEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IObjectInteractionDataContext dataContext = DataContextFactory.CreateObjectInteractionDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip ObjectInteractionEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IObjectInteractionEndpoint tester = EndpointFactory.CreateObjectInteractionEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateObjectInteractionEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            ObjInteractActionResult actionResult;

            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Capture a specific interaction");
            // create request
            CaptureObjectInteractionRequest requestNewInteractionData = new CaptureObjectInteractionRequest
            {
                objectUrnObj = dataContext.GetObjectUrn(),
                entityReferenceType = dataContext.GetEntityReferenceType(),
                referenceUrnObj = dataContext.GetReferenceUrn(),
                type = dataContext.GetInteractionType(),
                recordedTimestamp = dataContext.GetRecordedTimestamp(),
            };
            CaptureObjectInteractionResponse responseNewInteractionData;
            // call endpoint
            actionResult = tester.Create(requestNewInteractionData, out responseNewInteractionData);
            result = result && (actionResult == ObjInteractActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewInteractionData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Lookup Matching Interactions");
            QueryObjectInteractionsResponse responseLookupData;
            // call endpoint
            actionResult = tester.Lookup(dataContext.GetObjectUrn(),
                                        out responseLookupData,
                                        dataContext.GetViewType());
            result = result && (actionResult == ObjInteractActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Lookup Specific Object Interaction by URN");
            // call endpoint
            actionResult = tester.LookupByUrn(responseNewInteractionData.interactionUrn,
                                            out responseLookupData,
                                            dataContext.GetViewType());
            result = result && (actionResult == ObjInteractActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseLookupData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}