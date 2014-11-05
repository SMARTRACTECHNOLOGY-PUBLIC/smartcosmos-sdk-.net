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
using Smartrac.SmartCosmos.Objects.Base;
using Smartrac.SmartCosmos.Objects.TestCase;
using Smartrac.SmartCosmos.Objects.UserManagement;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(30)]
    public class TestCaseUserManagementEndpoint : BaseTestCaseUserManagementEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn userUrn;

            return RunTestCase_CreateNewUser() &&
                RunTestCase_UpdateUser() &&
                RunTestCase_LookupUserByEmail(out userUrn) &&
                RunTestCase_LookupUserByURN(userUrn) &&                
                RunTestCase_ChangeOrResetUserPassword();
        }

        protected virtual bool RunTestCase_CreateNewUser()
        {
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
            UserActionResult actionResult = endpoint.CreateNewUser(requestNewUserData, out responseNewUserData);
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_UpdateUser()
        {
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
            UserActionResult actionResult = endpoint.UpdateUser(requestUpdateUserData, out responseUpdateUserData);
            OnAfterTest(actionResult);

            return (actionResult == UserActionResult.Successful);
        }
        
        protected virtual bool RunTestCase_LookupUserByURN(Urn userUrn)
        {
            OnBeforeTest("Objects", "UserManagementEndpoint", "Lookup Specific User by URN");
            UserDataResponse responseUserData;
            // call endpoint
            UserActionResult actionResult = endpoint.LookupSpecificUser(userUrn, out responseUserData, dataContext.GetViewType());
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);

        }

        protected virtual bool RunTestCase_LookupUserByEmail(out Urn userUrn)
        {
            OnBeforeTest("Objects", "UserManagementEndpoint", "Lookup Specific User by Email Address");
            UserDataResponse responseUserEMailData;
            // call endpoint
            UserActionResult actionResult = endpoint.LookupSpecificUser(dataContext.GeteMailAddress(), out responseUserEMailData, dataContext.GetViewType());
            // log response
            OnAfterTest(actionResult);

            if (responseUserEMailData != null)
                userUrn = responseUserEMailData.urnObj;
            else
                userUrn = null;
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_ChangeOrResetUserPassword()
        {
            OnBeforeTest("Objects", "UserManagementEndpoint", "Change or Reset User Password");
            ChangeOrResetUserPasswordRequest requestPasswordResetData = new ChangeOrResetUserPasswordRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                newPassword = dataContext.GetNewPassword()
            };
            ChangeOrResetUserPasswordResponse responsePasswordResetData;
            // call endpoint
            UserActionResult actionResult = endpoint.ChangeOrResetUserPassword(requestPasswordResetData, out responsePasswordResetData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }
    }
}