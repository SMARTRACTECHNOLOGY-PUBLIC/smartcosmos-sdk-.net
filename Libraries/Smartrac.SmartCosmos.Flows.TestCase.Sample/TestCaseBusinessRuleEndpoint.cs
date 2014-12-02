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
            return TestListOfBusinessRule() && TestStructureOfBusinessRule() && TestStatusBusinessRule(dataContext.GetStatusStart()) &&
                TestStatusBusinessRule(dataContext.GetStatusStop()) && TestUpdateBusinessRule() && TestDeleteBusinessRule();
        }

        protected virtual bool TestStatusBusinessRule(StatusBusinessRule statusBusinessRule)
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "StatusBusinessRule");
            // create request
            BusinessRuleActionResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.SetStatus(dataContext.GetUrn(), statusBusinessRule, out responseData);
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

        protected virtual bool TestStructureOfBusinessRule()
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "LookupBusinessRule");
            // create request
            BusinessRuleDataResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.Lookup(dataContext.GetUrn(), out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }

        protected virtual bool TestUpdateBusinessRule()
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "UpdateBusinessRule");
            // create request
            BusinessRuleActionResponse responseData;
            BusinessRuleActionResult actionResult = endpoint.Update(dataContext.GetUrn(), dataContext.GetRuleData(), out responseData);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }

        protected virtual bool TestDeleteBusinessRule()
        {
            OnBeforeTest("Flows", "BusinessRuleEndpoint", "DeleteBusinessRule");
            // create request
            Urn deleteUrn = new Urn("urn:uuid:63df5662-3e18-4d7a-8649-0b4c6f96cbc0");
            BusinessRuleActionResult actionResult = endpoint.Delete(deleteUrn);
            OnAfterTest(actionResult);
            return (actionResult == BusinessRuleActionResult.Successful);
        }
    }
}
