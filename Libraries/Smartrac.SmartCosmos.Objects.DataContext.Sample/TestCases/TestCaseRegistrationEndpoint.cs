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
using Smartrac.SmartCosmos.Objects.Registration;
using Smartrac.SmartCosmos.TestCase.Base;

namespace Smartrac.SmartCosmos.Objects.DataContext.Sample
{
    [TestCaseAttribute(10)]
    public class TestCaseRegistrationEndpoint : BaseObjectsTestCase
    {
        protected override bool DoRun()
        {
            IRegistrationDataContext dataContext = DataContextFactory.CreateRegistrationDataContext();
            if (dataContext == null)
            {
                Logger.AddLog("");
                Logger.AddLog("Skip RegistrationEndpoint test cases, because of missing data context", LogType.Info);
                return true;
            }

            // create client for endpoint
            IRegistrationEndpoint tester = EndpointFactory.CreateRegistrationEndpoint();
            if (tester == null)
            {
                Logger.AddLog("");
                Logger.AddLog("CreateRegistrationEndpoint failed", LogType.Error);
                return false;
            }

            bool result = true;
            RegistrationActionResult actionResult;
            string emailVerificationToken = "";

            OnBeforeTest("Objects", "RegistrationEndpoint", "Realm Availability");
            RealmAvailabilityResponse responseRealmData;
            // call endpoint
            actionResult = tester.GetRealmAvailability(dataContext.GetRealm(), out responseRealmData);
            result = result && (actionResult == RegistrationActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            if (responseRealmData != null)
                Logger.AddLog("Result Data: " + responseRealmData.ToJSON());
            OnAfterTest();

            OnBeforeTest("Objects", "RegistrationEndpoint", "Account Registration");
            // create client for endpoint
            AccountRegistrationRequest requestRegisterData = new AccountRegistrationRequest
            {
                realm = dataContext.GetRealm(),
                emailAddress = dataContext.GeteMailAddress()
            };
            AccountRegistrationResponse responseRegisterData;
            // call endpoint
            actionResult = tester.RegisterAccount(requestRegisterData, out responseRegisterData);
            result = result && (actionResult == RegistrationActionResult.Successful);
            // log response
            Logger.AddLog("Result: " + actionResult);
            if (responseRealmData != null)
            {
                Logger.AddLog("Result Data: " + responseRegisterData.ToJSON());
                emailVerificationToken = responseRegisterData.emailVerificationToken;
            }
            OnAfterTest();

            if (!string.IsNullOrEmpty(emailVerificationToken))
            {
                OnBeforeTest("Objects", "RegistrationEndpoint", "Confirm Registration");
                // create client for endpoint
                ConfirmRegistrationRequest requestRegConfirmData = new ConfirmRegistrationRequest
                {
                    emailAddress = dataContext.GeteMailAddress(),
                    emailVerificationToken = emailVerificationToken
                };
                ConfirmRegistrationResponse responseRegConfirmData;
                // call endpoint
                actionResult = tester.ConfirmAccountRegistration(requestRegConfirmData, out responseRegConfirmData);
                result = result && (actionResult == RegistrationActionResult.Successful);
                // log response
                Logger.AddLog("Result: " + actionResult);
                if (responseRegConfirmData != null)
                {
                    Logger.AddLog("Result Data: " + responseRegConfirmData.ToJSON());
                    if (EndpointFactory.UserName == "")
                    {
                        EndpointFactory.UserName = dataContext.GeteMailAddress();
                        EndpointFactory.UserPassword = responseRegConfirmData.userPassword;
                    }
                }
                OnAfterTest();
            }

            return result;
        }
    }
}