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
using Smartrac.SmartCosmos.Objects.ObjectInteractionSession;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(107)]
    public class TestCaseObjectInteractionSessionEndpoint : BaseTestCaseObjectInteractionSessionEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn sessionUrn;

            return RunTestCase_Start(out sessionUrn) &&
                RunTestCase_Stop(sessionUrn) &&
                RunTestCase_Lookup(sessionUrn) &&
                RunTestCase_LookupByName();
        }

        protected virtual bool RunTestCase_Start(out Urn sessionUrn)
        {
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
            ObjInteractSessionActionResult actionResult = endpoint.Start(requestStartData, out responseStartData);
            OnAfterTest(actionResult);
            sessionUrn = (responseStartData == null) ? null : responseStartData.sessionUrn;
            return (actionResult == ObjInteractSessionActionResult.Successful);
        }

        protected virtual bool RunTestCase_Stop(Urn sessionUrn)
        {
            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Stop an existing object interaction session");
            StopObjectInteractionSessionResponse responseStopData;
            // call endpoint
            ObjInteractSessionActionResult actionResult = endpoint.Stop(sessionUrn,
                                        out responseStopData);
            OnAfterTest(actionResult);
            return (actionResult == ObjInteractSessionActionResult.Successful);
        }

        protected virtual bool RunTestCase_Lookup(Urn sessionUrn)
        {
            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Lookup Specific Object Interaction Session by URN");
            ObjectInteractionSessionDataResponse responseLookupData;
            // call endpoint
            ObjInteractSessionActionResult actionResult = endpoint.Lookup(sessionUrn,
                                            out responseLookupData,
                                            dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == ObjInteractSessionActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupByName()
        {
            OnBeforeTest("Objects", "ObjectInteractionSessionEndpoint", "Lookup Object Interaction Sessions by Name");
            ObjectInteractionSessionDataListResponse responseLookupListData;
            // call endpoint
            ObjInteractSessionActionResult actionResult = endpoint.Lookup(dataContext.GetName(),
                                         out responseLookupListData,
                                         dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == ObjInteractSessionActionResult.Successful);
        }
    }
}