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

using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.Flows.TestCase;
using Smartrac.SmartCosmos.Flows.BusinessRule;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;


namespace Smartrac.SmartCosmos.Flows.TestCase.Sample
{
    [TestCaseAttribute(1120)]
    public class TestCaseFlowsBusinessRuleEndpoint : BaseTestCaseBusinessRuleEndpoint
    {
        protected override bool ExecuteTests()
        {
            BusinessRuleResponse responseData;
            return TestCreateBusinessRule(out responseData) && TestListOfBusinessRule() && TestStructureOfBusinessRule(responseData) && TestStatusBusinessRule(dataContext.GetStatusStart(), responseData) &&
                TestStatusBusinessRule(dataContext.GetStatusStop(), responseData) && TestUpdateBusinessRule(responseData) && TestDeleteBusinessRule(responseData);
        }

        protected virtual bool TestCreateBusinessRule(out BusinessRuleResponse responseData)
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "StatusBusinessRule");
            // create request
            responseData = null;
            BusinessRuleRequest requestData = new BusinessRuleRequest
            {
                name = "apiTest",
                description = "Bar is a rule that can do baq"
            };
            BusinessRuleActionResult actionResult = endpoint.Create(requestData, out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }
        
        protected virtual bool TestStatusBusinessRule(StatusBusinessRule statusBusinessRule, BusinessRuleResponse requestData)
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "StatusBusinessRule");
            // create request
            BusinessRuleActionResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.SetStatus(requestData.ruleUrn, statusBusinessRule, out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }

        protected virtual bool TestListOfBusinessRule()
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "LookupBusinessRule");
            // create request
            BusinessRuleListResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.Lookup(out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }

        protected virtual bool TestStructureOfBusinessRule(BusinessRuleResponse requestData)
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "LookupBusinessRule");
            // create request
            BusinessRuleDataResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.Lookup(requestData.ruleUrn, out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }

        protected virtual bool TestUpdateBusinessRule(BusinessRuleResponse requestData)
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "UpdateBusinessRule");
            // create request
            BusinessRuleActionResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.Update(requestData.ruleUrn, dataContext.GetRuleData(), out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }

        protected virtual bool TestDeleteBusinessRule(BusinessRuleResponse requestData)
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "DeleteBusinessRule");
            // create request
            BusinessRuleActionResult actionResult = endpoint.Delete(requestData.ruleUrn);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }
    }
}
