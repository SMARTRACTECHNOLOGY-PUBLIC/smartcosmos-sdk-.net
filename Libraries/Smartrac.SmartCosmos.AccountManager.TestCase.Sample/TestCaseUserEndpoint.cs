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

using Smartrac.SmartCosmos.AccountManager.User;
using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.TestCase.Base;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.AccountManager.TestCase.Sample
{
    [TestCaseAttribute(2)]
    public class TestCaseUserEndpoint : BaseTestCaseUserEndpoint
    {
        protected override bool ExecuteTests()
        {
            return RunTestCase_Lookup() &&
                RunTestCase_ResetLostPassword() &&
                RunTestCase_Update() &&
                RunTestCase_Assign() &&
                RunTestCase_Dissociate() &&
                (RunTestCase_CreateUser() || true);
        }

        protected virtual bool RunTestCase_CreateUser()
        {
            OnBeforeTest("AccountManager", "UserEndpoint", "Create user");
            // call endpoint
            DefaultResponse responseDetailsData;
            UserRequest requestData = new UserRequest
            {
                EmailAddress = dataContext.GetEmail(),
                givenName = dataContext.GetGivenName(),
                surname = dataContext.GetSurname()
            };
            UserActionResult actionResult = endpoint.Create(requestData, out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_Lookup()
        {
            OnBeforeTest("AccountManager", "UserEndpoint", "Get user information");
            // call endpoint
            DefaultResponse responseDetailsData;

            UserActionResult actionResult = endpoint.Lookup(dataContext.GetEmail(), out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_ResetLostPassword()
        {
            OnBeforeTest("AccountManager", "UserEndpoint", "Reset user password");
            // call endpoint
            DefaultResponse responseDetailsData;
            UserActionResult actionResult = endpoint.ResetLostPassword(dataContext.GetEmail(), out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_Update()
        {
            OnBeforeTest("AccountManager", "UserEndpoint", "Update user info");
            // call endpoint
            DefaultResponse responseDetailsData;
            UserUpdateRequest requestData = new UserUpdateRequest
            {
                givenName = dataContext.GetGivenName(),
                surname = dataContext.GetSurname()
            };
            UserActionResult actionResult = endpoint.Update(requestData, dataContext.GetEmail(), out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_Assign()
        {
            OnBeforeTest("AccountManager", "UserEndpoint", "Assign role to user");
            // call endpoint
            UserRolesResponse responseDetailsData;
            List<UserRoleUrn> roleUrns = new List<UserRoleUrn>();
            roleUrns.Add(new UserRoleUrn(dataContext.GetRoleUrn()));
            UserActionResult actionResult = endpoint.Assign(dataContext.GetEmail(), roleUrns, out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }

        protected virtual bool RunTestCase_Dissociate()
        {
            OnBeforeTest("AccountManager", "UserEndpoint", "Remove role from user");
            // call endpoint
            DefaultResponse responseDetailsData;
            UserActionResult actionResult = endpoint.Dissociate(dataContext.GetEmail(), dataContext.GetRoleUrn(), out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == UserActionResult.Successful);
        }
    }
}