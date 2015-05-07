﻿#region License

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

using Smartrac.SmartCosmos.CredentialStore;
using Smartrac.SmartCosmos.Logging;
using Smartrac.SmartCosmos.Objects.Registration;
using Smartrac.SmartCosmos.TestCase.Base;
using System.Net;

namespace Smartrac.SmartCosmos.Objects.TestCase.Sample
{
    [TestCaseAttribute(1)]
    public class TestCaseTransactionEndpoint : BaseTestCaseRegistrationEndpoint
    {
        protected override bool ExecuteTests()
        {
            return true; // todo
        }

        /*
        protected virtual bool RunTestCase_RealmAvailability()
        {
            OnBeforeTest("Objects", "RegistrationEndpoint", "Realm Availability");
            RealmAvailabilityResponse responseRealmData;
            // call endpoint
            RegistrationActionResult actionResult = endpoint.GetRealmAvailability(dataContext.GetRealm(), out responseRealmData);
            OnAfterTest(actionResult);
            return (actionResult == RegistrationActionResult.Successful);
        }
         */
    }
}