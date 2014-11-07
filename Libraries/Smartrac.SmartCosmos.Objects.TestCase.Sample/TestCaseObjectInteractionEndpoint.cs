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

using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.ObjectInteraction;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(60)]
    public class TestCaseObjectInteractionEndpoint : BaseTestCaseObjectInteractionEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn interactionUrn;

            return RunTestCase_Create(out interactionUrn) &&
                RunTestCase_LookupByUrn(interactionUrn) &&
                RunTestCase_LookupByObjectUrn();
        }

        protected virtual bool RunTestCase_Create(out Urn interactionUrn)
        {
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
            ObjInteractActionResult actionResult = endpoint.Create(requestNewInteractionData, out responseNewInteractionData);
            // log response
            OnAfterTest(actionResult);
            interactionUrn = (responseNewInteractionData == null) ? null : responseNewInteractionData.interactionUrn;
            return (actionResult == ObjInteractActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByObjectUrn()
        {
            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Lookup Matching Interactions");
            QueryObjectInteractionsResponse responseLookupListData;
            // call endpoint
            ObjInteractActionResult actionResult = endpoint.Lookup(dataContext.GetObjectUrn(),
                                        out responseLookupListData,
                                        dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == ObjInteractActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByUrn(Urn interactionUrn)
        {
            OnBeforeTest("Objects", "ObjectInteractionEndpoint", "Lookup Specific Object Interaction by URN");
            QueryObjectInteractionResponse responseLookupData;
            // call endpoint
            ObjInteractActionResult actionResult = endpoint.LookupByUrn(interactionUrn,
                                            out responseLookupData,
                                            dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == ObjInteractActionResult.Successful);
        }
    }
}