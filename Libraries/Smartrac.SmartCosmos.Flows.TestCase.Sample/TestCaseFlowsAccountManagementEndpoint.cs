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

using Smartrac.SmartCosmos.Flows.AccountManagement;
using Smartrac.SmartCosmos.Flows.TestCase;
using Smartrac.SmartCosmos.TestCase.Base;

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