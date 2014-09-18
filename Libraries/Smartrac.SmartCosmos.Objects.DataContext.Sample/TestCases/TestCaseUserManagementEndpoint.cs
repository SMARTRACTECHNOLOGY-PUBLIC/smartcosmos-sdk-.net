﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smartrac.Logging;
using Smartrac.SmartCosmos.Objects.DataContext;
using Smartrac.SmartCosmos.Objects.UserManagement;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute]
    public class TestCaseUserManagementEndpoint : BaseTestCase
    {
        protected override bool DoRun()
        {
            IUserManagementDataContext dataContext = DataContextFactory.CreateUserManagementDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip UserManagementEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IUserManagementEndpoint tester = EndpointFactory.CreateUserManagementEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateUserManagementEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            UserManagementActionResult actionResult;

            OnBeforeTest("Objects", "UserManagementEndpoint", "Create new user");
            // create request         
            UserManagementRequest requestNewUserData = new UserManagementRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                givenName = dataContext.GetGivenName(),
                surname = dataContext.GetSurname(),
                roleType = dataContext.GetRoleType()
            };
            UserManagementResponse responseNewUserData;
            // call endpoint  
            actionResult = tester.CreateNewUser(requestNewUserData, out responseNewUserData);
            result = result && (actionResult == UserManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseNewUserData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "UserManagementEndpoint", "Update an existing user");
            // create request
            UserManagementRequest requestUpdateUserData = new UserManagementRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                givenName = dataContext.GetGivenName(),
                surname = dataContext.GetSurname() + "_updated",
            };
            UserManagementResponse responseUpdateUserData;
            // call endpoint  
            actionResult = tester.UpdateUser(requestUpdateUserData, out responseUpdateUserData);
            result = result && (actionResult == UserManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUpdateUserData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "UserManagementEndpoint", "Lookup Specific User by URN");
            UserDataResponse responseUserData;
            // call endpoint  
            actionResult = tester.LookupSpecificUser(responseNewUserData.userUrn, dataContext.GetViewType(), out responseUserData);
            result = result && (actionResult == UserManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUserData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "UserManagementEndpoint", "Lookup Specific User by Email Address");
            UserDataResponse responseUserEMailData;
            // call endpoint  
            actionResult = tester.LookupSpecificUser(dataContext.GeteMailAddress(), dataContext.GetViewType(), out responseUserEMailData);
            result = result && (actionResult == UserManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responseUserEMailData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "UserManagementEndpoint", "Change or Reset User Password");
            ChangeOrResetUserPasswordRequest requestPasswordResetData = new ChangeOrResetUserPasswordRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                newPassword = dataContext.GetNewPassword()
            };
            ChangeOrResetUserPasswordResponse responsePasswordResetData;
            // call endpoint  
            actionResult = tester.ChangeOrResetUserPassword(requestPasswordResetData, out responsePasswordResetData);
            result = result && (actionResult == UserManagementActionResult.Successful);
            // log response 
            Logger.AddLog("Result: " + actionResult);
            Logger.AddLog("Result Data: " + responsePasswordResetData.ToJSON());
            OnAfterTest();

            return result;
        }
    }
}