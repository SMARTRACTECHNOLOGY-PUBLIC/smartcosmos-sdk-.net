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
using Smartrac.SmartCosmos.Objects.Registration;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(10)]
    public class TestCaseRegistrationEndpoint : BaseTestCaseRegistrationEndpoint
    {
        protected override bool ExecuteTests()
        {
            string emailVerificationToken;

            return RunTestCase_RealmAvailability() &&
                RunTestCase_AccountRegistration(out emailVerificationToken) &&
                RunTestCase_ConfirmAccountRegistration(emailVerificationToken);
        }

        protected virtual bool RunTestCase_RealmAvailability()
        {
            OnBeforeTest("Objects", "RegistrationEndpoint", "Realm Availability");
            RealmAvailabilityResponse responseRealmData;
            // call endpoint
            RegistrationActionResult actionResult = endpoint.GetRealmAvailability(dataContext.GetRealm(), out responseRealmData);
            OnAfterTest(actionResult);
            return (actionResult == RegistrationActionResult.Successful);
        }

        protected virtual bool RunTestCase_AccountRegistration(out string emailVerificationToken)
        {
            OnBeforeTest("Objects", "RegistrationEndpoint", "Account Registration");
            // create client for endpoint
            AccountRegistrationRequest requestRegisterData = new AccountRegistrationRequest
            {
                realm = dataContext.GetRealm(),
                emailAddress = dataContext.GeteMailAddress()
            };
            AccountRegistrationResponse responseRegisterData;
            // call endpoint
            RegistrationActionResult actionResult = endpoint.RegisterAccount(requestRegisterData, out responseRegisterData);
            // emailVerificationToken
            if (responseRegisterData != null)
            {
                emailVerificationToken = responseRegisterData.emailVerificationToken;
            }
            else
            {
                emailVerificationToken = "";
            }
            OnAfterTest(actionResult);
            return (actionResult == RegistrationActionResult.Successful);
        }

        protected virtual bool RunTestCase_ConfirmAccountRegistration(string emailVerificationToken)
        {
            if (string.IsNullOrEmpty(emailVerificationToken))
            {
                Logger.AddLog("Skip RunTestCase_AccountRegistration", LogType.Info);
                return true;
            }

            OnBeforeTest("Objects", "RegistrationEndpoint", "Confirm Registration");
            // create client for endpoint
            ConfirmRegistrationRequest requestRegConfirmData = new ConfirmRegistrationRequest
            {
                emailAddress = dataContext.GeteMailAddress(),
                emailVerificationToken = emailVerificationToken
            };
            ConfirmRegistrationResponse responseRegConfirmData;
            // call endpoint
            RegistrationActionResult actionResult = endpoint.ConfirmAccountRegistration(requestRegConfirmData, out responseRegConfirmData);

            // user + pwd
            if (responseRegConfirmData != null)
            {
                if (EndpointFactory.ProfilesUserName == "")
                {
                    EndpointFactory.ProfilesUserName = dataContext.GeteMailAddress();
                    EndpointFactory.ProfilesUserPassword = responseRegConfirmData.userPassword;
                }
            }
            // log response
            OnAfterTest(actionResult);
            return (actionResult == RegistrationActionResult.Successful);
        }
    }
}