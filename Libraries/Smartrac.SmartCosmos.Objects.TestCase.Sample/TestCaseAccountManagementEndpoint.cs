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

using Smartrac.SmartCosmos.Objects.AccountManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(222)]
    public class TestCaseAccountManagementEndpoint : BaseTestCaseAccountManagementEndpoint
    {
        protected override bool ExecuteTests()
        {
            return RunTestCase_GetAccountDetails() &&
                RunTestCase_ChangeYourPassword() &&
                RunTestCase_ResetLostPassword();
        }

        protected virtual bool RunTestCase_GetAccountDetails()
        {
            OnBeforeTest("Objects", "AccountManagementEndpoint", "Account Details");
            // call endpoint
            AccountDetailsResponse responseDetailsData;
            AccountActionResult actionResult = endpoint.GetAccountDetails(out responseDetailsData, dataContext.GetViewType());
            // log response
            OnAfterTest(actionResult);
            return (actionResult == AccountActionResult.Successful);
        }

        protected virtual bool RunTestCase_ChangeYourPassword()
        {
            OnBeforeTest("Objects", "AccountManagementEndpoint", "Change Your Password");
            // call endpoint
            ChangeYourPasswordRequest requestPwdData = new ChangeYourPasswordRequest
            {
                newPassword = dataContext.GetNewPassword(),
                oldPassword = dataContext.GetOldPassword()
            };
            ChangeYourPasswordResponse responsePwdData;
            AccountActionResult actionResult = endpoint.ChangeYourPassword(requestPwdData, out responsePwdData);
            OnAfterTest(actionResult);
            return (actionResult == AccountActionResult.Successful);
        }

        protected virtual bool RunTestCase_ResetLostPassword()
        {
            OnBeforeTest("Objects", "AccountManagementEndpoint", "Reset Lost Password");
            // call endpoint
            ResetLostPasswordRequest requestPwdResetData = new ResetLostPasswordRequest { emailAddress = dataContext.GeteMailAddress() };
            ResetLostPasswordResponse responsePwdResetData;
            AccountActionResult actionResult = endpoint.ResetLostPassword(requestPwdResetData, out responsePwdResetData);
            OnAfterTest(actionResult);
            return (actionResult == AccountActionResult.Successful);
        }
    }
}