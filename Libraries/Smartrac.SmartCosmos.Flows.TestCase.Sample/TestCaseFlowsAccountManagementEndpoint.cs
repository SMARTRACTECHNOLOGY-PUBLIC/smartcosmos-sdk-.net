using Smartrac.SmartCosmos.Flows.AccountManagement;
using Smartrac.SmartCosmos.TestCase.Base;
using Smartrac.SmartCosmos.Flows.TestCase;

namespace Smartrac.SmartCosmos.Flows.DataContext.Sample
{
    [TestCaseAttribute(20)]
    public class TestCaseFlowsAccountManagementEndpoint : BaseTestCaseFlowsAccountManagementEndpoint
    {
        protected override bool ExecuteTests()
        {
           return TestGetAccountDetails() /*&& TestResetLostPassword()*/;
        }

        protected virtual bool TestGetAccountDetails()
        {
            OnBeforeTest("Flows", "FlowsAccountManagementEndpoint", "GetAccountDetails");
            // create request
            AccountDetailsResponse requestAccountDetails = new AccountDetailsResponse();
            AccountActionResult actionResult = endpoint.GetAccountDetails(out requestAccountDetails);
            OnAfterTest(actionResult);
            return (actionResult == AccountActionResult.Successful);
        }

        /*
        protected virtual bool TestResetLostPassword()
        {
            OnBeforeTest("Flows", "FlowsAccountManagementEndpoint", "ResetLostPassword");
            // create request
            ResetLostPasswordRequest requestData = new ResetLostPasswordRequest();
            requestData.emailAddress = dataContext.GeteMailAddress();
            ResetLostPasswordResponse responseData = new ResetLostPasswordResponse();
            AccountActionResult actionResult = endpoint.ResetLostPassword(requestData, out responseData);
            OnAfterTest(actionResult);
            return (actionResult == AccountActionResult.Successful);
        }
         */
    }
}
