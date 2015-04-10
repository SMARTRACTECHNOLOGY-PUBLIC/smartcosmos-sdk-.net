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

using Smartrac.SmartCosmos.ClientEndpoint.Base;
using Smartrac.SmartCosmos.AccountManager.Base;
using System;

namespace Smartrac.SmartCosmos.AccountManager.DataContext.Sample
{
    public static class MyDataContext
    {
       
        public static string GetName()
        {
            return "testSDK";
        }

        public static Email GetEmail()
        {
            return new Email("testSDK@test.sdk");
        }

        public static Urn GetRoleUrn()
        {
            return new Urn("urn:uuid:0ac50422-ba9b-4797-b5b1-bdbc32e13680");
        }

        public static string GetSurname()
        {
            return "SDK";
        }

        public static bool GetEnabled()
        {
            return true;
        }

        public static string GetGivenName()
        {
            return "TestSDK";
        }
    }
}