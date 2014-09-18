using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.AccountManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
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
            AccountManagementActionResult actionResult;

            OnBeforeTest("Objects", "AccountManagementEndpoint", "Account Details");
            // call endpoint          
            AccountDetailsResponse responseDetailsData;
            actionResult = tester.GetAccountDetails(dataContext.GetViewType(), out responseDetailsData);
            result = result && (actionResult == AccountManagementActionResult.Successful);
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
            result = result && (actionResult == AccountManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responsePwdData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "AccountManagementEndpoint", "Reset Lost Password");
            // call endpoint          
            ResetLostPasswordRequest requestPwdResetData = new ResetLostPasswordRequest { emailAddress = dataContext.GeteMailAddress() };
            ResetLostPasswordResponse responsePwdResetData;
            actionResult = tester.ResetLostPassword(requestPwdResetData, out responsePwdResetData);
            result = result && (actionResult == AccountManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responsePwdResetData.ToJSON());
            OnAfterTest();

            return result;          
        }
    }
}
