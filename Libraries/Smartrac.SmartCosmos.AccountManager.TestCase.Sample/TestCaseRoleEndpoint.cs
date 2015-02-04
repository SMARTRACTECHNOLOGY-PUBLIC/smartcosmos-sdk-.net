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

using Smartrac.SmartCosmos.AccountManager.Role;
using Smartrac.SmartCosmos.AccountManager.User;
using Smartrac.SmartCosmos.ClientEndpoint.BaseObject;
using Smartrac.SmartCosmos.TestCase.Base;
using System.Collections.Generic;

namespace Smartrac.SmartCosmos.AccountManager.TestCase.Sample
{
    [TestCaseAttribute(3)]
    public class TestCaseRoleEndpoint : BaseTestCaseRoleEndpoint
    {
        protected override bool ExecuteTests()
        {
            Urn roleUrn;
            return RunTestCase_CreateRole(out roleUrn) &&
                RunTestCase_LookupRoles() &&
                RunTestCase_DeleteRole(roleUrn) ;
        }

        protected virtual bool RunTestCase_CreateRole(out Urn roleUrn)
        {
            OnBeforeTest("AccountManager", "RoleEndpoint", "Create user");
            // call endpoint
            DefaultResponse responseDetailsData;
            RoleActionResult actionResult = endpoint.Create(new RoleNameRequest(dataContext.GetName()), out responseDetailsData);
            if (actionResult == RoleActionResult.Successful)
            {
                roleUrn = new Urn(responseDetailsData.message);
            }
            else
            {
                roleUrn = null;
            }
            // log response
            OnAfterTest(actionResult);
            return (actionResult == RoleActionResult.Successful);
        }

        protected virtual bool RunTestCase_LookupRoles()
        {
            OnBeforeTest("AccountManager", "RoleEndpoint", "Get all available roles");
            // call endpoint
            RolesResponse responseDetailsData;
            RoleActionResult actionResult = endpoint.Lookup(out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == RoleActionResult.Successful);
        }

        protected virtual bool RunTestCase_DeleteRole(Urn roleUrn)
        {
            OnBeforeTest("AccountManager", "RoleEndpoint", "Remove role");
            // call endpoint
            DefaultResponse responseDetailsData;
            RoleActionResult actionResult = endpoint.Delete(roleUrn, out responseDetailsData);
            // log response
            OnAfterTest(actionResult);
            return (actionResult == RoleActionResult.Successful);
        }        
        
    }
}