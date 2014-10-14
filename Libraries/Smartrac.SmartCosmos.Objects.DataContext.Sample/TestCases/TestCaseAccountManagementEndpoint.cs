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
using Smartrac.SmartCosmos.Objects.AccountManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseAccountManagementEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IAccountManagementDataContext dataContext = DataContextFactory.CreateAccountManagementDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip AccountManagementEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IAccountManagementEndpoint tester = EndpointFactory.CreateAccountManagementEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateAccountManagementEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            AccountActionResult actionResult;

            OnBeforeTest("Objects", "AccountManagementEndpoint", "Account Details");
            // call endpoint
            AccountDetailsResponse responseDetailsData;
            actionResult = tester.GetAccountDetails(dataContext.GetViewType(), out responseDetailsData);
            result = result && (actionResult == AccountActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseDetailsData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "AccountManagementEndpoint", "Change Your Password");
            // call endpoint
            ChangeYourPasswordRequest requestPwdData = new ChangeYourPasswordRequest
            {
                newPassword = dataContext.GetNewPassword(),
                oldPassword = dataContext.GetOldPassword()
            };
            ChangeYourPasswordResponse responsePwdData;
            actionResult = tester.ChangeYourPassword(requestPwdData, out responsePwdData);
            result = result && (actionResult == AccountActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responsePwdData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "AccountManagementEndpoint", "Reset Lost Password");
            // call endpoint
            ResetLostPasswordRequest requestPwdResetData = new ResetLostPasswordRequest { emailAddress = dataContext.GeteMailAddress() };
            ResetLostPasswordResponse responsePwdResetData;
            actionResult = tester.ResetLostPassword(requestPwdResetData, out responsePwdResetData);
            result = result && (actionResult == AccountActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responsePwdResetData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}